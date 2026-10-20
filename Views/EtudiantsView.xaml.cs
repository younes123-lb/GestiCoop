using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestiCoop.Views
{
    public partial class EtudiantsView : UserControl
    {
        public class Etudiant
        {
            public string Nom { get; set; } = "";
            public string Prenom { get; set; } = "";
            public string Programme { get; set; } = "";
            public string Courriel { get; set; } = "";
        }

        private ObservableCollection<Etudiant> etudiants =
            new ObservableCollection<Etudiant>();

        private ICollectionView vueEtudiants = null!;

        public EtudiantsView()
        {
            InitializeComponent();

            etudiants.Add(new Etudiant
            {
                Nom = "Tremblay",
                Prenom = "Sophie",
                Programme = "Informatique",
                Courriel = "s.tremblay@etud.lacite.ca"
            });

            etudiants.Add(new Etudiant
            {
                Nom = "Nguyen",
                Prenom = "Minh",
                Programme = "Informatique",
                Courriel = "m.nguyen@etud.lacite.ca"
            });

            etudiants.Add(new Etudiant
            {
                Nom = "Dubois",
                Prenom = "Élodie",
                Programme = "Génie logiciel",
                Courriel = "e.dubois@etud.lacite.ca"
            });

            etudiants.Add(new Etudiant
            {
                Nom = "Okonkwo",
                Prenom = "Chidi",
                Programme = "Cybersécurité",
                Courriel = "c.okonkwo@etud.lacite.ca"
            });

            etudiants.Add(new Etudiant
            {
                Nom = "Lavoie",
                Prenom = "Xavier",
                Programme = "Informatique",
                Courriel = "x.lavoie@etud.lacite.ca"
            });

            vueEtudiants =
                CollectionViewSource.GetDefaultView(etudiants);

            vueEtudiants.Filter = FiltrerEtudiant;
            dgEtudiants.ItemsSource = vueEtudiants;
        }

        private bool FiltrerEtudiant(object objet)
        {
            if (objet is not Etudiant etudiant)
                return false;

            string recherche =
                txtRecherche.Text.Trim();

            string programme =
                (cboProgramme.SelectedItem as ComboBoxItem)?
                .Content?.ToString() ?? "Tous les programmes";

            bool correspondRecherche =
                string.IsNullOrWhiteSpace(recherche) ||
                etudiant.Nom.Contains(recherche,
                    StringComparison.OrdinalIgnoreCase) ||
                etudiant.Prenom.Contains(recherche,
                    StringComparison.OrdinalIgnoreCase) ||
                etudiant.Courriel.Contains(recherche,
                    StringComparison.OrdinalIgnoreCase);

            bool correspondProgramme =
                programme == "Tous les programmes" ||
                etudiant.Programme == programme;

            return correspondRecherche && correspondProgramme;
        }

        private void txtRecherche_TextChanged(
            object sender, TextChangedEventArgs e)
        {
            vueEtudiants?.Refresh();
        }

        private void cboProgramme_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            vueEtudiants?.Refresh();
        }

        private void Ajouter_Click(
            object sender, RoutedEventArgs e)
        {
            AjouterEtudiantWindow fenetre = new AjouterEtudiantWindow();
            fenetre.Owner = System.Windows.Application.Current.MainWindow;
            fenetre.ShowDialog();
        }

        private void Modifier_Click(
            object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Le formulaire de modification sera créé ensuite.");
        }

        private void Supprimer_Click(
            object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext
                is not Etudiant etudiant)
                return;

            var confirmation = MessageBox.Show(
                "Supprimer " + etudiant.Prenom + " " +
                etudiant.Nom + " ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
                etudiants.Remove(etudiant);
        }
    }
}
