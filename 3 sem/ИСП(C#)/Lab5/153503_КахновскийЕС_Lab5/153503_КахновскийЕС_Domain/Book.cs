using System;

namespace _153503_КахновскийЕС_Domain
{
    public class Book
    {
        public string Name { get; set; }
        public int YearOfPublication { get; set; }
        public bool Availability { get; set; }
        public Book() { }
        public Book(string Name = "Unknown", int YearOfPublication = 0, bool Availability = true)
        {
            this.Name = Name;
            this.YearOfPublication = YearOfPublication;
            this.Availability = Availability;
        }
    }
}

