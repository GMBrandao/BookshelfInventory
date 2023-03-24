using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfInventory
{
    internal class Book
    {
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public long Isbn { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public bool Lent { get; set; }

        public Book(string title, string edition, string author, string description, long isbn, int pages) 
        {
            this.Title = title;
            this.Edition = edition;
            this.Author = author;
            this.Description = description;
            this.Isbn = isbn;
            this.NumberOfPages = pages;
            this.CurrentPage = 0;
            this.Lent = false;
        }

        public Book(string title, string edition, string author, string description, long isbn, int pages, int cp, bool l)
        {
            this.Title = title;
            this.Edition = edition;
            this.Author = author;
            this.Description = description;
            this.Isbn = isbn;
            this.NumberOfPages = pages;
            this.CurrentPage = cp;
            this.Lent = l;
        }

        public override string ToString()
        {
            return $"{this.Title}|{this.Edition}|{this.Author}|{this.Description}|{this.Isbn}|{this.NumberOfPages}|{this.CurrentPage}|{this.Lent}";
        }
    }
}
