
using AdvancedDevSample.Domain.Entyties;
using System.Threading.Tasks;

namespace AdvancedDevSample.Domain.Interfaces
{
    public interface IOrderRepositoryAsync
    {
        Task AddAsync(Order order);
    }
}