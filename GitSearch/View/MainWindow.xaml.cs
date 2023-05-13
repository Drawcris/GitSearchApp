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
            FunkcjaVM.Funckja(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);
           

        }
        //

        // button szukaj enterem
        private void Enter_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FunkcjaVM.Funckja(avatarImage, wynikListBox, accountNameTextBox, resultsTextBlock);

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

       


    }
}