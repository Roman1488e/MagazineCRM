using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class PaymentHistoryService(IPaymentHistoryRepository paymentHistoryRepository)
{
    private readonly IPaymentHistoryRepository _paymentHistoryRepository = paymentHistoryRepository;

    public async Task<List<PaymentHistory>> GetAllPaymentHistory()
    {
        return await _paymentHistoryRepository.GetAllPaymentHistory();
    }

    public async Task<PaymentHistory> GetPaymentHistory(Guid id)
    {
        return await _paymentHistoryRepository.GetByPaymentId(id);
    }

    public async Task<List<PaymentHistory>> GetPaymentHistoryByOrderId(Guid orderId)
    {
        var entities = await _paymentHistoryRepository.GetPaymentHistoryByOrderId(orderId);
        return entities;
    }

    public async Task<List<PaymentHistory>> GetPaymentHistoryByClientId(Guid clientId)
    {
        var enties = await _paymentHistoryRepository.GetPaymentHistoryByClientId(clientId);
        return enties;
    }
}