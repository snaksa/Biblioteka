using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
    static public class DataValidator
    {
        static public bool IsValidString(string data)
        {
            if (data == null || data == String.Empty) return false;
            else return true;
        }

        static public bool isValidInteger(string number)
        {
            int n;
            bool chec = Int32.TryParse(number, out n);
            if (!chec) return false;
            return true;
        }

        static public bool IsValidDouble(string number)
        {
            double n;
            bool chec = Double.TryParse(number, out n);
            if (!chec) return false;
            return true;
        }
    }
}
