namespace ERP.Reciclagem.Comercial.Domain.Pricing;

public sealed class PromotionalPricingStrategy : IPricingStrategy
{
    private const decimal DiscountFactor = 0.9m;

    public decimal CalculateItemUnitPrice(Guid productId)
    {
        var rawValue = Math.Abs(productId.GetHashCode());
        var price = ((rawValue % 5_000) + 100) * DiscountFactor;
        return price;
    }

    public int DefaultValidityDays()
    {
        return 7;
    }
}
