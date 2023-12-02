using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionNutri
{
    public partial class Form1 : Form
    {
        SqlConnection cnx;
        public Form1()
        {
            InitializeComponent();
            cnx = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetPateint;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Connecter_Click(object sender, EventArgs e)
        {
            string adresse = textBox_add.Text;
            string mot_de_passe = textBox_mdp.Text;

            // Vérifier les informations de connexion
            if (VerifierConnexion(adresse, mot_de_passe))
            {
                // Connexion réussie, afficher l'interface HomeForm
                Acceuil AcceuilForm = new Acceuil();
                AcceuilForm.Show();
                this.Hide();
                // Ajoutez ici le code pour ouvrir votre interface HomeForm
            }
            else
            {
                // Informations de connexion incorrectes
                MessageBox.Show("Adresse et mot de passe incorrects !");
            }
        }
        private bool VerifierConnexion(string adresse, string mot_de_passe)
        {
            try
            {
                cnx.Open();

                // Requête SQL pour vérifier les informations de connexion
                string query = "SELECT COUNT(*) FROM Compte WHERE adresse = @adresse AND [mot de passe] = @mot_de_passe";
                using (SqlCommand cmd = new SqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@adresse", adresse);
                    cmd.Parameters.AddWithValue("@mot_de_passe", mot_de_passe);

                    int compte = (int)cmd.ExecuteScalar();

                    // Si le compte existe, renvoyer true, sinon false
                    return compte > 0;
                }
            }
            finally
            {
                cnx.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register registerForm = new register();
            registerForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox_mdp.UseSystemPasswordChar = false;
            else
                textBox_mdp.UseSystemPasswordChar = true;
        }
    }
}
