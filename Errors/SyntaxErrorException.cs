using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Errors
{
    public sealed class SyntaxErrorException : Exception
    {
        public SyntaxErrorException(string message) : base(message)
        {
        }
    }
}
