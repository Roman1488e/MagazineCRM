namespace BeratCRM.Entities;

public class PaymentHistory
{
    public Guid PaymentHistoryId { get; set; }
    public Guid ClientId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PaymentAmount { get; set; }
}