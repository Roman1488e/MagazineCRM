using BeratCRM.Entities;
using BeratCRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Controllers;

public class PaymentHistoryController(PaymentHistoryService paymentHistoryService, StatisticsService service) : Controller
{
    private readonly PaymentHistoryService _paymentHistoryService = paymentHistoryService;
    private readonly StatisticsService _service = service;

    [HttpGet("api/paymenthistory")]
    public async Task<IActionResult> GetPaymentHistories()
    {
        var ph = await _paymentHistoryService.GetAllPaymentHistory();
        return Ok(ph);
    }

    [HttpGet("api/paymenthistory/{id}")]
    public async Task<IActionResult> GetPaymentHistory(Guid id)
    {
        try
        {
            var ph = await _paymentHistoryService.GetPaymentHistory(id);
            return Ok(ph);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/paymenthistory/{clientId}")]
    public async Task<IActionResult> GetPaymentHistoryByClientId(Guid clientId)
    {
        var ph = await _paymentHistoryService.GetPaymentHistoryByClientId(clientId);
        return Ok(ph);
    }

    [HttpGet("api/paymenthistory/{orderId}")]
    public async Task<IActionResult> GetPaymentHistoryByOrderId(Guid orderId)
    {
        var ph = await _paymentHistoryService.GetPaymentHistoryByOrderId(orderId);
        return Ok(ph);
    }

    [HttpGet("api/paymenthistory/statistic")]
    public async Task<IActionResult> Statistics()
    {
        return Ok(await _service.ShowPaymentStatistic());
    }
}