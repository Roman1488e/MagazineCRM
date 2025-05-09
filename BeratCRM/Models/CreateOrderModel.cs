namespace BeratCRM.Models;

public class CreateOrderModel
{
    public Guid CustomerId { get; set; }
    public int RemindInMonths { get; set; }
    public string ProductName { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal PaidAmount { get; set; }
}