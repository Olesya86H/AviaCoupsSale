using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaCoupsSale
{
    class AdultPassengerClass : PassengerClass
    {        
        public AdultPassengerClass() : base("Взрослые")
        { }
        public override double GetCost()
        {
            return 1;
        }
    }
}
