namespace ERP.Reciclagem.Comercial.Application.Proposals;

public sealed class RepriceProposalResponse
{
    public Guid ProposalId { get; init; }
    public bool WasExpired { get; init; }
    public DateTime NewValidUntil { get; init; }
    public decimal NewTotal { get; init; }
}

