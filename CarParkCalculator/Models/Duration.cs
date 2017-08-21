using System;
using System.Collections.Generic;

namespace CarParkCalculator.Models {

    public class Duration
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public IEnumerable<string> Days { get; set; }
    }
}