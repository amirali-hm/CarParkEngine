using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarParkCalculator.Abstractions {
    public interface IService<T> {
        Task<IEnumerable<T>> GetAllAsync();
    }
}