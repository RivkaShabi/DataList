using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_list.Models
{
    public class DataPerson
    {
        public string areaAddres { get; set; }
        public string nameAddres { get; set; }
        public string cityAddres { get; set; }
        public DataPerson(string area, string name, string city)
        {
            areaAddres = area;
            nameAddres = name;
            cityAddres = city;
        }
    
    }
 }
