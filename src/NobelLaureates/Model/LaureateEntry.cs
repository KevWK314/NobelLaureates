using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NobelLaureates.Model
{
    public class LaureateEntry
    {
        public Laureate Laureate{ get; set; }

        public NobelPrize Prize { get; set; }
        
        public string Motivation { get; set; }

        public string PrizeShare { get; set; }
    }
}
