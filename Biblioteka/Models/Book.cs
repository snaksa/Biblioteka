using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Book
    {
        string status;
        public string SerialNumber { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string PublishedYear { get; set; }
        public int Genre { get; set; }

        
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public decimal PriceAsDecimal
        {
            get
            {
                return Decimal.Parse(this.Price);
            }
        }

        public Book(string serialNumber)
            : this(serialNumber, "", "", "", "", 0) {}

        public Book (string serialNumber, string author, string title)
            : this(serialNumber, author, title, "", "", 0) {}

        public Book(string serialNumber, string author, string title, string price, string publishedYear, int genre)
        {
            this.SerialNumber = serialNumber;
            this.Author = author;
            this.Title = title;
            this.Price = price;
            this.PublishedYear = publishedYear;
            this.Genre = genre;

            if (DataPersister.takenBooksSerials.Contains(serialNumber))
            {
                this.status = "Взета";
            }
            else
            {
                if (DataPersister.archivedBookSerials.Contains(serialNumber))
                {
                    this.status = "Архивирана";
                }
                else this.status = "Свободна";
            }
        }
    }
}
