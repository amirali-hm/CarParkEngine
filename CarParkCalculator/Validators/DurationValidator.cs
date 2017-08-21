using System;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Validators
{
    public class DurationValidator : IValidator<ParkingTimer>
    {
        public Validation IsValid(ParkingTimer input)
        {
            var result = new Validation() {
                IsValid = true
            };

            var start = Convert.ToDateTime(input.Entry);
            var end = Convert.ToDateTime(input.Exit);

            if (end > start) return result;
            result.IsValid = false;
            result.ErrorMessage = "Start datetime could not be greater than End datetime";

            return result;
        }
    }
}
