using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GestionNutri
{
    public partial class register : Form
    {
        SqlConnection cnx;
        public register()
        {
            InitializeComponent();
            cnx = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetPateint;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }

        private void Connecter_Click(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();

                string sql = "INSERT INTO Compte (adresse, [mot de passe]) VALUES (@adresse, @motDePasse)";

                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@adresse", textBox.Text);
                    cmd.Parameters.AddWithValue("@motDePasse", textBox2.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Compte ajouté avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Aucune ligne n'a été affectée.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            finally
            {
                cnx.Close();
            }
        }

        private void register_Load(object sender, EventArgs e)
        {

        }
    }
}
