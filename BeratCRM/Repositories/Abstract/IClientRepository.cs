using BeratCRM.Entities;

namespace BeratCRM.Repositories.Abstract;

public interface IClientRepository : IBaseRepository<Client>
{
    public Task<Client> GetClientById(Guid id);
    public Task<List<Client>> GetAllClients();
    public Task<Client> GetClientByNumber(string number);
    public Task<List<Client>> GetClientByFullName(string number);
    public Task<Client> GetClientByGovId(string govId);
    
    public Task<List<Client>> SearchClient(string searchString, bool ascordesc);
    
}