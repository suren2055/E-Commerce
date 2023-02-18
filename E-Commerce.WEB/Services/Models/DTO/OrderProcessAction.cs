namespace E_Commerce.WEB.Services.Models.DTO;

public record OrderProcessAction
{
    public string Code { get; }
    public string Name { get; }

    public static OrderProcessAction Ship = new OrderProcessAction(nameof(Ship).ToLowerInvariant(), "Ship");

    protected OrderProcessAction()
    {
    }

    public OrderProcessAction(string code, string name)
    {
        Code = code;
        Name = name;
    }
}