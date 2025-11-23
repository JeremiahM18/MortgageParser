using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Lexer
{
    public sealed class Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public int Position { get; }
        public Token(TokenType type, string lexeme, int position)
        {
            Type = type;
            Lexeme = lexeme;
            Position = position;
        }
        public override string ToString()
        {
            return $"{Type} ('{Lexeme}') at position {Position}";

        }
    }
}
