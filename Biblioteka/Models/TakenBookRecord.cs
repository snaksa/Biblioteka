using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class TakenBookRecord : Book
    {
        int days;
        public int ID { get; set; }
        public string ReaderEGN { get; set; }
        public string ReaderName { get; set; }
        public string ClassAndParalelka { get; set; }
        public DateTime DateOfTaking { get; set; }
        public DateTime Deadline { get; set; }

        public int DaysToDeadine
        {
            get
            {
                return (DateTime.Today - this.Deadline).Days;
            }
        }
        public string DaysToDeadlineString
        {
            get
            {
                if (DaysToDeadine > 0) return "Просрочена";
                else if (DaysToDeadine == -1) return "1 ден";
                else if (DaysToDeadine == 0) return "Днес";
                else return Math.Abs(DaysToDeadine) + " дни"; 
            }
        }

        public TakenBookRecord(int id, string egn, string serial, DateTime d1, DateTime d2)
            : this(0, egn, serial, d1, d2, "", "", "", 0, "", "") { }

        public TakenBookRecord(int id, string egn, string serial, DateTime d1, DateTime d2, string author, string title, string price, int genre, string readerName, string classAndParalelka)
            : base(serial, author, title, price, "", 0)
        {
            this.ID = id;
            this.ReaderEGN = egn;
            this.DateOfTaking = d1;
            this.Deadline = d2;
            this.ReaderName = readerName;
            this.ClassAndParalelka = classAndParalelka;
        }
    }
}
