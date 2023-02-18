namespace E_Commerce.WEB.ViewModels;

public record Basket
{
    public List<BasketItem> Items { get; init; } = new List<BasketItem>();
    public string BuyerId { get; init; }

    public decimal Total()
    {
        return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
    }
}