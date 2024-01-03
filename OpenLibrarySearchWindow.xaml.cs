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
    /// Interaction logic for OpenLibrarySearchWindow.xaml
    /// </summary>
    public partial class OpenLibrarySearchWindow : Window
    {
        public OpenLibrarySearchWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearchTerm.Text;
            var openLibraryService = new OpenLibraryService();
            var results = openLibraryService.SearchBooks(searchTerm, "title");

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

        // transfer open library search result to dad's library  
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (resultListView.SelectedItem != null)
            {
                var selectedBook = (Doc)resultListView.SelectedItem;
                var openlibraryBook = new OpenLibraryBookTransfer
                {
                    Title = selectedBook.title,
                    Author = selectedBook.author_name?.FirstOrDefault(),
                    CallNum = selectedBook.ddc?.FirstOrDefault(),
                    ISBN = selectedBook.isbn?.FirstOrDefault(),
                    Subject = selectedBook.subject?.FirstOrDefault(),


                };

                ManagementWindow managementWindow = new ManagementWindow(openlibraryBook);
                managementWindow.Show();

                this.Hide();
            }
        }
    }
}

