using System.Windows;
using System.Windows.Input;
using GitSearch.ViewModel;


namespace GitSearch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        // Button Szukaj
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Informacje.ZnajdzInformacje(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);
           

        }
        //

        // button szukaj enterem
        private void Enter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Informacje.ZnajdzInformacje(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);

            }

        }
        //

        // button wyjdz
        private void Button_Close(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();

        }
        //

        // Otwórz URL doubleclickiem

        private void wynikListBox_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

            Link.Doubleclick(wynikListBox);

        }
        //
    }
}