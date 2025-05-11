using BeratCRM.DTOs;
using BeratCRM.Repositories;
using BeratCRM.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Services;

public class StatisticsService(IPaymentHistoryRepository paymentHistoryRepository, IDebtRepository debtRepository, IOrderRepository orderRepository, IClientRepository clientRepository)
{
    private readonly IPaymentHistoryRepository _paymentHistoryRepository = paymentHistoryRepository;
    private readonly IDebtRepository _debtRepository = debtRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IClientRepository _clientRepository = clientRepository;

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

    public async Task<DebtStatisticDto> ShowDebtStatistic()
    {
        var debt = await _debtRepository.GetAllDebts();
        var orders = await _orderRepository.GetAllOrders();
        var debtstatistic = new DebtStatisticDto()
        {
            LastDebtPaymentDate = debt.Max(x => x.LastPaymentDate),
            PaidDebtAmount = orders.Sum(x => x.TotalPrice - x.PaidAmount),
            TotalDebtAmount = debt.Sum(x => x.Amount)
        };
        return debtstatistic;
    }

    public async Task<GeneralStatisticDto> ShowGeneralStatistic()
    {
        var debt = await _debtRepository.GetAllDebts();
        var orders = await _orderRepository.GetAllOrders();
        var clients = await _clientRepository .GetAllClients();
        var generalStatistic = new GeneralStatisticDto()
        {
            TotalClients = clients.Count,
            TotalDebts = debt.Count,
            TotalOrders = orders.Count,
            PaidOrders = orders.Count(x => x.IsPaid),
        };
        return generalStatistic;
    }
}