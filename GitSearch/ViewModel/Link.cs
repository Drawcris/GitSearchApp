using System.Windows.Controls;
using System.Diagnostics;

namespace GitSearch.ViewModel
{
    public static class Link
    {
        public static void Doubleclick(ListBox wynikListBox)
        {
            if (wynikListBox.SelectedItem != null)
            {
                ListBoxItem? item = wynikListBox.SelectedItem as ListBoxItem;
                string url = GetUrlFromListBoxItem(item);
                Process.Start("cmd.exe", $"/C start {url}");
            }
        }

        public static string GetUrlFromListBoxItem(ListBoxItem item)
        {
            string content = item.Content.ToString();
            int startIndex = content.IndexOf("Link do repozytorium: ") + "Link do repozytorium: ".Length;
            int endIndex = content.IndexOf("\n", startIndex);
            return content.Substring(startIndex, endIndex - startIndex);
        }
    }
}
