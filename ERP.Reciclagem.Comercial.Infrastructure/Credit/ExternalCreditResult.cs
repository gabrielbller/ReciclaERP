namespace ERP.Reciclagem.Comercial.Infrastructure.Credit;

public sealed class ExternalCreditResult
{
    public bool Approved { get; }
    public string DecisionCode { get; }

    public ExternalCreditResult(bool approved, string decisionCode)
    {
        Approved = approved;
        DecisionCode = decisionCode;
    }
}

