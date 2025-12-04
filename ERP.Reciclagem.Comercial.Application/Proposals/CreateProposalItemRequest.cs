namespace ERP.Reciclagem.Comercial.Application.Proposals;

public sealed class CreateProposalItemRequest
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}

