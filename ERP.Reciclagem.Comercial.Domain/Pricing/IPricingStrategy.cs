namespace ERP.Reciclagem.Comercial.Domain.Pricing;

public interface IPricingStrategy
{
    decimal CalculateItemUnitPrice(Guid productId);
    int DefaultValidityDays();
}

