using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Payment.Entities;

public class Payment
{
    [Key] 
    public Guid PaymentID { get; set; }
    public Guid PayerId { get; set; } //user id from keycloak
    public decimal Amount { get; set; }
    public DateTime PaymentDateTime { get; set; }
    public string CardHolderName { get; set; }
    public int TransactionStatus { get; set; } //Transaction Successfully Completed = 1
    public string BillingAddress { get; set; }
}