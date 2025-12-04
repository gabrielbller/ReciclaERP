namespace ERP.Reciclagem.Comercial.Application.Orders;

public sealed class ConfirmOrderResponse
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public decimal Total { get; init; }
    public DateTime CreatedAt { get; init; }
}

