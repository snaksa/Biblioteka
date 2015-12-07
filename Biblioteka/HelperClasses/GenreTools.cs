using Biblioteka.Data;
using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.HelperClasses
{
    static public class GenreTools
    {
        static private List<Genre> allGenres = DataPersister.GetAllGenres();

        static public List<Genre> GetAllGenres
        {
            get
            {
                return DataPersister.GetAllGenres();
            }
        }

        static public string TextFromNumber(int n)
        {
            allGenres = DataPersister.GetAllGenres();
            foreach (var item in allGenres)
            {
                if (item.Index == n) return item.GenreText;
            }
            return "err";
        }
        static public int NumberFromText(string text)
        {
            allGenres = DataPersister.GetAllGenres();
            foreach (var item in allGenres)
            {
                if (item.GenreText.ToUpper().Equals(text.ToUpper())) return item.Index;
            }
            return 0;
        }
        static public List<string> GetOnlyGenreText()
        {
            allGenres = DataPersister.GetAllGenres();
            List<string> genres = new List<string>();
            foreach (var genre in allGenres)
            {
                genres.Add(genre.GenreText);
            }
            return genres;
        }
    }
}
