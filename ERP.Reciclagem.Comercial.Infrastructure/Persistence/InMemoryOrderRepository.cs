using ERP.Reciclagem.Comercial.Domain.Orders;
using ERP.Reciclagem.Comercial.Domain.Repositories;

namespace ERP.Reciclagem.Comercial.Infrastructure.Persistence;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _store = new();

    public Order GetById(Guid id)
    {
        if (!_store.TryGetValue(id, out var order))
        {
            throw new KeyNotFoundException($"Order {id} not found.");
        }

        return order;
    }

    public void Save(Order order)
    {
        _store[order.Id] = order;
    }
}

