namespace BeratCRM.DTOs;

public class DebtStatisticDto
{
    public decimal TotalDebtAmount { get; set; }
    public decimal PaidDebtAmount { get; set; }
    public DateTime LastDebtPaymentDate { get; set; }
}