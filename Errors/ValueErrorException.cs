using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Errors
{
    public sealed class ValueErrorException : Exception
    {
        public ValueErrorException(string message) : base(message)
        {
        }
    }
}
