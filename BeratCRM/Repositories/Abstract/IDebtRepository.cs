using BeratCRM.Entities;

namespace BeratCRM.Repositories.Abstract;

public interface IDebtRepository : IBaseRepository<Debt>
{
    public Task<Debt> GetDebtById(Guid id);
    public Task<List<Debt>> GetAllDebts();
    public Task<List<Debt>> GetDebtByClientId(Guid clientId);
    
    public Task<List<Debt>> GetDebtByOrderId(Guid orderId);
}