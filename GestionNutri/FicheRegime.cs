using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace GestionNutri
{
    public partial class FicheRegime : Form
    {
        public FicheRegime(string NomPatient, string Pointact, string PointPred, string Petitdej, string Repatl, string Dinner, string temps, string Remarque)
        {
            InitializeComponent();
            label_name.Text = NomPatient;
            label_Pointact.Text = Pointact;
            label_PointPred.Text = PointPred;
            label_Petitdej.Text = Petitdej;
            label_Repat.Text = Repatl;
            label_Dinner.Text = Dinner;
            label_temps.Text = temps;
            label_Remarque.Text = Remarque;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(ImprimerFiche);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void ImprimerFiche(object sender, PrintPageEventArgs e)
        {
            // Définir le contenu à imprimer (le contenu de votre formulaire)
            string contenuAImprimer = $"Nom : {label_name.Text}\n" +
                                      $"Prénom : {label_Pointact.Text}\n" +
                                      $"le poind precedent : {label_PointPred.Text}\n" +
                                      $"le poind actuel : {label_Petitdej.Text}\n" +
                                      $"Repat : {label_Repat.Text}\n" +
                                      $"Dinner : {label_Dinner.Text}\n" +
                                      $"temps : {label_temps.Text}\n" + // Correction : point-virgule ajouté ici
                                      $"Remarque : {label_Remarque.Text}";

            // Définir la police et la position pour l'impression
            Font police = new Font("Arial", 12);
            PointF position = new PointF(10, 10);

            // Dessiner le contenu sur la page
            e.Graphics.DrawString(contenuAImprimer, police, Brushes.Black, position);
        }
    }
}
