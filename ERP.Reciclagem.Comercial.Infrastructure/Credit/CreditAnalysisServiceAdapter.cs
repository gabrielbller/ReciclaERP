using ERP.Reciclagem.Comercial.Domain.Credit;

namespace ERP.Reciclagem.Comercial.Infrastructure.Credit;

public sealed class CreditAnalysisServiceAdapter : ICreditAnalysisService
{
    private readonly ExternalCreditApiClient _client;

    public CreditAnalysisServiceAdapter(ExternalCreditApiClient client)
    {
        _client = client;
    }

    public CreditDecision Analyze(Guid customerId, decimal orderTotal)
    {
        var result = _client.AnalyzeCredit(customerId.ToString(), orderTotal);
        return result.Approved ? CreditDecision.Approved : CreditDecision.Rejected;
    }
}

