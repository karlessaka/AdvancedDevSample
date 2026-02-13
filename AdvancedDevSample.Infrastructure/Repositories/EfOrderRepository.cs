
using AdvancedDevSample.Domain.Entyties;
using AdvancedDevSample.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvancedDevSample.Infrastructure.Repositories
{
    public class EfOrderRepository : IOrderRepositoryAsync
    {
        // Notre fausse table "Orders"
        private static readonly List<Order> _orders = new List<Order>();

        public Task AddAsync(Order order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }
    }
}