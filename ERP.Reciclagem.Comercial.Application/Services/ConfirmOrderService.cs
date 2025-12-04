using ERP.Reciclagem.Comercial.Application.Orders;
using ERP.Reciclagem.Comercial.Domain.Credit;
using ERP.Reciclagem.Comercial.Domain.Factories;
using ERP.Reciclagem.Comercial.Domain.Repositories;

namespace ERP.Reciclagem.Comercial.Application.Services;

public sealed class ConfirmOrderService
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICreditAnalysisService _creditAnalysisService;
    private readonly IOrderFactory _orderFactory;

    public ConfirmOrderService(
        IProposalRepository proposalRepository,
        IOrderRepository orderRepository,
        ICreditAnalysisService creditAnalysisService,
        IOrderFactory orderFactory)
    {
        _proposalRepository = proposalRepository;
        _orderRepository = orderRepository;
        _creditAnalysisService = creditAnalysisService;
        _orderFactory = orderFactory;
    }

    public ConfirmOrderResponse Confirm(ConfirmOrderRequest request, DateTime now)
    {
        if (!request.DownPaymentReceived)
        {
            throw new InvalidOperationException("Down payment is required to confirm the order.");
        }

        var proposal = _proposalRepository.GetById(request.ProposalId);

        var creditDecision = _creditAnalysisService.Analyze(proposal.CustomerId, proposal.Total);
        if (creditDecision == CreditDecision.Rejected)
        {
            throw new InvalidOperationException("Credit analysis rejected for this customer.");
        }

        var order = _orderFactory.CreateFromProposal(proposal, now);

        _orderRepository.Save(order);

        return new ConfirmOrderResponse
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Total = order.Total,
            CreatedAt = order.CreatedAt
        };
    }
}

