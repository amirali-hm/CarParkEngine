using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;

namespace CarParkCalculator.Services {
    public class SpecialService : ISpecialService {

        private readonly IRepo<Special> _specialRepo;

        public SpecialService(ISpecialRepo specialRepo)
        {
            _specialRepo = specialRepo;
        }

        public async Task<IEnumerable<Special>> GetAllAsync()
        {
            return await _specialRepo.SelectAll();
        }

        public ParkingRate CalculateRate(IEnumerable<Special> specialRates, DateTime startDateTime, DateTime endDateTime)
        {
            var result = new ParkingRate();

            foreach (var rate in specialRates)
            {
                var isSpecial = (rate.Entry.Start <= startDateTime.TimeOfDay && startDateTime.TimeOfDay <= rate.Entry.End) ||
                                 (rate.MaxDays > 0 && (rate.Entry.Start <= startDateTime.TimeOfDay && startDateTime.TimeOfDay <= rate.Entry.End.Add(TimeSpan.FromDays(1))) ||
                                  (rate.Entry.Start.Subtract(TimeSpan.FromDays(1)) <= startDateTime.TimeOfDay &&
                                   startDateTime.TimeOfDay <= rate.Entry.End));
                if ( !rate.Entry.Days.Any( day => string.Equals(day, startDateTime.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase))) {
                    isSpecial = false;
                }
                
                var maxDayToExit = startDateTime.AddDays(rate.MaxDays);
                var maxValidToExit = new DateTime(maxDayToExit.Year, maxDayToExit.Month, maxDayToExit.Day, rate.Exit.End.Hours, rate.Exit.End.Minutes, 0);

                if (endDateTime > maxValidToExit) {
                    isSpecial = false;
                }
                if (!rate.Exit.Days.Any( day => string.Equals(day, endDateTime.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase))) {
                    isSpecial = false;
                }

                
                if ((endDateTime - startDateTime).Days > rate.MaxDays) {
                    isSpecial = false;
                }

                if (!isSpecial) continue;
                if (!(result.Price > rate.TotalPrice) && result.Price != 0) continue;

                result.Name = rate.Name;
                result.Price = rate.TotalPrice;
            }

            return result;
        }


    }
}