using CarParkCalculator.Models;

namespace CarParkCalculator.Abstractions
{
    public interface IValidator<in T>
    {
        Validation IsValid(T input);
    }
}
