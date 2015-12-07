using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biblioteka.Models
{
    public class StatsBook : Book
    {
        private int numberOfReaders;
        private int position;

        public StatsBook(string serial, string author, string title, int readers)
            : base(serial, author, title)
        {
            this.numberOfReaders = readers;
        }

        public int NumberOfReaders
        {
            get
            {
                return this.numberOfReaders;
            }
            set
            {
                this.numberOfReaders = value;
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
