using ERP.Reciclagem.Comercial.Domain.Orders;
using ERP.Reciclagem.Comercial.Domain.Proposals;

namespace ERP.Reciclagem.Comercial.Domain.Factories;

public interface IOrderFactory
{
    Order CreateFromProposal(Proposal proposal, DateTime createdAt);
}

