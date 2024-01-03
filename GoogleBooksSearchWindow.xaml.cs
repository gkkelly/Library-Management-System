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

namespace DadLibrarySystem
{
    /// <summary>
    /// Interaction logic for GoogleBooksSearchWindow.xaml
    /// </summary>
    public partial class GoogleBooksSearchWindow : Window
    {
        public GoogleBooksSearchWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearchTerm.Text;
            var googleBooksService = new GoogleBooksService();
            var results = googleBooksService.SearchBooks(searchTerm, "q");

            if (results != null && results.Count > 0)
            {
                resultListView.ItemsSource = results;
            }
            else
            {
                MessageBox.Show("No results found.");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearchTerm.Clear();
            resultListView.ItemsSource = null;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
