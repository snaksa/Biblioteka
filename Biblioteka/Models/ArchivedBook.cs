using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class ArchivedBook : Book
    {
        public string ArchiveNumber { get; set; }
        public DateTime ArchivedDate { get; set; }

        public ArchivedBook(string serialNumber, string author, string title, string price, string publishedYear, int genre, string archiveNumber, DateTime date)
            : base(serialNumber, author, title, price, publishedYear, genre)
        {
            this.ArchiveNumber = archiveNumber;
            this.ArchivedDate = date;
        }
    }
}
