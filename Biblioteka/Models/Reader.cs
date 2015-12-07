using Biblioteka.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Reader
    {
        public string EGN { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SerialNumber { get; set; }
        public int ClasNo { get; set; }
        public int Paralelka { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string ClassAndParalelka
        {
            get
            {
                if (this.ClasNo == 0) return "Учител";
                return this.ClasNo + ParalelkiTools.CharFromNumber(this.Paralelka);
            }
        }

        public Reader(string egn, string name, string serial, int classNo, int paral)
            : this(egn, name, "", serial, classNo, paral, DateTime.Now)
        {

        }
        public Reader(string egn, string name, string address, string serialNumber, int classNo, int paral, DateTime dateOfCreation)
        {
            this.EGN = egn;
            this.Name = name;
            this.Address = address;
            this.SerialNumber = serialNumber;
            this.ClasNo = classNo;
            this.Paralelka = paral;
            this.DateOfCreation = dateOfCreation;
        }
    }
}
