using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaCoupsSale
{
    class ChildrenPassengerClass : PassengerClass
        {
            public ChildrenPassengerClass() : base("Ребёнок")
            { }
            public override double GetCost()
            {
                return 0.7;
            }
        }    
}
