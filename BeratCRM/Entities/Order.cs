namespace BeratCRM.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public Debt? Debt { get; set; }
    public int OrderNumber { get; set; }
    public string ProductName { get; set; }
    public string CustomerFullName { get; set; }
    public bool IsReminding { get; set; } = false;
    public bool IsPaid { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ReminderDate { get; set; }
    public List<PaymentHistory>? PaymentHistory { get; set; }
    public int RemindInMounts { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal PaidAmount { get; set; }
}