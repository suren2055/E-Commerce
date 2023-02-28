using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }
    public string Street { get;  set; }
    public string City { get;  set; }
    public string State { get;  set; }
    public string Country { get;  set; }
    public string ZipCode { get;  set; }
}