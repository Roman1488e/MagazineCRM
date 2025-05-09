using BeratCRM.Context;
using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BeratCRM.Repositories;

public class PaymentHistoryRepository(CrmDbContext context) : IPaymentHistoryRepository
{
    private readonly CrmDbContext _context = context;

    public async Task<PaymentHistory> Create(PaymentHistory entity)
    {
        await _context.PaymentHistories.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PaymentHistory> Update(PaymentHistory entity)
    {
        _context.PaymentHistories.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PaymentHistory> Delete(Guid id)
    {
        var entity = await _context.PaymentHistories.FirstOrDefaultAsync(x => x.PaymentHistoryId == id);
        if (entity == null)
            throw new Exception("PaymentHistory was not found");
        _context.PaymentHistories.Remove(entity);
        return entity;
        
    }

    public async Task<PaymentHistory> GetByPaymentId(Guid paymentHistoryId)
    {
        var entity = await _context.PaymentHistories.FirstOrDefaultAsync(x => x.PaymentHistoryId == paymentHistoryId);
        if(entity == null)
            throw new Exception("PaymentHistory was not found");
        return entity;
    }

    public async Task<List<PaymentHistory>> GetAllPaymentHistory()
    {
        var entities = await _context.PaymentHistories.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<List<PaymentHistory>> GetPaymentHistoryByOrderId(Guid orderId)
    {
        var entities = await _context.PaymentHistories.AsNoTracking().Where(x => x.OrderId == orderId).ToListAsync();
        return entities;
    }

    public async Task<List<PaymentHistory>> GetPaymentHistoryByClientId(Guid clientId)
    {
        var entities = await _context.PaymentHistories.AsNoTracking().Where(x => x.ClientId == clientId).ToListAsync();
        return entities;
    }
}