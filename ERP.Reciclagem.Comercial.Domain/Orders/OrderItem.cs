namespace ERP.Reciclagem.Comercial.Domain.Orders;

public sealed class OrderItem
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Total => UnitPrice * Quantity;

    public OrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be non-negative.");
        }

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}

