namespace BeratCRM.DTOs;

public class PaymentStatisticDto
{
    public decimal EarnedToday { get; set; }
    public decimal EarnedForAllTime { get; set; }
    public List<MounthEarnings>  MounthEarnings { get; set; }
    
}