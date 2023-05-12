using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace GitSearch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string accountName = accountNameTextBox.Text;
                string apiUrl = $"https://api.github.com/users/{accountName}/repos";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    string responseString = await response.Content.ReadAsStringAsync();

                    List<Repository> repositories = JsonConvert.DeserializeObject<List<Repository>>(responseString);

                    

                    wynikListBox.Items.Clear();

                    foreach (var repo in repositories)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = $"Konto: {accountName} \nNazwa repozytorium: {repo.Name}\nOpis repozytorium: {repo.Description}\nLink do repozytorium: {repo.html_url}\n\n";

                        wynikListBox.Items.Add(item);
                    }


                    // Avatar

                    string userApiUrl = $"https://api.github.com/users/{accountName}";
                    HttpResponseMessage userResponse = await client.GetAsync(userApiUrl);
                    string userResponseString = await userResponse.Content.ReadAsStringAsync();
                    Repository user = JsonConvert.DeserializeObject<Repository>(userResponseString);

                    string avatarUrl = user.avatar_url;
                    byte[] imageData = await client.GetByteArrayAsync(avatarUrl);

                    using (MemoryStream stream = new MemoryStream(imageData))
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.StreamSource = stream;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.EndInit();

                        avatarImage.Source = image;
                    }

                    ///

                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych z API GitHub. \nSprawdź połączenie z internetem bądź API.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                MessageBox.Show($"Zła nazwa konta!", "Hamuj się!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();

        }
        public class Repository
        {

           
            public string Name { get; set; }
            public string Description { get; set; }
            public string html_url { get; set; }
            public string avatar_url { get; set; }
        }

       
    }
}
