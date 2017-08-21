using CarParkCalculator.Models;

namespace CarParkCalculator.Abstractions {
    public interface ISpecialService : IService<Special>, IRateService<Special> {         
    }
}