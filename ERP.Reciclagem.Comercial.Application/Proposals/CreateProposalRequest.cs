namespace ERP.Reciclagem.Comercial.Application.Proposals;

public sealed class CreateProposalRequest
{
    public Guid CustomerId { get; init; }
    public IReadOnlyCollection<CreateProposalItemRequest> Items { get; init; } = Array.Empty<CreateProposalItemRequest>();
}

