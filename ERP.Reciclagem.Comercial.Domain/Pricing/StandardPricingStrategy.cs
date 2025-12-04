namespace ERP.Reciclagem.Comercial.Domain.Pricing;

public sealed class StandardPricingStrategy : IPricingStrategy
{
    public decimal CalculateItemUnitPrice(Guid productId)
    {
        var rawValue = Math.Abs(productId.GetHashCode());
        var price = (rawValue % 5_000) + 100;
        return price;
    }

    public int DefaultValidityDays()
    {
        return 15;
    }
}

