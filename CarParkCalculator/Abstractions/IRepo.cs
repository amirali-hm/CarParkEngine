using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarParkCalculator.Abstractions
{
    public interface IRepo<T> {
        Task<IEnumerable<T>> SelectAll();
    }
}
