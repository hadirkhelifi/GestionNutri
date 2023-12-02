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
    public partial class Regime : Form
    {
        SqlConnection cnx;
        SqlDataReader dr;
        SqlCommand cmd;
        public Regime()
        {
            InitializeComponent();
            cnx = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjetPateint;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            AfficherDonneesDansDataGridView();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void AfficherDonneesDansDataGridView()
        {
            string sql = "SELECT * FROM Regime";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, cnx))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Regime");

                // Liaison de l'ensemble de données au DataGridView
                dataGridView1.DataSource = dataSet.Tables["Regime"];
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
                    textBox_name.Text = selectedRow.Cells["NomPatient"].Value.ToString();
                    textBox_p.Text = selectedRow.Cells["Pointact"].Value.ToString();
                    textBox_pp.Text = selectedRow.Cells["PointPred"].Value.ToString();
                    textBox_pd.Text = selectedRow.Cells["Petitdej"].Value.ToString();
                    textBox_r.Text = selectedRow.Cells["Repat"].Value.ToString();
                    textBox_d.Text = selectedRow.Cells["Dinner"].Value.ToString();
                    textBox_t.Text = selectedRow.Cells["temps"].Value.ToString();
                    textBox_rm.Text = selectedRow.Cells["Remarque"].Value.ToString();
                }
                else
                {
                    // Gérez le cas où la ligne sélectionnée n'a pas suffisamment de cellules
                    // Affichez un message d'erreur, par exemple
                    MessageBox.Show("La ligne sélectionnée ne contient pas suffisamment de cellules.");
                }
            }
        }
            private void button1_Click(object sender, EventArgs e)
        {

            cnx.Open();
            string sql = "INSERT INTO Regime(NomPatient, Pointact, PointPred, Petitdej, Repat, Dinner, temps, Remarque) " +
                         "VALUES(@NomPatient, @Pointact, @PointPred, @Petitdej, @Repat, @Dinner, @temp, @Remarque)";

            using (SqlCommand cmd = new SqlCommand(sql, cnx))
            {
                cmd.Parameters.AddWithValue("@NomPatient", textBox_name.Text);
                cmd.Parameters.AddWithValue("@Pointact", textBox_p.Text);
                cmd.Parameters.AddWithValue("@PointPred", textBox_pp.Text);
                cmd.Parameters.AddWithValue("@Petitdej", textBox_pd.Text);
                cmd.Parameters.AddWithValue("@Repat", textBox_r.Text);
                cmd.Parameters.AddWithValue("@Dinner", textBox_d.Text);
                cmd.Parameters.AddWithValue("@temp", textBox_t.Text);
                cmd.Parameters.AddWithValue("@Remarque", textBox_rm.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data inserted successfully.");
                    AfficherDonneesDansDataGridView();
                    cnx.Close();
                }
                else
                {
                    MessageBox.Show("No rows were affected.");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the ID value from the corresponding column in the selected row
                int idToUpdate = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                cnx.Open();
                String sql = "UPDATE Regime SET NomPatient = @NomPatient, Pointact = @Pointact, PointPred = @PointPred, Petitdej = @Petitdej, Repat = @Repat, Dinner = @Dinner, temps = @temps, Remarque=@Remarque WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@NomPatient", textBox_name.Text);
                    cmd.Parameters.AddWithValue("@Pointact", textBox_p.Text);
                    cmd.Parameters.AddWithValue("@PointPred", textBox_d.Text);
                    cmd.Parameters.AddWithValue("@Petitdej", textBox_pd.Text);
                    cmd.Parameters.AddWithValue("@Repat", textBox_r.Text);
                    cmd.Parameters.AddWithValue("@Dinner", textBox_d.Text);
                    cmd.Parameters.AddWithValue("@temps", textBox_t.Text);
                    cmd.Parameters.AddWithValue("@Remarque", textBox_rm.Text);

                    // Use the ID value retrieved from the selected row
                    cmd.Parameters.AddWithValue("@Id", idToUpdate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Modification réussie.");
                        AfficherDonneesDansDataGridView(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Aucune ligne modifiée. Vérifiez l'ID.");
                    }
                }

                cnx.Close(); // Move the closing of the connection outside the using block
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                cnx.Open();
                string sql = "DELETE FROM Regime WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cnx))
                {
                    cmd.Parameters.AddWithValue("@Id", idToDelete);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Suppression réussie.");
                        AfficherDonneesDansDataGridView(); // Rafraîchir le DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Aucune ligne supprimée. Vérifiez l'ID.");
                    }
                }

                cnx.Close();
            }
            else
            {
                MessageBox.Show("Aucune ligne sélectionnée. Veuillez sélectionner une ligne à supprimer.");
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            cnx.Open();

            cmd = new SqlCommand("SELECT * FROM Regime WHERE NomPatient LIKE @NomPatient", cnx);
            cmd.Parameters.AddWithValue("@NomPatient", "%" + textBox9.Text + "%");

            dr = cmd.ExecuteReader();

            // Créer une DataTable pour stocker les données
            DataTable dataTable = new DataTable();

            // Charger les données du DataReader dans la DataTable
            dataTable.Load(dr);

            // Assigner la DataTable comme source de données du DataGridView
            dataGridView1.DataSource = dataTable;

            // Fermer le DataReader et la connexion
            dr.Close();
            cnx.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           string NomPatient = textBox_name.Text;
            string Pointact = textBox_p.Text;
            string PointPred = textBox_pd.Text;
            string Petitdej = textBox_pd.Text;
            string Repatl = textBox_r.Text;
            string Dinner = textBox_d.Text;
            string temps = textBox_t.Text;
            string Remarque = textBox_rm.Text;

            // Créer une instance du nouveau formulaire (FicheForm) et l'afficher
            FicheRegime FicheRegimeForm = new FicheRegime(NomPatient, Pointact, PointPred, Petitdej, Repatl, Dinner, temps, Remarque);
            FicheRegimeForm.Show();

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

        private void label2_Click(object sender, EventArgs e)
        {
            Form1 Form1Form = new Form1();
            Form1Form.Show();
            this.Hide();
        }
    }
}



