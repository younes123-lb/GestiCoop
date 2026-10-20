
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace GestiCoop.ViewModels
{
    public class AjouterEtudiantVM : ViewModelBase
    {
        private string _nom = "";
        private string _prenom = "";
        private string _courriel = "";
        private string _programme = "";
        private string _numeroEtudiant = "";

        public string Nom
        {
            get => _nom;
            set
            {
                _nom = value;
                OnPropertyChanged();
                ActualiserValidation();
            }
        }

        public string Prenom
        {
            get => _prenom;
            set
            {
                _prenom = value;
                OnPropertyChanged();
                ActualiserValidation();
            }
        }

        public string Courriel
        {
            get => _courriel;
            set
            {
                _courriel = value;
                OnPropertyChanged();
                ActualiserValidation();
            }
        }

        public string Programme
        {
            get => _programme;
            set
            {
                _programme = value;
                OnPropertyChanged();
                ActualiserValidation();
            }
        }

        public string NumeroEtudiant
        {
            get => _numeroEtudiant;
            set
            {
                _numeroEtudiant = value;
                OnPropertyChanged();
                ActualiserValidation();
            }
        }

        public bool CourrielInvalide =>
            !string.IsNullOrWhiteSpace(Courriel) &&
            !Regex.IsMatch(Courriel,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public RelayCommand EnregistrerCommand { get; }

        public AjouterEtudiantVM()
        {
            EnregistrerCommand = new RelayCommand(
                p => Enregistrer(),
                p => FormulaireValide());
        }

        private bool FormulaireValide()
        {
            return !string.IsNullOrWhiteSpace(Nom)
                && !string.IsNullOrWhiteSpace(Prenom)
                && !string.IsNullOrWhiteSpace(Programme)
                && !CourrielInvalide
                && !string.IsNullOrWhiteSpace(Courriel)
                && (string.IsNullOrWhiteSpace(NumeroEtudiant)
                    || Regex.IsMatch(NumeroEtudiant, @"^\d{7}$"));
        }

        private void ActualiserValidation()
        {
            OnPropertyChanged(nameof(CourrielInvalide));
            EnregistrerCommand.RaiseCanExecuteChanged();
        }

        private void Enregistrer()
        {
            // La fermeture de la fenêtre sera reliée
            // à cette commande à l'étape suivante.
        }
    }
}
