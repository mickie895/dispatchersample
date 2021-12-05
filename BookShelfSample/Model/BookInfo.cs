using System;
using System.Collections.Generic;
using System.Text;

namespace BookShelfSample.Model
{
    class BookInfo
    {
        public Book book;
        public bool hasRead;
    }

    class Book
    {
        public string Name;
        public Book(string name)
        {
            Name = name;
        }
    }
}
