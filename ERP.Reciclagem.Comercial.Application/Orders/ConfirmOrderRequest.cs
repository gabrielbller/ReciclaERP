namespace ERP.Reciclagem.Comercial.Application.Orders;

public sealed class ConfirmOrderRequest
{
    public Guid ProposalId { get; init; }
    public bool DownPaymentReceived { get; init; }
}

