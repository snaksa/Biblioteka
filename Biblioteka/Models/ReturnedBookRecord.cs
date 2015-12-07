using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class ReturnedBookRecord : Book
    {
        private bool onTime;
        public int ID { get; set; }
        public string ReaderEGN { get; set; }
        public DateTime DateOfTaking { get; set; }
        public DateTime DateOfReturn { get; set; }
        public string ReturnedOnTime
        {
            get
            {
                if (this.onTime == true) return "Да";
                return "Не";
            }
        }

        public ReturnedBookRecord(int id, string egn, string serial, DateTime d1, DateTime d2, bool returnedOnTime)
            : this(id, egn, serial, d1, d2, returnedOnTime, "", "", "", 0) { }

        public ReturnedBookRecord(int id, string egn, string serial, DateTime d1, DateTime d2, bool returnedOnTime, string author, string title, string price, int genre)
            : base(serial, author, title, price, "", genre)
        {
            this.ID = id;
            this.ReaderEGN = egn;
            this.DateOfTaking = d1;
            this.DateOfReturn = d2;
            this.onTime = returnedOnTime;
        }
    }
}
