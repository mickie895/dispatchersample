using System;
using System.Collections.Generic;
using System.Text;

namespace BookShelfSample.Model
{
    public class BookInfo
    {
        public Book book;
        public bool hasRead;

        public string BookName => book.Name;

        public BookInfo(string name, bool read=false)
        {
            book = new Book(name);
            hasRead = read;
        }
    }

    public class Book
    {
        public string Name;
        public Book(string name)
        {
            Name = name;
        }
    }
}
