using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DadLibrarySystem
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private LibrarySearch librarySearch; 


        public SearchWindow()
        {
            InitializeComponent();
            librarySearch = new DadLibrarySystem.LibrarySearch(); 
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearchTerm.Text;

            if (rbAuthor.IsChecked == true)
            {
                DisplaySearchResults(LibrarySearch.SearchType.ByAuthor, searchTerm);
            }
            else if (rbTitle.IsChecked == true)
            {
                DisplaySearchResults(LibrarySearch.SearchType.ByTitle, searchTerm);
            }
            else if (rbSubject.IsChecked == true)
            {
                DisplaySearchResults(LibrarySearch.SearchType.BySubject, searchTerm);
            }
            else if (rbKeyWord.IsChecked == true)
            {
                DisplaySearchResults(LibrarySearch.SearchType.ByKeyword, searchTerm);
            }
        }

        private void DisplaySearchResults(LibrarySearch.SearchType searchType, string searchTerm)
        {
            var results = librarySearch.SearchBooks(searchType, searchTerm);
            resultListView.ItemsSource = results;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();         

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearchTerm.Clear();
            resultListView.ItemsSource = null;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = resultListView.SelectedItem as DadLibrary;

            if (selectedBook != null)
            {
                string title = selectedBook.Title;

                this.Hide();
                ManagementWindow managementWindow = new ManagementWindow(selectedBook);

                managementWindow.Show(); 

            }

            else
            {
                MessageBox.Show("Please select a book to use this button.");
            }
        }
    }
}