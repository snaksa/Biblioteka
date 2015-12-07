using Biblioteka.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Student
    {
        private string egn;
        private string name;
        private int classNo;
        private int paralelka;
        private string address;

        public Student(string egn, string name, int classNo, int paralelka, string address)
        {
            this.egn = egn;
            this.name = name;
            this.classNo = classNo;
            this.paralelka = paralelka;
            this.address = address;
        }

        public string EGN
        {
            get
            {
                return this.egn;
            }
            set
            {
                this.egn = value;

            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public int ClassNo
        {
            get
            {
                return this.classNo;
            }
            set
            {
                this.classNo = value;
            }
        }
        public int Paralelka
        {
            get
            {
                return this.paralelka;
            }
            set
            {
                this.paralelka = value;
            }
        }
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }
        public string ClassAndParalelka
        {
            get
            {
                return this.classNo.ToString() + ParalelkiTools.CharFromNumber(this.paralelka);
            }
        }
    }
}
