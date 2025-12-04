using ERP.Reciclagem.Comercial.Domain.Orders;
using ERP.Reciclagem.Comercial.Domain.Proposals;

namespace ERP.Reciclagem.Comercial.Domain.Factories;

public sealed class OrderFactory : IOrderFactory
{
    public Order CreateFromProposal(Proposal proposal, DateTime createdAt)
    {
        var order = new Order(proposal.CustomerId, createdAt);

        foreach (var item in proposal.Items)
        {
            var orderItem = new OrderItem(item.ProductId, item.Quantity, item.UnitPrice);
            order.AddItem(orderItem);
        }

        order.MarkAsWaitingProduction();
        return order;
    }
}

