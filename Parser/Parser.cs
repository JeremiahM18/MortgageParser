using System;
using System.Collections.Generic;
using System.Text;
using MortgageParser_JeremiahMcDonald.Errors;
using MortgageParser_JeremiahMcDonald.Lexer;
using MortgageParser_JeremiahMcDonald.Mortgage;

namespace MortgageParser_JeremiahMcDonald.Parser
{
    public sealed class Parser
    {
        private readonly List<Token> _tokens;
        private int _current;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _current = 0;
        }

        // Entry point for parsing tokens into a Mortgage object
        public MortgageInput ParseMortgage()
        {
            // Grammer
            // price NUMBER, down NUMBER %, rate NUMBER %, term NUMBER
            Consume(TokenType.PRICE, "Expected keyword 'price' at start.");
            double price = ConsumeNumber("Expected home price after 'price'.");

            ConsumeOptionalComma();

            Consume(TokenType.DOWN, "Expected keyword 'down' after price.");
            double down = ConsumeNumber("Expected down payment percentage after 'down'.");
            Consume(TokenType.PERCENT, "Expected '%' after down payment percentage.");

            ConsumeOptionalComma();

            Consume(TokenType.RATE, "Expected keyword 'rate' after down payment.");
            double rate = ConsumeNumber("Expected interest rate percentage after 'rate'.");
            Match(TokenType.PERCENT); // Interest rate '%' is optional

            ConsumeOptionalComma();

            Consume(TokenType.TERM, "Expected keyword 'term' after interest rate.");
            int term = ConsumeInt("Expected term in years after 'term'.");

            //Consume(TokenType.EOF, "Expected end of input after term.");
            SkipTrailingCommas();
            Consume(TokenType.EOF, "Expected end of input after term.");


            // Value verification
            if (price <= 0)
                throw new ValueErrorException("Home price must be greater than zero.");

            if(down < 0 || down >= 100)
                throw new ValueErrorException("Down payment percentage must be between 0 and 100 (exclusive).");

            if(rate <= 0 || rate >= 100)
                throw new ValueErrorException("Interest rate percentage must be between 0 and 100 (exclusive).");

            if(term <= 0)
                throw new ValueErrorException("Term in years must be greater than zero.");

            return new MortgageInput(price, down, rate, term);
        }

        // Helpers
        private void ConsumeOptionalComma()
        {
            Match(TokenType.COMMA);
        }

        private double ConsumeNumber(string errorMessage)
        {
            Token t = Consume(TokenType.NUMBER, errorMessage);
            if (!double.TryParse(t.Lexeme, out double value))
                throw new SyntaxErrorException($"Invalid number '{t.Lexeme}' at position {t.Position}.");
            return value;
        }

        private int ConsumeInt(string errorMessage)
        {
            Token t = Consume(TokenType.NUMBER, errorMessage);
            if (!int.TryParse(t.Lexeme, out int value))
                throw new ValueErrorException($"Term must be a whole number. Got '{t.Lexeme}'.");
            return value;
        }

        private Token Consume(TokenType type, string errorMessage)
        {
            if (Check(type))
                return Advance();
            throw new SyntaxErrorException(errorMessage + $" Found token {Peek().Type} at position {Peek().Position}.");
        }

        private bool Match(TokenType type)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
            return false;
        }

        private bool Check(TokenType type)
        {
            if (_current >= _tokens.Count)
            {
                return false;
            }
            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _current++;
            return Previous();
        }

        private bool IsAtEnd()
        {
            return _current >= _tokens.Count || Peek().Type == TokenType.EOF;
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private Token Previous()
        {
            return _tokens[_current - 1];
        }

        private void SkipTrailingCommas()
        {
            while (Match(TokenType.COMMA)) { }
        }
    }
}
