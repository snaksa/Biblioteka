using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class BookReader
    {
        public string ReaderEGN { get; set; }
        public string ReaderName { get; set; }
        public string ClassAndParalelka { get; set; }
        public DateTime DateOfTaking { get; set; }
        public DateTime DateOfReturn { get; set; }

        public BookReader(string egn, string name, string classAndParalelka, DateTime dateOfTaking, DateTime dateOfReturn)
        {
            this.ReaderEGN = egn;
            this.ReaderName = name;
            this.ClassAndParalelka = classAndParalelka;
            this.DateOfTaking = dateOfTaking;
            this.DateOfReturn = dateOfReturn;
        }
    }
}
