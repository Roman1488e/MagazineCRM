using BeratCRM.DTOs;
using BeratCRM.Models;
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
        };
        payment.MounthEarnings = payments
            .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
            .Select(g => new MounthEarnings
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Amount = g.Sum(x => x.PaymentAmount),
                MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM")
            })
            .OrderBy(e => e.Year)
            .ThenBy(e => e.Month)
            .ToList();

        return payment;


    }

    public async Task<DebtStatisticDto> ShowDebtStatistic()
    {
        var debt = await _debtRepository.GetAllDebts();
        var orders = await _orderRepository.GetAllOrders();
        var debtstatistic = new DebtStatisticDto()
        {
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
            NotPaidDebts = debt.Count(x => !x.isPaid),
            TotalOrders = orders.Count,
            PaidOrders = orders.Count(x => x.IsPaid),
        };
        return generalStatistic;
    }
}