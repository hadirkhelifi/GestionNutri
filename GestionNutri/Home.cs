using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionNutri
{
    public partial class Home : Form
    {
        SqlConnection cnx;
        public Home()
        {
            InitializeComponent();
            cnx = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetPateint;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            AfficherDonneesDansDataGridView();

        }
        private void AfficherDonneesDansDataGridView()
        {
            string sql = "SELECT * FROM PatientP";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, cnx))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "PatientP");

                // Liaison de l'ensemble de données au DataGridView
                dataGridView1.DataSource = dataSet.Tables["PatientP"];
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; // Obtenez la première ligne sélectionnée

                // Assurez-vous que le nombre de cellules dans la ligne est supérieur ou égal à l'index que vous essayez d'accéder
                if (selectedRow.Cells.Count >= 7)
                {
                    textBox_nom.Text = selectedRow.Cells["Nom"].Value.ToString();
                    textBox_prenom.Text = selectedRow.Cells["Prenom"].Value.ToString();
                    textBox_date.Text = selectedRow.Cells["Date de naissance"].Value.ToString();
                    textBox_num.Text = selectedRow.Cells["Num telephone"].Value.ToString();
                    textBox_mail.Text = selectedRow.Cells["Adresse mail"].Value.ToString();
                    textBox_poidac.Text = selectedRow.Cells["Le poids"].Value.ToString();
                    textBox_objectif.Text = selectedRow.Cells["Objectif"].Value.ToString();
                }
                else
                {
                    // Gérez le cas où la ligne sélectionnée n'a pas suffisamment de cellules
                    // Affichez un message d'erreur, par exemple
                    MessageBox.Show("La ligne sélectionnée ne contient pas suffisamment de cellules.");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string nom = textBox_nom.Text;
            string prenom = textBox_prenom.Text;
            string dateNaissance = textBox_date.Text;
            string numTelephone = textBox_num.Text;
            string adresseMail = textBox_mail.Text;
            string poids = textBox_poidac.Text;
            string objectif = textBox_objectif.Text;

            // Créer une instance du nouveau formulaire (FicheForm) et l'afficher
            FichePateints FichePateintsForm = new FichePateints(nom, prenom, dateNaissance, numTelephone, adresseMail, poids, objectif);
            FichePateintsForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    using (SqlConnection cnx = new SqlConnection("votre_chaine_de_connexion"))
                    {
                        cnx.Open();

                        string sql = "INSERT INTO PatientP(Nom, Prenom, [Date de naissance], [Num telephone], [Adresse mail], [Le poids], Objectif) " +
                                     "VALUES(@Nom, @Prenom, @DateNaissance, @NumTelephone, @AdresseMail, @LePoids, @Objectif)";

                        using (SqlCommand cmd = new SqlCommand(sql, cnx))
                        {
                            cmd.Parameters.AddWithValue("@Nom", textBox_nom.Text);
                            cmd.Parameters.AddWithValue("@Prenom", textBox_prenom.Text);
                            cmd.Parameters.AddWithValue("@DateNaissance", DateTime.ParseExact(textBox_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                            cmd.Parameters.AddWithValue("@NumTelephone", textBox_num.Text);
                            cmd.Parameters.AddWithValue("@AdresseMail", textBox_mail.Text);
                            cmd.Parameters.AddWithValue("@LePoids", textBox_poidac.Text);
                            cmd.Parameters.AddWithValue("@Objectif", textBox_objectif.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data inserted successfully.");
                                AfficherDonneesDansDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No rows were affected.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private bool ValidateInput()
        {
            // Validate textBox_nom and textBox_prenom (alphabetic characters only)
            if (!IsAlphabetic(textBox_nom.Text) || !IsAlphabetic(textBox_prenom.Text))
            {
                MessageBox.Show("Nom and Prenom should contain only alphabetic characters.");
                return false;
            }

            // Validate textBox_date (date format: dd/MM/yyyy)
            if (!IsDate(textBox_date.Text))
            {
                MessageBox.Show("Invalid Date de naissance format. Please use dd/MM/yyyy.");
                return false;
            }

            // Validate textBox_num (numeric, not exceeding 8 digits)
            if (!IsNumeric(textBox_num.Text) || textBox_num.Text.Length > 8)
            {
                MessageBox.Show("Num telephone should be a numeric value with a maximum of 8 digits.");
                return false;
            }

            // Validate textBox_mail (should contain "@gmail.com")
            if (!textBox_mail.Text.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Adresse mail should end with '@gmail.com'.");
                return false;
            }

            // Validate textBox_poidac (numeric)
            if (!IsNumeric(textBox_poidac.Text))
            {
                MessageBox.Show("Le poids should be a numeric value.");
                return false;
            }

            return true;
        }

        private bool IsAlphabetic(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsLetter);
        }

        private bool IsDate(string input)
        {
            DateTime dummyOutput;
            return DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummyOutput);
        }

        private bool IsNumeric(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }
        

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Home HomeForm = new Home();
            HomeForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Regime RegimeForm = new Regime();
            RegimeForm.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            RenvezVous RenvezVousForm = new RenvezVous();
            RenvezVousForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form1 Form1Form = new Form1();
            Form1Form.Show();
            this.Hide();
        }
    }
}
