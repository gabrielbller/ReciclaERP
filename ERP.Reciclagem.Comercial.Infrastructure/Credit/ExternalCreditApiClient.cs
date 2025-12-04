namespace ERP.Reciclagem.Comercial.Infrastructure.Credit;

public sealed class ExternalCreditApiClient
{
    public ExternalCreditResult AnalyzeCredit(string document, decimal value)
    {
        var approved = value <= 100_000m;
        var code = approved ? "APPROVED" : "REJECTED";
        return new ExternalCreditResult(approved, code);
    }
}

