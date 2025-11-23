using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MortgageParser_JeremiahMcDonald.Lexer
{
    public sealed class Lexer
    {
        private readonly string _input;
        private int _pos;
        public Lexer(string input)
        {
            _input = input;
            _pos = 0;
        }

        // Public API: returns a list of all tokens
        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (!IsAtEnd())
            {
                SkipWhitespace();

                if (IsAtEnd()) break;

                char c = Peek();

                // Keywords
                if (MatchKeyword("price"))
                {
                    tokens.Add(MakeToken(TokenType.PRICE, "price"));
                    continue;
                }
                if (MatchKeyword("down"))
                {
                    tokens.Add(MakeToken(TokenType.DOWN, "down"));
                    continue;
                }
                if (MatchKeyword("rate"))
                {
                    tokens.Add(MakeToken(TokenType.RATE, "rate"));
                    continue;
                }
                if (MatchKeyword("term"))
                {
                    tokens.Add(MakeToken(TokenType.TERM, "term"));
                    continue;
                }

                // Numbers (integer of decimal)
                if (char.IsDigit(c))
                {
                    tokens.Add(LexNumber());
                    continue;
                }

                // Symbols
                if (c == ',')
                {
                    Advance();
                    tokens.Add(new Token(TokenType.COMMA, ",", _pos - 1));
                    continue;
                }

                if (c == '%')
                {
                    Advance();
                    tokens.Add(new Token(TokenType.PERCENT, "%", _pos - 1));
                    continue;
                }

                throw new Exception($"Unexpected character '{c}' at position {_pos}. ");
            }

            tokens.Add(new Token(TokenType.EOF, string.Empty, _pos));
            return tokens;
        }

        // Helper methods

        private Token LexNumber()
        {
            int start = _pos;

            while (!IsAtEnd() && char.IsDigit(Peek()))
            {
                Advance();
            }

            // decimal number
            if (!IsAtEnd() && Peek() == '.')
            {
                Advance(); // consume '.'

                if (IsAtEnd() || !char.IsDigit(Peek()))
                {
                    throw new Exception($"Invalid number format at " +
                        $"position {_pos}. Expected digits after decimal point.");
                }

                while (!IsAtEnd() && char.IsDigit(Peek()))
                {
                    Advance();
                }
            }

            string number = _input.Substring(start, _pos - start);
            return new Token(TokenType.NUMBER, number, start);
        }

        private bool MatchKeyword(string keyword)
        {
            int remaining = _input.Length - _pos;
            if (remaining < keyword.Length)
            {
                return false;
            }

            string slice = _input.Substring(_pos, keyword.Length);

            if (slice.Equals(keyword, StringComparison.OrdinalIgnoreCase))
            {
                // Ensure it's not part of a longer identifier
                if (_pos + keyword.Length < _input.Length &&
                    char.IsLetterOrDigit(_input[_pos + keyword.Length]))
                {
                    return false;
                }

                _pos += keyword.Length;
                return true;
            }
            return false;
        }

        private bool IsAtEnd()
        {
            return _pos >= _input.Length;
        }

        private char Peek()
        {
            return _input[_pos];
        }

        private void Advance()
        {
            _pos++;
        }

        private void SkipWhitespace()
        {
            while (!IsAtEnd() && char.IsWhiteSpace(Peek()))
            {
                Advance();
            }
        }

        private Token MakeToken(TokenType type, string lexeme)
        {
            return new Token(type, lexeme, _pos - lexeme.Length);
        }
    }
}
