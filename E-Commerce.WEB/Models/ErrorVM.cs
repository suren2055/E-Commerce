namespace E_Commerce.WEB.Models;

public class ErrorVM
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}