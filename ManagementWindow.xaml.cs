using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        BookRepository bookRepository = new BookRepository();

        public ObservableCollection<string> Genre { get; set; }

        // in order to get from the Search Library Window
        private DadLibrary selectedBook; 
                
        public DadLibrary SelectedBook { get; set; }
        public ManagementWindow()
        {
            InitializeComponent();
            DataContext = this;

            Genre = new ObservableCollection<string>
            {
                "Fiction",
                "NonFiction",                
            };
        }

        public ManagementWindow(DadLibrary selectedBook)
        {
            InitializeComponent();
            SelectedBook = selectedBook;
            DataContext = this; 

            PopulatefromLibrarySearchWindow();

            Genre = new ObservableCollection<string>
            {
                "Fiction",
                "NonFiction",
            };

            btnUpdateBook.IsEnabled = true;
            btnDelete.IsEnabled = true; 
        }

        public ManagementWindow(OpenLibraryBookTransfer selectedBook)
        {
            InitializeComponent();
            DataContext = this;
            Genre = new ObservableCollection<string>
            {
                "Fiction",
                "NonFiction",
            };

            txtTitle.Text = selectedBook.Title;
            txtAuthor.Text = selectedBook.Author;
            txtCallNum.Text = selectedBook.CallNum;
            txtISBN13.Text = selectedBook.ISBN;
            txtSubject.Text = selectedBook.Subject; 

        }

        private void ManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //btnSubmitNew.IsEnabled = false;
            //btnUpdateBook.IsEnabled = false;
            //btnDelete.IsEnabled = false;

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // consider making a clear entries function
            txtISBN10.Clear();
            txtISBN13.Clear();
            txtTitle.Clear();
            txtEdition.Clear();
            txtAuthor.Clear();
            comboGenre.SelectedItem = null;
            txtSubject.Clear();
            txtKeyWords.Clear();
            txtLocation.Clear();
            txtCopies.Clear();
            txtCallNum.Clear();  
            dpDate.SelectedDate = null;
            txtLoanInfo.Clear();

            btnSubmitNew.IsEnabled = true;

        }

        private void btnSubmitNew_Click(object sender, RoutedEventArgs e)  // add a new book,  use tryparse for  and integers
        {
            
            // make sure the title is not blank 
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title.");
                return;
            }
                        
            var newBook = new DadLibrary();
            newBook.ISBN10 = txtISBN10.Text;
            newBook.ISBN13 = txtISBN13.Text;
            newBook.Title = txtTitle.Text;

            if (string.IsNullOrWhiteSpace(txtEdition.Text))
            {
                newBook.Edition = null;
            }
            else if (int.TryParse(txtEdition.Text, out int edition))
            {
                newBook.Edition = edition;
            }
            else
            {
                MessageBox.Show("Please enter a valid edition number.");
                return;
            }

            newBook.Author = txtAuthor.Text;
            newBook.Genre = comboGenre.Text;
            newBook.Subject = txtSubject.Text;

            if (string.IsNullOrWhiteSpace(txtCopies.Text))
            {
                newBook.Copies = null;
            }
            else if (int.TryParse(txtCopies.Text, out int copies))
            {
                newBook.Copies = copies;
            }
            else
            {
                MessageBox.Show("Please enter a valid number of copies.");
                return;
            }

            //newBook.Copies = Int32.Parse(txtCopies.Text);
            newBook.Location = txtLocation.Text;
            newBook.KeyWords = txtKeyWords.Text;
            newBook.CallNum = txtCallNum.Text;

            if (dpDate.SelectedDate.HasValue)
            {
                newBook.LastRead = dpDate.SelectedDate.Value;
            }
            else
            {
                newBook.LastRead = null;
            }

            if (rbNo.IsChecked == true)
            {
                newBook.LoanedOut = "No";

            }

            else if (rbYes.IsChecked == true)
            {
                newBook.LoanedOut = "Yes";
                txtLoanInfo.IsEnabled = true;
            }

            newBook.LoanInfo = txtLoanInfo.Text;

            //try
            //{
            //    bookRepository.Add(newBook);

            //    MessageBox.Show("New Book has been added.");
            //}
            //catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            //{
            //    // if title already exists.  
            //    if (ex.InnerException is System.Data.SqlClient.SqlException sqlException && sqlException.Number == 2627)
            //    {
            //        MessageBox.Show("Title already in use.");
            //    }
            //    else
            //    {
            //        MessageBox.Show("An error occurred: " + ex.Message);
            //    }
            //}


        }

        private void btnFindTitle_Click(object sender, RoutedEventArgs e)
        {

            var bookfind = bookRepository.GetTitle(txtTitle.Text);

            if (bookfind.Any())
            {
                // Display information from the first found book in the text boxes
                var firstBook = bookfind.First();

                // Update text boxes with book information
                txtISBN10.Text = firstBook.ISBN10;
                txtISBN13.Text = firstBook.ISBN13;
                txtTitle.Text = firstBook.Title;
                txtEdition.Text = firstBook.Edition.ToString();  // integer value
                txtAuthor.Text = firstBook.Author;
                comboGenre.SelectedItem = firstBook.Genre;
                txtSubject.Text = firstBook.Subject;
                txtKeyWords.Text = firstBook.KeyWords;
                txtCallNum.Text = firstBook.CallNum;
                txtLocation.Text = firstBook.Location;
                txtCopies.Text = firstBook.Copies.ToString();  // integer value
                dpDate.SelectedDate = firstBook.LastRead;
                txtLoanInfo.Text = firstBook.LoanInfo;

                if (firstBook.LoanedOut == "Yes")
                {
                    rbYes.IsChecked = true;
                }

                else if (firstBook.LoanedOut == "No")
                {
                    rbNo.IsChecked = true;  
                }

                btnUpdateBook.IsEnabled = true;
                btnDelete.IsEnabled = true; 
            }

            else
            {
                // Display a message if no books are found with the specified title
                MessageBox.Show($"No books found with the title: {txtTitle.Text}");
            }
        }

        private void btnFindISBN10_Click(object sender, RoutedEventArgs e)
        {
            var bookfind = bookRepository.GetTitle(txtISBN10.Text);

            if (bookfind.Any())
            {
                // Display information from the first found book in the text boxes
                var firstBook = bookfind.First();

                // Update text boxes with book information
                txtISBN10.Text = firstBook.ISBN10;
                txtISBN13.Text = firstBook.ISBN13;
                txtTitle.Text = firstBook.Title;
                txtEdition.Text = firstBook.Edition.ToString();  // Assuming Edition is an int
                txtAuthor.Text = firstBook.Author;
                comboGenre.SelectedItem = firstBook.Genre;
                txtSubject.Text = firstBook.Subject;
                txtKeyWords.Text = firstBook.KeyWords;
                txtCallNum.Text = firstBook.CallNum;
                txtLocation.Text = firstBook.Location;
                txtCopies.Text = firstBook.Copies.ToString();
                dpDate.SelectedDate = firstBook.LastRead;
                txtLoanInfo.Text = firstBook.LoanInfo;

                if (firstBook.LoanedOut == "Yes")
                {
                    rbYes.IsChecked = true;
                }

                else if (firstBook.LoanedOut == "No")
                {
                    rbNo.IsChecked = true;
                }

                btnUpdateBook.IsEnabled = true;
                btnDelete.IsEnabled = true; 
            }

            else
            {
                // Display a message if no books are found with the specified title
                MessageBox.Show($"No books found with the ISBN10: {txtISBN10.Text}");
            }
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            // make sure title is not blank
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title.");
                return;
            }

            var title = txtTitle.Text;
            var bookupdate = new DadLibrary();
            bookupdate.ISBN10 = txtISBN10.Text;
            bookupdate.ISBN13 = txtISBN13.Text;
            bookupdate.Title = txtTitle.Text;

            if (string.IsNullOrWhiteSpace(txtEdition.Text))
            {
                bookupdate.Edition = null;
            }
            else if (int.TryParse(txtEdition.Text, out int edition))
            {
                bookupdate.Edition = edition;
            }
            else
            {
                MessageBox.Show("Please enter a valid edition number.");
                return;
            }

            bookupdate.Author = txtAuthor.Text;
            bookupdate.Genre = comboGenre.Text;
            bookupdate.Subject = txtSubject.Text;


            if (string.IsNullOrWhiteSpace(txtCopies.Text))
            {
                bookupdate.Copies = null;
            }
            else if (int.TryParse(txtCopies.Text, out int copies))
            {
                bookupdate.Copies = copies;
            }
            else
            {
                MessageBox.Show("Please enter valid number of copies.");
                return;
            }

            bookupdate.Location = txtLocation.Text;
            bookupdate.KeyWords = txtKeyWords.Text;
            bookupdate.CallNum = txtCallNum.Text;

            if (dpDate.SelectedDate.HasValue)
            {
                bookupdate.LastRead = dpDate.SelectedDate.Value;
            }
            else
            {
                bookupdate.LastRead = null;
            }

            if (rbNo.IsChecked == true)
            {
                bookupdate.LoanedOut = "No";

            }

            else if (rbYes.IsChecked == true)
            {
                bookupdate.LoanedOut = "Yes";
            }

            bookupdate.LoanInfo = txtLoanInfo.Text;

            bookRepository.Update(title, bookupdate);
            
            MessageBox.Show("Book has been updated.");  

        }
              
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var title = txtTitle.Text;
            var bookdelete = bookRepository.GetTitle((string)txtTitle.Text).ToList();

            if (bookdelete.Any())
            {
                var booktodelete = bookdelete.First(); 
                bookRepository.Delete(booktodelete);
                MessageBox.Show("Book has been deleted.");
            }

            else
            {
                MessageBox.Show("Book not found."); 
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            this.Hide();
        }

        private void ClearTextBoxes()
        {
            // Add code to clear text boxes
            txtISBN10.Clear();
            txtISBN13.Clear();
            txtTitle.Clear();
            txtEdition.Clear();
            txtAuthor.Clear();
            comboGenre.SelectedItem = null;
            txtSubject.Clear();
            txtKeyWords.Clear();
            txtLocation.Clear();
            txtCopies.Clear();
            dpDate.SelectedDate = null;
            txtLoanInfo.Clear();

            // Reset radio buttons
            rbYes.IsChecked = false;
            rbNo.IsChecked = true;  // book most likely not loaned out
        }

        //populate the textboxes based upon the library search window
        private void PopulatefromLibrarySearchWindow()
        {
            txtISBN10.Text = SelectedBook.ISBN10;
            txtISBN13.Text = SelectedBook.ISBN13;
            txtTitle.Text = SelectedBook.Title;
            txtEdition.Text = SelectedBook.Edition.ToString();
            txtAuthor.Text = SelectedBook.Author;
            comboGenre.SelectedItem = SelectedBook.Genre;
            txtSubject.Text = SelectedBook.Subject;
            txtKeyWords.Text = SelectedBook.KeyWords;
            txtCallNum.Text = SelectedBook.CallNum;
            txtLocation.Text = SelectedBook.Location;
            txtCopies.Text = SelectedBook.Copies.ToString();
            dpDate.SelectedDate = SelectedBook.LastRead;
            txtLoanInfo.Text = SelectedBook.LoanInfo;

            if (SelectedBook.LoanedOut == "Yes")
            {
                rbYes.IsChecked = true;
            }
            else if (SelectedBook.LoanedOut == "No")
            {
                rbNo.IsChecked = true;
            }

        }
    }
}
   