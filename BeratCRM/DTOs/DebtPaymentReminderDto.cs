namespace BeratCRM.DTOs;

public class DebtPaymentReminderDto
{
    public decimal Amount { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public string ClientFullName { get; set; }
    public string? ContactNum { get; set; }
    public decimal LastPaymentAmount { get; set; }
}