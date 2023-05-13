using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;


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

            Funckja(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);

        }
        //

        // button szukaj enterem
        private void Enter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Funckja(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);

            }

        }
        //

        // button wyjdz
        private void Button_Close(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();

        }
        //

        // Otwórz URL
        private void wynikListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (wynikListBox.SelectedItem != null)
            {
                ListBoxItem item = wynikListBox.SelectedItem as ListBoxItem;
                string url = GetUrlFromListBoxItem(item);
                Process.Start("cmd.exe", $"/C start {url}");
            }
        }

        private string GetUrlFromListBoxItem(ListBoxItem item)
        {
            string content = item.Content.ToString();
            int startIndex = content.IndexOf("Link do repozytorium: ") + "Link do repozytorium: ".Length;
            int endIndex = content.IndexOf("\n", startIndex);
            return content.Substring(startIndex, endIndex - startIndex);
        }
        //

        public class Repository
        {


            public string Name { get; set; }
            public string Description { get; set; }
            public string html_url { get; set; }
            public string avatar_url { get; set; }
            public DateTime created_at { get; set; }
            public string login { get; set; }
            public int id { get; set; }
        }

        public async static void Funckja(Image avatarImage, ListBox wynikListBox, TextBox accountNameTextBox, TextBlock resultsTextBlock)
        {
            try
            {
                //szukaj konto
                string accountName = accountNameTextBox.Text;
                string apiUrl = $"https://api.github.com/users/{accountName}/repos";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    string responseString = await response.Content.ReadAsStringAsync();

                    List<Repository> repositories = JsonConvert.DeserializeObject<List<Repository>>(responseString);


                    wynikListBox.Items.Clear();
                    resultsTextBlock.Text = ""; // wyczyszczenie zawartości TextBlocka

                    foreach (var repo in repositories)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = $"Nazwa repozytorium:   {repo.Name}\nOpis repozytorium:   {repo.Description}\nLink do repozytorium:   {repo.html_url}\n\n";
                        item.Tag = repo;
                        wynikListBox.Items.Add(item);
                    }

                    // Dodanie informacji do TextBlocka
                    resultsTextBlock.Text = $"Login: {accountName}\nData utworzenia: {repositories[0].created_at}\nID: {repositories[0].id}\n\n";





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

                    //


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





    }
}