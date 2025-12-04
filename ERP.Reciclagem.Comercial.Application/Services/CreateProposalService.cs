using ERP.Reciclagem.Comercial.Application.Proposals;
using ERP.Reciclagem.Comercial.Domain.Pricing;
using ERP.Reciclagem.Comercial.Domain.Proposals;
using ERP.Reciclagem.Comercial.Domain.Repositories;

namespace ERP.Reciclagem.Comercial.Application.Services;

public sealed class CreateProposalService
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IPricingStrategy _pricingStrategy;

    public CreateProposalService(IProposalRepository proposalRepository, IPricingStrategy pricingStrategy)
    {
        _proposalRepository = proposalRepository;
        _pricingStrategy = pricingStrategy;
    }

    public ProposalResponse Create(CreateProposalRequest request, DateTime now)
    {
        var validUntil = now.AddDays(_pricingStrategy.DefaultValidityDays());
        var proposal = new Proposal(request.CustomerId, now, validUntil);

        foreach (var item in request.Items)
        {
            var unitPrice = _pricingStrategy.CalculateItemUnitPrice(item.ProductId);
            var proposalItem = new ProposalItem(item.ProductId, item.Quantity, unitPrice);
            proposal.AddItem(proposalItem);
        }

        proposal.MarkAsSent();
        _proposalRepository.Save(proposal);

        return new ProposalResponse
        {
            Id = proposal.Id,
            CustomerId = proposal.CustomerId,
            CreatedAt = proposal.CreatedAt,
            ValidUntil = proposal.ValidUntil,
            Total = proposal.Total
        };
    }
}

