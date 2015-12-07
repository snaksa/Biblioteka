using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Paralelka
    {
        public int Index { get; set; }
        public string Character { get; set; }

        public Paralelka(int index, string character)
        {
            this.Index = index;
            this.Character = character;
        }
    }
}
