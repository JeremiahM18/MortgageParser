using System;
using MortgageParser_JeremiahMcDonald.Lexer;
using MortgageParser_JeremiahMcDonald.Parser;
using MortgageParser_JeremiahMcDonald.Mortgage;
using MortgageParser_JeremiahMcDonald.Errors;


namespace MortgageParser_JeremiahMcDonald
{
    internal class Program
    {
        // Normalizes input into strict syntax for the parser
        static string NormalizeInput(string input)
        {
            input = input.Trim().ToLower();

            // Case 1: user enters four numbers with commas or spaces
            var parts = input.Replace("%", "").Split(new char[] { ' ', ',' }, 
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 4 && double.TryParse(parts[0], out _) 
                && double.TryParse(parts[1], out _) && double.TryParse(parts[2], out _) 
                && int.TryParse(parts[3], out _)) {
                return $"price {parts[0]}, down {parts[1]}%, rate {parts[2]}%, term {parts[3]}";
            }

            // Case 2: user forgot commas but included the keywords
            input = input.Replace(" ", " ");

            // Add missing commas
            input = input.Replace("down", ", down")
                .Replace("rate", ", rate").Replace("term", ", term");

            // Fix double commas
            input = input.Replace(",,", ",");

            // Trim leading comma if it exists
            if (input.StartsWith(",")) input = input.Substring(1).Trim();

            return input;
        }

        static void PrintHelp()
        {
            Console.WriteLine("\n--- How to Use MortgageParser ---");
            Console.WriteLine("You can enter your mortgage information in ANY of these formats:");
            Console.WriteLine();
            Console.WriteLine("  1) Full command with keywords:");
            Console.WriteLine("     price 450000, down 15%, rate 7%, term 30");
            Console.WriteLine();
            Console.WriteLine("  2) Just the numbers:");
            Console.WriteLine("     450000 15 7 30");
            Console.WriteLine();
            Console.WriteLine("  3) Numbers separated by commas:");
            Console.WriteLine("     450000, 15, 7, 30");
            Console.WriteLine();
            Console.WriteLine("  4) Mixed or messy input:");
            Console.WriteLine("     price 450000 down 15 rate 7 term 30");
            Console.WriteLine();
            Console.WriteLine("The program will automatically clean and convert your input.");
            Console.WriteLine("Type 'quit' to exit.");
            Console.WriteLine("--------------------------------------------\n");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("            MortgageParser v1.0");
            Console.WriteLine("============================================");
            Console.WriteLine("Type your mortgage command, 'help' for help, or 'quit' to exit.\n");

            PrintHelp();

            while (true)
            {
                Console.Write("Enter mortgage command:");
                string raw = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(raw))
                {
                    Console.WriteLine("Please enter a command or type 'help'.");
                    continue;
                }

                if (raw.Equals("quit", StringComparison.OrdinalIgnoreCase) || raw.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                if (raw.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    PrintHelp();
                    continue;
                }

                string input = NormalizeInput(raw);

                Console.WriteLine($"\nNormalized Input {input}");

                try
                {
                    // 1. Lexing
                    var lexer = new Lexer.Lexer(input);
                    var tokens = lexer.Tokenize();

                    // 2. Parsing
                    var parser = new Parser.Parser(tokens);
                    MortgageInput mortgage = parser.ParseMortgage();

                    // 3. Calculation
                    var calc = new MortgageCalculator(mortgage);

                    // 4. Output
                    Console.WriteLine("\nMortgage Summary");
                    Console.WriteLine($"Home Price:          ${mortgage.Price:N2}");
                    Console.WriteLine($"Down Payment:        {mortgage.DownPercent}%");
                    Console.WriteLine($"Down Payment Amount: ${calc.DownPaymentAmount():N2}");
                    Console.WriteLine($"Loan Amount:         ${calc.LoanAmount():N2}");
                    Console.WriteLine($"Interest Rate:       {mortgage.RatePercent}%");
                    Console.WriteLine($"Term:                {mortgage.TermYears} years");
                    Console.WriteLine($"Monthly Payment:     ${calc.MonthlyPayment():N2}");
                    Console.WriteLine($"Total Paid:          ${calc.TotalPaid():N2}");
                    Console.WriteLine($"Total Interest:      ${calc.TotalInterest():N2}");
                }
                catch (SyntaxErrorException ex)
                {
                    Console.WriteLine($"\nSyntax Error: {ex.Message}\n");
                }
                catch (ValueErrorException ex)
                {
                    Console.WriteLine($"\nValue Error: {ex.Message}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}\n");
                }
            }
        }
    }
}
