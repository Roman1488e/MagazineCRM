namespace BeratCRM.DTOs;

public class DebtDto
{
    public Guid id { get; set; }
    public string clientFullName { get; set; } = "";
    public string productName { get; set; } = "";
    public decimal amount { get; set; }
    public bool IsPaid { get; set; }
    public DateTime lastPaymentDate { get; set; }
    public decimal lastPaymentAmount { get; set; }
}