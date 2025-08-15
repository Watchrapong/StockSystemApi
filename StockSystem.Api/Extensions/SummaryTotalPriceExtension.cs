using StockSystem.Api.Domain;

namespace StockSystem.Api.Extensions;

public static class SummaryTotalPriceExtension
{
    public static decimal GetSummaryTotalPrice(this ShoppingCart shoppingCart)
    {
        var totalPrice = 0m;
        foreach (var item in shoppingCart.Items)
        { 
            totalPrice += item.PricePerUnit * item.Quantity;
        }
        return totalPrice;
    }
}