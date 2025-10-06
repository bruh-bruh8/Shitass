using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace shitass
{
    public class CalcCommand : ICommand
    {
        public string Name => "calc";
        public string[] Aliases => new string[] { "calculate", "math" };
        public string Description => "Evaluates mathematical expressions";
        public string Usage => "calc <expression>";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: calc <expression>");
                Console.WriteLine("Example: calc 2^8 or calc 25*3+17");
                return Task.CompletedTask;
            }

            string expression = string.Join("", args.Skip(1));
            string originalExpression = expression;

            try
            {
                // replace exponents
                expression = HandleExponents(expression);

                var result = new DataTable().Compute(expression, null);
                Console.WriteLine($"\n{originalExpression} = {result}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid expression: {e.Message}");
            }

            return Task.CompletedTask;
        }

        private string HandleExponents(string expression)
        {
            // find exponents
            var regex = new Regex(@"(\d+\.?\d*)\^(\d+\.?\d*)");

            while (regex.IsMatch(expression))
            {
                expression = regex.Replace(expression, match =>
                {
                    double baseNum = double.Parse(match.Groups[1].Value);
                    double exponent = double.Parse(match.Groups[2].Value);
                    double result = Math.Pow(baseNum, exponent);
                    return result.ToString();
                }, 1);
            }

            return expression;
        }
    }
}