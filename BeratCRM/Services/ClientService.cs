using BeratCRM.Entities;
using BeratCRM.Models;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class ClientService(IClientRepository clientRepository)
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<List<Client>> GetClients()
    {
        var clients = await _clientRepository.GetAllClients();
        return clients;
    }

    public async Task<Client> GetClient(Guid clientId)
    {
        var client = await _clientRepository.GetClientById(clientId);
        return client;
    }

    public async Task<string> CreateClient(CreateClientModel client)
    {
        var entity = new Client()
        {
            Id = Guid.NewGuid(),
            Name = client.Name,
            Surname = client.Surname,
            GovernmentId = client.GovernmentId,
            Address = client.Address,
            ContactNamber = client.ContactNamber,
            DateOfRegistration = DateTime.UtcNow
        };
        await _clientRepository.Create(entity);
        return entity.Id.ToString();
    }

    public async Task<string> UpdateGeneralInfo(Guid id, UpdateClientsGeneralInfo model)
    {
        var client = await _clientRepository.GetClientById(id);
        var checker = false;
        if (!string.IsNullOrEmpty(client.Name))
        {
            client.Name = model.Name;
            checker = true;
        }

        if (!string.IsNullOrEmpty(client.Surname))
        {
            client.Surname = model.Surname;
            checker = true;
        }

        if (!string.IsNullOrEmpty(client.Address))
        {
            client.Address = model.Address;
            checker = true;
        }

        if (checker)
            await _clientRepository.Update(client);
        return "Client Updated";
    }

    public async Task<string> UpdateGovId(Guid id, UpdateClientsGovId model)
    {
        if(string.IsNullOrEmpty(model.GovId))
            return "GovernmentId is empty";
        var client = await _clientRepository.GetClientById(id);
        client.GovernmentId = model.GovId;
        await _clientRepository.Update(client);
        return "Client Updated";
    }

    public async Task<string> UpdatesContactNumber(Guid id, UpdateContactInfo model)
    {
        if(string.IsNullOrEmpty(model.ContactNumber))
            return "ContactNumber is empty";
        var client = await _clientRepository.GetClientById(id);
        client.ContactNamber = model.ContactNumber;
        await _clientRepository.Update(client);
        return "Client Updated";
    }

    public async Task<string> DeleteClient(Guid id)
    {
        var client = await _clientRepository.GetClientById(id);
        await _clientRepository.Delete(id);
        return "Client Deleted";
    }
}