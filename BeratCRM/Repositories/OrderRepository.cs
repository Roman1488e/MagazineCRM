using BeratCRM.Context;
using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BeratCRM.Repositories;

public class OrderRepository(CrmDbContext context) : IOrderRepository
{
    private readonly CrmDbContext _context = context;

    public async Task<Order> Create(Order entity)
    {
        await context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> Update(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> Delete(Guid id)
    {
        var entity = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (entity == null)
            throw new Exception("No order was found");
        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> GetByOrderId(Guid orderId)
    {
        var entity = await _context.Orders.Include(x=> x.PaymentHistory)
            .Include(x=> x.Debt).FirstOrDefaultAsync(x => x.OrderId == orderId);
        if (entity == null)
            throw new Exception("No order was found");
        return entity;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        var entity = await _context.Orders.Include(x=> x.Debt).ToListAsync();
        return entity;
    }

    public async Task<List<Order>> GetOrdersByClientId(Guid clientId)
    {
        var entity = await _context.Orders
            .Where(x => x.CustomerId == clientId).AsNoTracking().ToListAsync();
        return entity;
    }
}