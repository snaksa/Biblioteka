using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biblioteka.Models
{
    public class StatsReader : Reader
    {
        private int numberOfBooks;
        private int position;
        public StatsReader(string egn, string name, string serial, int classNo, int par, int books)
            : base(egn, name, serial, classNo, par)
        {
            this.numberOfBooks = books;
        }

        public int NumberOfBooks
        {
            get
            {
                return this.numberOfBooks;
            }
            set
            {
                this.numberOfBooks = value;
            }
        }
        public int Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
    }
}
