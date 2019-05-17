using System.Collections;
using System.Collections.Generic;

namespace BookLibrary
{
    public interface IBookService
    {
        void AddBook(Book book);
        void DeleteBook(int id);
        IList<Book> GetBooks();
    }
}