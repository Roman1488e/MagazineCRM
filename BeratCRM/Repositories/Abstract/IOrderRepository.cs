using BeratCRM.Entities;

namespace BeratCRM.Repositories.Abstract;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<Order> GetByOrderId(Guid orderId);
    public Task<List<Order>> GetAllOrders();
    public Task<List<Order>> GetOrdersByClientId(Guid clientId);
}