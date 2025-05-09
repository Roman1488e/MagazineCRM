using System.Security.Cryptography.X509Certificates;

namespace BeratCRM.Entities;

public class Debt
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid ClientId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public decimal LastPaymentAmount { get; set; }
}
