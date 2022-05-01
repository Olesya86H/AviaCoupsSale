using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaCoupsSale
{
    abstract class PassengerDecorator : PassengerClass
    {
        protected PassengerClass passenger;
        public PassengerDecorator(string n, PassengerClass passenger) : base(n)
        {
            this.passenger = passenger;
        }
    }
}
