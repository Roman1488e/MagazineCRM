using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class DebtService(IDebtRepository debtRepository, IPaymentHistoryRepository paymentHistoryRepository, IOrderRepository orderRepository)
{
    private readonly IDebtRepository _debtRepository = debtRepository;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository = paymentHistoryRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    public async Task<List<Debt>> GetAllDebts()
    {
        var debts = await _debtRepository.GetAllDebts();
        return debts;
    }

    public async Task<Debt> GetDebt(Guid debtId)
    {
        var debt = await _debtRepository.GetDebtById(debtId);
        return debt;
    }

    public async Task<List<Debt>> GetDebtsByClientId(Guid clientId)
    {
        var debts = await _debtRepository.GetDebtByClientId(clientId);
        return debts;
    }

    public async Task<List<Debt>> GetDebtsByOrderId(Guid orderId)
    {
        var debts = await _debtRepository.GetDebtByOrderId(orderId);
        return debts;
    }
    
    public async Task<Debt> PayDebt(Guid debtId, decimal amount)
    {
        if (amount <= 0)
            return new Debt();
        var debt = await _debtRepository.GetDebtById(debtId);
        debt.Amount -= amount;
        debt.LastPaymentAmount = amount;
        debt.LastPaymentDate = DateTime.UtcNow;
        var paymentHistory = new PaymentHistory()
        {
            ClientId = debt.ClientId,
            OrderId = debt.OrderId,
            PaymentAmount = amount,
            PaymentDate = DateTime.UtcNow
        };
        var order =await _orderRepository.GetByOrderId(debt.OrderId);
        order.PaidAmount += amount;
        if (order.PaidAmount == order.TotalPrice)
            order.IsPaid = true;
        await _orderRepository.Update(order);
        await _paymentHistoryRepository.Create(paymentHistory);
        await _debtRepository.Update(debt);
        return debt;
    }
}

