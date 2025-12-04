namespace ERP.Reciclagem.Comercial.Domain.Credit;

public interface ICreditAnalysisService
{
    CreditDecision Analyze(Guid customerId, decimal orderTotal);
}

