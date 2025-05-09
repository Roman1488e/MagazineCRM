using BeratCRM.Entities;

namespace BeratCRM.Repositories.Abstract;

public interface IPaymentHistoryRepository : IBaseRepository<PaymentHistory>
{
    public Task<PaymentHistory> GetByPaymentId(Guid paymentHistoryId);
    public Task<List<PaymentHistory>> GetAllPaymentHistory();
    public Task<List<PaymentHistory>> GetPaymentHistoryByOrderId(Guid orderId);
    public Task<List<PaymentHistory>> GetPaymentHistoryByClientId(Guid clientId);
    
}