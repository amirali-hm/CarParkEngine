using System.Collections.Generic;
using System.Threading.Tasks;
using CarParkCalculator.Models;

namespace CarParkCalculator.Abstractions {
    public interface IAppService {
        Validation ValidateInput(IList<string> input, out ParkingTimer timer);
        Task<string> Process(ParkingTimer input);
    }
}