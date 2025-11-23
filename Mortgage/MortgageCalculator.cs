using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Mortgage
{
    public sealed class MortgageCalculator
    {
        private readonly MortgageInput _input;

        public MortgageCalculator(MortgageInput input)
        {
            _input = input;
        }

        public double DownPaymentAmount()
        {
            return _input.Price * (_input.DownPercent / 100.0);
        }

        public double LoanAmount()
        {
            return _input.Price - DownPaymentAmount();
        }

        // Monthly interest rate (APR / 12)
        public double MonthlyInterestRate()
        {
            return (_input.RatePercent / 100.0) / 12.0;
        }

        // Number of monthly payments (term in years * 12)
        public int NumberOfPayments()
        {
            return _input.TermYears * 12;
        }

        // Standard mortgage payment formula
        // M = P * r(1+r)^n / ((1+r)^n – 1)
        public double MonthlyPayment()
        {
            double P = LoanAmount();
            double r = MonthlyInterestRate();
            int n = NumberOfPayments();

            if (r == 0) // Edge case for 0% interest
            {
                return P / n;
            }

            double numerator = P * r * Math.Pow(1 + r, n);
            double denominator = Math.Pow(1 + r, n) - 1;

            return numerator / denominator;
        }

        public double TotalPaid()
        {
            return MonthlyPayment() * NumberOfPayments();

        }
        public double TotalInterest()
        {
            return TotalPaid() - LoanAmount();
        }

    }
}
