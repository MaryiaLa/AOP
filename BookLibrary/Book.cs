using System;

namespace BookLibrary
{
    public class Book
    {
        public Book(string name, string author)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentNullException(nameof(author));
            }

            Author = author;
            Name = name;

            var random = new Random();
            int id = random.Next(1, 1000);
            Id = id;
        }

        public string Author { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}