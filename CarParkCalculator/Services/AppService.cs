using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Services {
    public class AppService : IAppService {

        private readonly ICalculateService _calculateService;
        private readonly IEnumerable<IValidator<string>> _consoleValidators;
        private readonly IEnumerable<IValidator<ParkingTimer>> _durationValidators;

        public AppService(ICalculateService calculateService, IEnumerable<IValidator<string>> consoleValidator, IEnumerable<IValidator<ParkingTimer>> durationValidator)
        {
            _calculateService = calculateService;
            _consoleValidators = consoleValidator;
            _durationValidators = durationValidator;
        }

        public async Task<string> Process(ParkingTimer input)
        {
            var response = await _calculateService.Calculate(input);
            var result = "\nTYPE USED : " + response.Name + "\nTOTAL FEE : " + response.Price.ToString("C");

            return result;
        }

        public Validation ValidateInput(IList<string> input, out ParkingTimer timer)
        {
            timer = new ParkingTimer();

            foreach (var validator in _consoleValidators) {
                foreach (var res in input.Select(i => validator.IsValid(i)).Where(r => !r.IsValid)) {
                    return res;
                }
            }

            timer.Entry = Convert.ToDateTime(input.ElementAt(0));
            timer.Exit = Convert.ToDateTime(input.ElementAt(1));

            foreach (var validator in _durationValidators) {
                var res = validator.IsValid(timer);
                if (!res.IsValid) { return res; }
            }

            return new Validation() { IsValid = true };
        }


    }
}