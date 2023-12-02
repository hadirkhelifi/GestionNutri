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
    public partial class FichePateints : Form
    {
        public FichePateints(string nom, string prenom, string dateNaissance, string numTelephone, string adresseMail, string poids, string objectif)
        {
            InitializeComponent();
            labelNom.Text = nom;
            labelPrenom.Text = prenom;
            labelnaiss.Text = dateNaissance;
            labelNumTelephone.Text = numTelephone;
            labelAdresseMail.Text = adresseMail;
            labelPoids.Text = poids;
            label.Text = objectif;
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
            string contenuAImprimer = $"Nom : {labelNom.Text}\n" +
                                      $"Prénom : {labelPrenom.Text}\n" +
                                      $"Date de naissance : {labelnaiss.Text}\n" +
                                      $"Numéro de téléphone : {labelNumTelephone.Text}\n" +
                                      $"Adresse mail : {labelAdresseMail.Text}\n" +
                                      $"Poids : {labelPoids.Text}\n" +
                                      $"Objectif : {label.Text}";

            // Définir la police et la position pour l'impression
            Font police = new Font("Arial", 12);
            PointF position = new PointF(10, 10);

            // Dessiner le contenu sur la page
            e.Graphics.DrawString(contenuAImprimer, police, Brushes.Black, position);
        }
    }
}
