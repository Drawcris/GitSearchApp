﻿using System;
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
            }
        }



        public class Repository
        {

           
            public string Name { get; set; }
            public string Description { get; set; }
            public string html_url { get; set; }
        }
    
    }
}
