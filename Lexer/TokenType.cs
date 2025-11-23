using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Lexer
{
    // All token types the lexer can produce
    public enum TokenType
    {
        // Keywords
        PRICE,
        DOWN,
        RATE,
        TERM,

        // Literals
        NUMBER,
        PERCENT,

        // Symbols
        COMMA,
        EOF
    }
}
