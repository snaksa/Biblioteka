using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Models
{
    public class Genre
    {
        public int Index { get; set; }
        public string GenreText { get; set; }

        public Genre(int index, string text)
        {
            this.Index = index;
            this.GenreText = text;
        }
    }
}
