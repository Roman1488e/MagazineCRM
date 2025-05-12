using BeratCRM.DTOs;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class ReminderService(IOrderRepository orderRepository)
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<List<ReminderDto>?> GetReminders()
    {
        var orders = await _orderRepository.GetAllOrders();
        if (!orders.Any(x => x.IsReminding))
            return null; 
        var remindingOrders = orders.Where(x => x.IsReminding).ToList();
        var reminderDtos = new List<ReminderDto>();
        foreach (var order in remindingOrders)
        {
            if (DateTime.UtcNow.AddDays(3) <= order.ReminderDate)
            {
                order.IsReminding = true;
                await _orderRepository.Update(order);
            }
            var reminderDto = new ReminderDto()
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                ReminderDate = order.ReminderDate,
            };
            reminderDtos.Add(reminderDto);
        }
        return reminderDtos;
    }
}