using System;
using System.Collections.Generic;
using CarParkCalculator.Models;

namespace CarParkCalculator.Abstractions {
    public interface IRateService<T> {
        ParkingRate CalculateRate(IEnumerable<T> rates, DateTime startDateTime, DateTime endDateTime);
    }
}