using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace _153503_КахновскийЕС_Domain
{
    public class Library
    {
        private List<Book> _books = new List<Book>();
        public List<Book> Books
        {
            get { return _books; }
            set { _books = value; }
        }
        public Library() { }
        public Library(List<Book> books)
        {
            this.Books = Books;
        }
    }
}
