using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaCoupsSale
{
    abstract class PassengerClass
    {
        public PassengerClass(string n)
            {
                this.AgeCategory = n;
            }
            public string AgeCategory { get; protected set; }
            public abstract double GetCost();  
    }
}
