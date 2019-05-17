using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookLibrary
{
    public class BookService:IBookService
    {
        private readonly IList<Book> BookList;

        public BookService()
        {
            BookList = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            BookList.Add(book);
        }

        public void DeleteBook(int id)
        {
            var book = BookList.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }

            BookList.Remove(book);
        }

        public IList<Book> GetBooks()
        {
            return BookList;
        }
    }
}