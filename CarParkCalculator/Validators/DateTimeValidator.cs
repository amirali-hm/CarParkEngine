using System;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Validators
{
    public class DateTimeValidator : IValidator<string>
    {
        public Validation IsValid(string input)
        {
            var result = new Validation() { IsValid = true };

            try
            {
                if (Convert.ToDateTime(input) == default(DateTime)) {
                    result.IsValid = false;
                    result.ErrorMessage = "Invalid datetime entered";
                }
            }
            catch (Exception ex) {
                result.IsValid = false;
                result.ErrorMessage = "Failed to parse input datetime : " + ex.Message;
            }

            return result;
        }
    }
}
