using BeratCRM.Context;
using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BeratCRM.Repositories;

public class ClientRepository(CrmDbContext dbContext) : IClientRepository
{
    private readonly CrmDbContext _dbContext = dbContext;
    public async Task<Client> Create(Client entity)
    {
        await _dbContext.Clients.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Client> Update(Client entity)
    {
        _dbContext.Clients.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Client> Delete(Guid id)
    {
        var entity = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new Exception("No client found");
        _dbContext.Clients.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Client> GetClientById(Guid id)
    {
        var entity = await _dbContext.Clients.Include(x=> x.Debt).Include(x=>x.Orders)
            .Include(x=> x.PaymentHistory).FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new Exception("No client found");
        return entity;
    }

    public async Task<List<Client>> GetAllClients()
    {
        var entities = await _dbContext.Clients.AsNoTracking().Include(x=> x.Debt)
            .Include(x=> x.PaymentHistory).ToListAsync();
        return entities;
    }

    public async Task<Client> GetClientByNumber(string number)
    {
        var entity = await _dbContext.Clients
            .Include(x=> x.PaymentHistory)
            .Include(x => x.Debt).FirstOrDefaultAsync(x => x.ContactNamber == number);
        if(entity == null)
            throw new Exception("No client found");
        return entity;
    }

    public async Task<List<Client>> GetClientByFullName(string fullname)
    {
        var entities = await _dbContext.Clients.AsNoTracking().ToListAsync();
        return entities;
    }

    public async Task<Client> GetClientByGovId(string govId)
    {
        var entitie = await _dbContext.Clients.FirstOrDefaultAsync(x=> x.GovernmentId == govId);
        if(entitie == null)
            throw new Exception("No client found");
        return entitie;
    }

    public async Task<List<Client>> SearchClient(string searchString, bool ascordesc)
    {
        searchString = searchString.ToLower();
        var clients = await _dbContext.Clients.AsNoTracking().Where(x=>  (x.GovernmentId.ToLower().Contains(searchString) || x.Address.ToLower().Contains(searchString) || x.Name.ToLower().Contains(searchString) || x.Surname.ToLower().Contains(searchString)) || (x.Name + x.Surname).ToLower().Contains(searchString))
            .ToListAsync();
        if (ascordesc)
        {
            var orderByDescending = clients.OrderByDescending(x => x.DateOfRegistration).ToList();
            return orderByDescending;
        }
        var orderByAscending = clients.OrderBy(x => x.DateOfRegistration).ToList();
        return orderByAscending;
        
    }
}