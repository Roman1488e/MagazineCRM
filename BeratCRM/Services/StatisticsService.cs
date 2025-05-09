using BeratCRM.DTOs;
using BeratCRM.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Services;

public class StatisticsService(IPaymentHistoryRepository paymentHistoryRepository)
{
    private readonly IPaymentHistoryRepository _paymentHistoryRepository = paymentHistoryRepository;

    public async Task<PaymentStatisticDto> ShowPaymentStatistic()
    {
        var payments = await _paymentHistoryRepository.GetAllPaymentHistory();
        var payment = new PaymentStatisticDto()
        {
            EarnedForAllTime = payments.Sum(x => x.PaymentAmount),
            EarnedinMounth =
                payments.Where(x => x.PaymentDate.Month == DateTime.UtcNow.Month).Sum(x => x.PaymentAmount),
            EarnedToday = payments.Where(x => x.PaymentDate.Day == DateTime.UtcNow.Day).Sum(x => x.PaymentAmount)
        };
        return payment;

    }
}