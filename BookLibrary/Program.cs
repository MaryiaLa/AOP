using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace BookLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService;

            var generator = new ProxyGenerator();
            bookService = generator.CreateInterfaceProxyWithTarget<IBookService>(
                new BookService(), new Logger());

            int option;

            do
            {
                Console.Write("\n");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Get book list");
                Console.WriteLine("3. Delete book by id");
                Console.WriteLine("4. Exit");
                Console.Write("\n" + "Please enter command: ");

                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out option) && option > 0 && option < 5)
                {
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter name:");
                            string bookName = Console.ReadLine();
                            Console.WriteLine("Enter author:");
                            string bookAuthor = Console.ReadLine();
                            Book book = new Book(bookName, bookAuthor);
                            bookService.AddBook(book);
                            break;
                        case 2:
                            var books = bookService.GetBooks();
                            foreach (var item in books)
                            {
                                Console.WriteLine("Id is " + item.Id + " author is " + item.Author + " name is " + item.Name);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter book id:");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out var bookId))
                            {
                                bookService.DeleteBook(bookId);
                            }
                            else
                            {
                                Console.WriteLine("The id is invalid");
                            }
                            break;
                        case 4:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("The command is invalid");
                }
            } while (option != 4);

            Console.ReadLine();
        }
    }
}
