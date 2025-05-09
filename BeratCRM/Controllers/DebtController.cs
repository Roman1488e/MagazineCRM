using BeratCRM.Repositories.Abstract;
using BeratCRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Controllers;

public class DebtController(DebtService service, SearchService searchService) : Controller
{
    private readonly DebtService _service = service;
    private readonly SearchService _searchService = searchService;
    [HttpGet("api/debt/{id}")]
    public async Task<IActionResult> GetDebt(Guid id)
    {
        try
        {
            var clients = await _service.GetDebt(id);
            return Ok(clients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/debt")]
    public async Task<IActionResult> GetDebts()
    {
        try
        {
            var debts = await _service.GetAllDebts();
            return Ok(debts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/debt/search")]
    public async Task<IActionResult> SearchDebts()
    {
        return Ok(await _searchService.SearchDebts());
    }

    [HttpPut("api/debt/{debtid}/payfordebt")]
    public async Task<IActionResult> PayforDebt(Guid debtid, decimal amount)
    {
        var result = await _service.PayDebt(debtid, amount);
        return Ok(result);
    }
    
}