using BeratCRM.Entities;
using BeratCRM.Models;
using BeratCRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeratCRM.Controllers;

public class OrderController(OrderService orderService) : Controller
{
    private readonly OrderService _orderService = orderService;
    
    [HttpPost("api/order/create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
    {
        var order = await _orderService.CreateOrder(model);
        return Ok(order);
    }
    
    [HttpGet("api/orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetOrders();
        return Ok(orders);
    }

    [HttpGet("api/order/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        try
        {
            var order = await _orderService.GetOrder(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPut("api/order/{id}/updategeninfo")]
    public async Task<IActionResult> UpdateGeneralInfo(Guid id,[FromBody] UpdateOrderGeneralInfoModel model)
    {
        try
        {
            var response = await _orderService.UpdateGeneralInfo(id, model);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/order/{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {   
            var response = await _orderService.DeleteOrder(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/order/{clientId}/byclientid")]
    public async Task<IActionResult> GetOrderByCLientId(Guid clientId)
    {
        try
        {
            var response = await _orderService.GetOrdersByCustomerId(clientId);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}