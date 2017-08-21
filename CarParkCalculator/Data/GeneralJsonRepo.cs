using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Models;
using CarParkCalculator.Utilities;

namespace CarParkCalculator.Data
{
    public class GeneralJsonRepo : IGeneralRepo
    {
        public async Task<IEnumerable<General>> SelectAll()
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            var data = IOHelper.ReadFile(path + "\\Data\\general.json");
            return JsonConvert.DeserializeObject<IEnumerable<General>>(data);
        }
    }
}
