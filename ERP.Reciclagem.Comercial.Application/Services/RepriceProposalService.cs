using ERP.Reciclagem.Comercial.Application.Proposals;
using ERP.Reciclagem.Comercial.Domain.Pricing;
using ERP.Reciclagem.Comercial.Domain.Proposals;
using ERP.Reciclagem.Comercial.Domain.Repositories;

namespace ERP.Reciclagem.Comercial.Application.Services;

public sealed class RepriceProposalService
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IPricingStrategy _pricingStrategy;

    public RepriceProposalService(IProposalRepository proposalRepository, IPricingStrategy pricingStrategy)
    {
        _proposalRepository = proposalRepository;
        _pricingStrategy = pricingStrategy;
    }

    public RepriceProposalResponse Reprice(RepriceProposalRequest request)
    {
        var proposal = _proposalRepository.GetById(request.ProposalId);
        var expired = proposal.IsExpiredAt(request.ReferenceDate);

        if (!expired)
        {
            return new RepriceProposalResponse
            {
                ProposalId = proposal.Id,
                WasExpired = false,
                NewValidUntil = proposal.ValidUntil,
                NewTotal = proposal.Total
            };
        }

        proposal.ExtendValidity(request.ReferenceDate.AddDays(_pricingStrategy.DefaultValidityDays()));

        var updatedItems = proposal.Items
            .Select(item => new ProposalItem(
                item.ProductId,
                item.Quantity,
                _pricingStrategy.CalculateItemUnitPrice(item.ProductId)))
            .ToList();

        proposal.ReplaceItems(updatedItems);
        proposal.MarkAsSent();
        _proposalRepository.Save(proposal);

        return new RepriceProposalResponse
        {
            ProposalId = proposal.Id,
            WasExpired = true,
            NewValidUntil = proposal.ValidUntil,
            NewTotal = proposal.Total
        };
    }
}

