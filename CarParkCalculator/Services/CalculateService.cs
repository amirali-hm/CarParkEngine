using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Services {
    public class CalculateService : ICalculateService {

        private readonly ISpecialService _specialService;
        private readonly IGeneralService _generalService;

        public CalculateService(ISpecialService specialService, IGeneralService generalService)
        {
            _specialService = specialService;
            _generalService = generalService;
        }

        public async Task<ParkingRate> Calculate(ParkingTimer input)
        {
            var specialRates = await _specialService.GetAllAsync();
            var generalRates = await _generalService.GetAllAsync();

            var result = new ParkingRate();

            // TODO: Refactor this with Abstraction like IValidator 
            // TODO: and orchestrate with Chain of Responsibility pattern
            var resultSpecial = _specialService.CalculateRate(specialRates, input.Entry, input.Exit);
            result = resultSpecial;

            var resultNormal = _generalService.CalculateRate(generalRates, input.Entry, input.Exit);

            // choose for either normal vs special
            if (resultNormal.Price > 0 && (result.Price == 0 || result.Price > resultNormal.Price))
            {
                result.Name = resultNormal.Name;
                result.Price = resultNormal.Price;
            }

            return result;
        }

    }
}