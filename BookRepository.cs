using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DadLibrarySystem
{
    // CRUD operations 
    interface CRUD
    {
        void Add(DadLibrary newBook); // create - add a new book to the library
        void Delete(DadLibrary book); // delete - remove book from library (probably never needed since not books leave)
        ICollection<DadLibrary> GetTitle(string title);  // Read by title
        ICollection<DadLibrary> GetISBN10(string isbn10);  // Read by ISBN10
        void Update(string title, DadLibrary book);  // Update by finding title
        void UpdatebyTitle(string title, DadLibrary book); // Update by finding title

        // DadLibrary GetBook(string ISBN )  // find the book - determine how to do searches

    }

    internal class BookRepository : CRUD
    {
        // link to database
        public static DadLibrarydbEntities1 Entities = new DadLibrarydbEntities1();


        public void Add(DadLibrary newBook)  // adding a new book (create)
        {
            Entities.DadLibraries.Add(newBook);
            Entities.SaveChanges();
        }

        public void Delete(DadLibrary book)  // deleting (delete)
        {
            Entities.DadLibraries.Remove(book);
            Entities.SaveChanges();
        }

        public ICollection<DadLibrary> GetISBN10(string isbn10)
        {
            return Entities.DadLibraries.Where(book => book.ISBN10 == isbn10).ToList();
        }

        public ICollection<DadLibrary> GetTitle(string title)
        {
            return Entities.DadLibraries.Where(book => book.Title.Contains(title)).ToList();
        }

        public void Update(string title, DadLibrary book)  // update by ISBN10
        {
            var bookupdate = Entities.DadLibraries.Find(title);
            if (bookupdate != null)
            {
                bookupdate.ISBN10 = book.ISBN10;
                bookupdate.ISBN13 = book.ISBN13;
                bookupdate.Title = book.Title;
                bookupdate.Edition = book.Edition;
                bookupdate.Author = book.Author;
                bookupdate.Genre = book.Genre;
                bookupdate.Subject = book.Subject;
                bookupdate.Copies = book.Copies; 
                bookupdate.CallNum = book.CallNum;
                bookupdate.Location = book.Location;
                bookupdate.LastRead = book.LastRead;
                bookupdate.LoanedOut = book.LoanedOut;
                bookupdate.LoanInfo = book.LoanInfo; 

                Entities.SaveChanges();
            }

            else
            {
                MessageBox.Show("Book not found.");

            }
        }

        public void UpdatebyTitle(string title, DadLibrary book) // update by title
        {
            var bookupdate = Entities.DadLibraries.FirstOrDefault(b => b.Title == title);
            if (bookupdate != null)
            {
                bookupdate.ISBN10 = book.ISBN10;
                bookupdate.ISBN13 = book.ISBN13;
                bookupdate.Title = book.Title;
                bookupdate.Edition = book.Edition;
                bookupdate.Author = book.Author;
                bookupdate.Genre = book.Genre;
                bookupdate.Subject = book.Subject;
                bookupdate.Copies = book.Copies;
                bookupdate.CallNum = book.CallNum;
                bookupdate.Location = book.Location;
                bookupdate.LastRead = book.LastRead;
                bookupdate.LoanedOut = book.LoanedOut;
                bookupdate.LoanInfo = book.LoanInfo;

                Entities.SaveChanges();
            }
            else
            {
                MessageBox.Show($"{title} not found.");
            }
        }
    }
}
