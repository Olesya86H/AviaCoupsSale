using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaCoupsSale
{
    class SubsPassenger : PassengerDecorator
    {
        public SubsPassenger(PassengerClass p)
            : base(p.AgeCategory, p)
        { }

        public override double GetCost()
        {
            return passenger.GetCost() * 0.7;
        }
    }
}
