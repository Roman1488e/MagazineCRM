using BeratCRM.Context;
using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BeratCRM.Repositories;

public class DebtRepository(CrmDbContext context) : IDebtRepository
{
    private readonly CrmDbContext _context = context;

    public async Task<Debt> Create(Debt entity)
    {
        await _context.Debts.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Debt> Update(Debt entity)
    {
        _context.Debts.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Debt> Delete(Guid id)
    {
       var entity = await _context.Debts.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null)
            throw new Exception("No Debts was found");
        _context.Debts.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Debt> GetDebtById(Guid id)
    {
        var entity = await _context.Debts.FirstOrDefaultAsync(x => x.Id == id);
        if(entity == null)
            throw new Exception("No Debts was found");
        return entity;
    }

    public async Task<List<Debt>> GetAllDebts()
    {
        var entities = await _context.Debts.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<List<Debt>> GetDebtByClientId(Guid clientId)
    {
        var entities = await _context.Debts.AsNoTracking().Where(x => x.ClientId == clientId).ToListAsync();
        return entities;
    }

    public async Task<List<Debt>> GetDebtByOrderId(Guid orderId)
    {
        var entities = await _context.Debts.AsNoTracking().Where(x => x.OrderId == orderId).ToListAsync();
        return entities;
    }
}