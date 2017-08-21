using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Validators
{
    public class StirngValidator : IValidator<string>
    {
        public Validation IsValid(string input)
        {
            var result = new Validation() { IsValid = true };

            if (!string.IsNullOrEmpty(input)) return result;

            result.IsValid = false;
            result.ErrorMessage = "Input string cannot be null or empty";

            return result;
        }
    }
}
