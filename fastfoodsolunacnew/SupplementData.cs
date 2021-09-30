using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fastfoodsolunacnew
{
    public class SupplementData
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Priority { get; set; }
        public string skracenicaZaRacun { get; set; }

        public SupplementData()
        {

        }

        public SupplementData(string name,string shortname,string priority,string skracenicazaracun)
        {
            Name = name;
            ShortName = shortname;
            Priority = priority;
            skracenicaZaRacun = skracenicazaracun;
        }
    }
}
