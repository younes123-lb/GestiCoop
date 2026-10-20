using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestiCoop.Views
{
    /// <summary>
    /// Interaction logic for AjouterEtudiantWindow.xaml
    /// </summary>
    public partial class AjouterEtudiantWindow : Window
    {


        public AjouterEtudiantWindow()
        {
            InitializeComponent();

            DataContext = new GestiCoop.ViewModels.AjouterEtudiantVM();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCourriel_TextChanged(object sender,
            System.Windows.Controls.TextChangedEventArgs e)
        {
            string courriel = txtCourriel.Text.Trim();

            bool valide = System.Text.RegularExpressions.Regex.IsMatch(
                courriel,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            txtErreur.Visibility =
                !string.IsNullOrEmpty(courriel) && !valide
                ? Visibility.Visible
                : Visibility.Collapsed;

            txtCourriel.BorderBrush =
                !string.IsNullOrEmpty(courriel) && !valide
                ? System.Windows.Media.Brushes.Tomato
                : System.Windows.Media.Brushes.Gray;
            VerifierChamps(sender, e);
        }


        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) ||
                string.IsNullOrWhiteSpace(txtPrenom.Text) ||
                string.IsNullOrWhiteSpace(txtCourriel.Text) ||
                cmbProgramme.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtCourriel.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Adresse courriel invalide.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtNumero.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(
                    txtNumero.Text, @"^\d{7}$"))
            {
                MessageBox.Show("Le numéro d'étudiant doit contenir 7 chiffres.");
                return;
            }

            DialogResult = true;
        }

        private void VerifierChamps(object sender, EventArgs e)
        {
            bool valide =
                !string.IsNullOrWhiteSpace(txtNom.Text) &&
                !string.IsNullOrWhiteSpace(txtPrenom.Text) &&
                System.Text.RegularExpressions.Regex.IsMatch(
                    txtCourriel.Text,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                cmbProgramme.SelectedItem != null &&
                (string.IsNullOrWhiteSpace(txtNumero.Text) ||
                 System.Text.RegularExpressions.Regex.IsMatch(
                     txtNumero.Text, @"^\d{7}$"));

            btnEnregistrer.IsEnabled = valide;
        }

    }
}
