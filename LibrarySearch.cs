using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DadLibrarySystem
{
    public class LibrarySearch
    {
        private string connectionString = "Data Source=GKK-HP;Initial Catalog=DadLibrarydb;Integrated Security=True;";
        public static DadLibrarydbEntities1 Entities = new DadLibrarydbEntities1();

        public enum SearchType
        {
            ByAuthor,
            ByTitle,
            BySubject,
            ByKeyword
        }

        public ObservableCollection<DadLibrary> SearchBooks(SearchType searchType, string searchTerm)
        {
            ObservableCollection<DadLibrary> searchResults = new ObservableCollection<DadLibrary>();

            try
            {
                switch (searchType)
                {
                    case SearchType.ByAuthor:
                        searchResults = new ObservableCollection<DadLibrary>(Entities.DadLibraries.Where(b => b.Author.Contains(searchTerm)));
                        break;

                    case SearchType.ByTitle:
                        searchResults = new ObservableCollection<DadLibrary>(Entities.DadLibraries.Where(b => b.Title.Contains(searchTerm)));
                        break;

                    case SearchType.BySubject:
                        searchResults = new ObservableCollection<DadLibrary>(Entities.DadLibraries.Where(b => b.Subject.Contains(searchTerm)));
                        break;

                    case SearchType.ByKeyword:
                        searchResults = new ObservableCollection<DadLibrary>(Entities.DadLibraries.Where(b => b.KeyWords.Contains(searchTerm)));
                        break;
                }

                if (searchResults.Count == 0)
                {
                    MessageBox.Show("No results found.", "Search Results", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return searchResults;
        }
    }
}