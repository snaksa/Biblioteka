using Biblioteka.Data;
using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.HelperClasses
{
    static public class ParalelkiTools
    {
        static private List<Paralelka> allParalelki = DataPersister.GetAllParalelki();

        static public List<Paralelka> GetAllParalelki()
        {
            return allParalelki;
        }

        static public string CharFromNumber(int n)
        {
            foreach (var par in allParalelki)
            {
                if (par.Index == n) return par.Character;
            }
            return "а";
        }

        static public int NumberFromChar(string character)
        {
            foreach (var par in allParalelki)
            {
                if (par.Character.ToUpper().Equals(character.ToUpper())) return par.Index;
            }
            return 0;
        }

        static public List<string> GetOnlyCharacters()
        {
            List<string> allCharacters = new List<string>();
            foreach (var par in allParalelki)
            {
                allCharacters.Add(par.Character);
            }
            return allCharacters;
        }

    }
}
