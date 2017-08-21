using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Services {
    public class GeneralService : IGeneralService {

        private readonly IRepo<General> _generalRepo;
        private const string StandardRate = "Standard Rate";

        public GeneralService(IGeneralRepo generalRepo) {
            _generalRepo = generalRepo;
        }

        public async Task<IEnumerable<General>> GetAllAsync() {
            return await _generalRepo.SelectAll();
        }

        public ParkingRate CalculateRate(IEnumerable<General> generalRates, DateTime startDateTime, DateTime endDateTime)
        {
            var result = new ParkingRate { Name = StandardRate };

            var partialMaxResult = 0.0;
            var partialMinResult = 0.0;
            var isGeneral = false;

            var duration = (endDateTime - startDateTime).TotalHours;
            var maxRate = generalRates.OrderBy(r => r.MaxHours).LastOrDefault();

            if (duration >= maxRate.MaxHours)
            {
                partialMaxResult = Math.Floor(duration / maxRate.MaxHours) * maxRate.Rate;
                duration = duration % maxRate.MaxHours;
            }

            if (duration > 0)
            {
                foreach (var rate in generalRates)
                {
                    if (!isGeneral && duration <= rate.MaxHours)
                    {
                        isGeneral = true;
                        partialMinResult = rate.Rate;
                    }
                }
            }

            result.Price = partialMaxResult + partialMinResult;

            return result;
        }

    }
}