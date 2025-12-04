using ERP.Reciclagem.Comercial.Application.Orders;
using ERP.Reciclagem.Comercial.Application.Proposals;
using ERP.Reciclagem.Comercial.Application.Services;
using ERP.Reciclagem.Comercial.Domain.Factories;
using ERP.Reciclagem.Comercial.Domain.Pricing;
using ERP.Reciclagem.Comercial.Infrastructure.Credit;
using ERP.Reciclagem.Comercial.Infrastructure.Persistence;

// Setup dependencies
var proposalRepository = new InMemoryProposalRepository();
var orderRepository = new InMemoryOrderRepository();
IPricingStrategy pricingStrategy = new StandardPricingStrategy();
var orderFactory = new OrderFactory();
var creditClient = new ExternalCreditApiClient();
var creditService = new CreditAnalysisServiceAdapter(creditClient);

var createService = new CreateProposalService(proposalRepository, pricingStrategy);
var repriceService = new RepriceProposalService(proposalRepository, pricingStrategy);
var confirmService = new ConfirmOrderService(proposalRepository, orderRepository, creditService, orderFactory);

// Create proposal
var customerId = Guid.NewGuid();
var createRequest = new CreateProposalRequest
{
    CustomerId = customerId,
    Items = new List<CreateProposalItemRequest>
    {
        new() { ProductId = Guid.NewGuid(), Quantity = 2 },
        new() { ProductId = Guid.NewGuid(), Quantity = 1 }
    }
};

var now = DateTime.UtcNow;
var proposalResponse = createService.Create(createRequest, now);
Console.WriteLine($"Proposal {proposalResponse.Id} total: {proposalResponse.Total:C}");

// Simulate expiration and reprice
var repriceRequest = new RepriceProposalRequest
{
    ProposalId = proposalResponse.Id,
    ReferenceDate = now.AddDays(20)
};

var repriceResponse = repriceService.Reprice(repriceRequest);
Console.WriteLine($"Repriced (expired={repriceResponse.WasExpired}) new total: {repriceResponse.NewTotal:C}");

// Confirm order
var confirmRequest = new ConfirmOrderRequest
{
    ProposalId = proposalResponse.Id,
    DownPaymentReceived = true
};

var orderResponse = confirmService.Confirm(confirmRequest, DateTime.UtcNow);
Console.WriteLine($"Order {orderResponse.OrderId} confirmed total: {orderResponse.Total:C}");
