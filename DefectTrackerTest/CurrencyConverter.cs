using System;
using CookBook.Recipes.Core.DefectTracker.Attributes;

namespace DefectTrackerTest
{
    public class CurrencyConverter
    {
        private double _value;

        [DefectTrackerAttribute(1042, "AP", "2012/02/11", "Changed float param to double")]
        public CurrencyConverter (double value)
        {
            _value = value;
        }

        [DefectTrackerAttribute(DefectID = 1042, ResolvedBy = "AP", ResolvedOn = "2012/02/11")]
        public double ToRupee ()
        {
            return _value * 50;
        }
    }
}

