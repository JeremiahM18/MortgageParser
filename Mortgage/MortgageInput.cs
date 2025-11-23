using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Mortgage
{
    // Holds validated values from the parser
    public sealed class MortgageInput
    {
        public double Price { get; }
        public double DownPercent { get; }
        public double RatePercent { get; }
        public int TermYears { get; }

        public MortgageInput(double price, double downPercent, double ratePercent, int termYears)
        {
            Price = price;
            DownPercent = downPercent;
            RatePercent = ratePercent;
            TermYears = termYears;
        }
    }
}
