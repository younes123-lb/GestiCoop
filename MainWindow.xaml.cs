
using System.Windows;
using System.Windows.Controls;
using GestiCoop.Views;

namespace GestiCoop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AfficherPage("Tableau de bord");
        }

        private void Navigation_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is Button bouton)
            {
                string page = bouton.Tag?.ToString() ?? "";
                AfficherPage(page);
            }
        }


        private void AfficherPage(string page)
        {
            TitrePage.Text = page;

            if (page == "Étudiants")
            {
                ContenuPage.Content = new EtudiantsView();
            }
            else
            {
                ContenuPage.Content = new TextBlock
                {
                    Text = "Bienvenue dans la section " + page,
                    FontSize = 22,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
        }

    }
}

