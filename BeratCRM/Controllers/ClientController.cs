using BeratCRM.Entities;
using BeratCRM.Models;
using BeratCRM.Repositories;
using BeratCRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Controllers;

public class ClientController(ClientService service, SearchService searchService) : Controller
{
    private readonly ClientService _service = service;
    private readonly SearchService _searchService = searchService;

    [HttpPost("/api/clients/create")]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientModel model)
    {
        var client = await _service.CreateClient(model);
        return Ok(client);
    }
    
    [HttpGet("/api/clients/search/{searchString}/{ascordesc}")]
    public async Task<List<Client>> SearchClient(string searchString, bool ascordesc)
    {
        var clients = await _searchService.Search(searchString, ascordesc);
        return clients;
    }

    [HttpGet("/api/clients/{id}")]
    public async Task<IActionResult> GetClientById(Guid id)
    {
        var client = await _service.GetClient(id);
        return Ok(client);
    }

    [HttpGet("/api/clients")]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _service.GetClients();
        return Ok(clients);
    }
    
    [HttpPost("/api/clients/delete/{id}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        var client = await _service.DeleteClient(id);
        return Ok(client);
    }

    [HttpPut("/api/clients/update/{id}/governmentId")]
    public async Task<IActionResult> UpdateGovId(Guid id, [FromBody] UpdateClientsGovId model)
    {
        var client = await _service.UpdateGovId(id, model);
        return Ok(client);
    }

    [HttpPut("/api/clients/update/{id}/generalInfo")]
    public async Task<IActionResult> UpdateGeneralInfo(Guid id,[FromBody] UpdateClientsGeneralInfo model)
    {
        var client = await _service.UpdateGeneralInfo(id, model);
        return Ok(client);
    }

    [HttpPut("/api/clients/update/{id}/contactInfo")]
    public async Task<IActionResult> UpdateContactNumber(Guid id,[FromBody] UpdateContactInfo model)
    {
        var clients = await _service.UpdatesContactNumber(id, model);
        return Ok(clients);
    }
}