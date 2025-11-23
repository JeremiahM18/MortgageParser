using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Mortgage
{
    // Holds validated values from the parser
    public sealed class MortgageInput
    {
        public double Price { get; }
        public double DownPayment { get; }
        public double RatePercent { get; }
        public int TermYears { get; }

        public MortgageInput(double price, double downPayment, double ratePercent, int termYears)
        {
            Price = price;
            DownPayment = downPayment;
            RatePercent = ratePercent;
            TermYears = termYears;
        }
    }
}
