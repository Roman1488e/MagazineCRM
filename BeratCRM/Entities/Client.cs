namespace BeratCRM.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? ContactNamber { get; set; }
    public string GovernmentId { get; set; }
    public string Address { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public List<PaymentHistory>? PaymentHistory { get; set; }
    public List<Debt>? Debt { get; set; }
    public List<Order>? Orders { get; set; }
}