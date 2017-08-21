using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;
using CarParkCalculator.Utilities;
using Newtonsoft.Json;

namespace CarParkCalculator.Data
{
    class SpecialJsonRepo : ISpecialRepo
    {
        public async Task<IEnumerable<Special>> SelectAll()
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            var data = IOHelper.ReadFile(path + "\\Data\\special.json");
            return JsonConvert.DeserializeObject<IEnumerable<Special>>(data);
        }
    }
}
