using ERP.Reciclagem.Comercial.Domain.Orders;

namespace ERP.Reciclagem.Comercial.Domain.Repositories;

public interface IOrderRepository
{
    Order GetById(Guid id);
    void Save(Order order);
}

