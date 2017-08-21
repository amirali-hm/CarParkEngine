using System.Threading.Tasks;
using CarParkCalculator.Models;

namespace CarParkCalculator.Abstractions {
    public interface ICalculateService {
        Task<ParkingRate> Calculate(ParkingTimer input);
    }
}