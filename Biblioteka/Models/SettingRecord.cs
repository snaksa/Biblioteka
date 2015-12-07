using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biblioteka.Models
{
    public class SettingRecord
    {
        public int ID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        public SettingRecord(int id, string name, string val)
        {
            this.ID = id;
            this.SettingName = name;
            this.SettingValue = val;
        }
    }
}
