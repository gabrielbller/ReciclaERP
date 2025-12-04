namespace ERP.Reciclagem.Comercial.Application.Proposals;

public sealed class RepriceProposalRequest
{
    public Guid ProposalId { get; init; }
    public DateTime ReferenceDate { get; init; }
}

