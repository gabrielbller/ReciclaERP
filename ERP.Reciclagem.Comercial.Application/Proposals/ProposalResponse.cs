namespace ERP.Reciclagem.Comercial.Application.Proposals;

public sealed class ProposalResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ValidUntil { get; init; }
    public decimal Total { get; init; }
}

