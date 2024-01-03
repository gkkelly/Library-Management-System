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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DadLibrarySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnManage_Click(object sender, RoutedEventArgs e) // open management window
        {
            ManagementWindow managementWindow = new ManagementWindow();

            managementWindow.Show();

            this.Hide();  
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)  // open search window
        {
            SearchWindow searchWindow = new SearchWindow();

            searchWindow.Show();

            this.Hide();  

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)  // exit application  
        {
            Application.Current.Shutdown();  
        }

        private void BtnSearchOpenLibrary_Click(object sender, RoutedEventArgs e)
        {
            OpenLibrarySearchWindow openlibrarysearchWindow = new OpenLibrarySearchWindow();

            openlibrarysearchWindow.Show();

            this.Hide();
        }

        private void BtnSearchGoogleBooks_Click(object sender, RoutedEventArgs e)
        {
            GoogleBooksSearchWindow googlebookssearchWindow = new GoogleBooksSearchWindow();

            googlebookssearchWindow.Show(); 

            this.Hide();
        }
    }
}
