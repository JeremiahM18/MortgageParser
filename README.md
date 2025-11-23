# MortgageParser

A custom mortgage command parser and calculator built in C#.
It reads structured user input, validates it with a compiler-style architecture(lexer, and parser)
and computes a full mortgage cost summary including monthly payments, total interest paid, and loan ammount.


## Features
- **Lexer**
 Converts raw input strings into meaningful tokens for parsing.

 - **Parser**
 Validates the token sequence against defined grammar rules to ensure correct syntax.

 - **Calculator**
 Uses financial formulas to compute:
	- Monthly payment amounts
	- Total paid
	- Total interest over the life of the loan
	- Down payment breakdown
	- Loan amount

- **Error Handling**
 Provides clear error messages for invalid input or syntax errors.

## Project Structure
- `Lexer/` - Contains the lexer implementation for tokenizing input.
	- Token.cs - Defines token types and structures.
	- TokenType.cs - Enumerates possible token types.
	- Lexer.cs - Implements the lexer logic.
- `Parser/` - Contains the parser implementation for validating token sequences
	- Parser.cs - Implements the parser logic.
- `Mortgage/` - Contains the mortgage calculation logic.
	- MortgageCalculator.cs - Implements mortgage calculation methods.
	- MortgageInput.cs - Defines the structure for mortgage input data.
- `Errors/` - Contains error handling classes.
	- SyntaxErrorException.cs - Defines syntax error structures.
	- ValueErrorException.cs - Defines value error structures.
- `Program.cs` - Entry point for the application.

## Author

**Jeremiah McDonald**
Senior Computer Science Student at Belmont University
