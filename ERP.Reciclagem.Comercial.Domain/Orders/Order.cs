using System.Collections.ObjectModel;

namespace ERP.Reciclagem.Comercial.Domain.Orders;

public sealed class Order
{
    private readonly IList<OrderItem> _items = new List<OrderItem>();
    
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public DateTime CreatedAt { get; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => new ReadOnlyCollection<OrderItem>(_items);
    public decimal Total => _items.Sum(item => item.Total);

    public Order(Guid customerId, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = createdAt;
        Status = OrderStatus.Created;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void MarkAsWaitingProduction()
    {
        Status = OrderStatus.WaitingProduction;
    }

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
    }
}

