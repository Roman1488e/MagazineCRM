using BeratCRM.DTOs;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class ReminderService(IOrderRepository orderRepository, IClientRepository clientRepository, IDebtRepository debtRepository)
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IDebtRepository _debtRepository = debtRepository;

    public async Task<List<ReminderDto>?> GetReminders()
    {
        var orders = await _orderRepository.GetAllOrders();
        if (!orders.Any(x => x.IsReminding))
            return null; 
        var remindingOrders = orders.Where(x => x.IsReminding).ToList();
        var clients = await _clientRepository.GetAllClients();
        var reminderDtos = new List<ReminderDto>();
        foreach (var order in remindingOrders)
        {
            if (DateTime.UtcNow.AddDays(3) >= order.ReminderDate)
            {
                order.IsReminding = true;
                await _orderRepository.Update(order);
            }
            var client = clients.First(x => x.Id == order.ClientId);
            var reminderDto = new ReminderDto()
            {
                ClientsFullName = client.Name + " " + client.Surname,
                ClientsNumber = client.ContactNamber,
                ProductName = order.ProductName,
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                ReminderDate = order.ReminderDate,
            };
            reminderDtos.Add(reminderDto);
        }
        return reminderDtos;
    }

    public async Task<List<DebtPaymentReminderDto>?> GetDebtPaymentReminders()
    {
        var debts = await _debtRepository.GetAllDebts();
        var reminderDtos = new List<DebtPaymentReminderDto>();
        var clients = await _clientRepository.GetAllClients();
        var remindingDebts = debts.Where(x=> x.LastPaymentDate >= DateTime.UtcNow.AddDays(27) && !x.isPaid).ToList();
        foreach (var debt in remindingDebts)
        {
            var client = clients.First(x => x.Id == debt.ClientId);
            var reminderDto = new DebtPaymentReminderDto()
            {
                Amount = debt.Amount,
                ClientFullName = client.Name + " " + client.Surname,
                LastPaymentDate = debt.LastPaymentDate,
                ContactNum = client.ContactNamber,
                LastPaymentAmount = debt.LastPaymentAmount,
            };
            reminderDtos.Add(reminderDto);
        }
        return reminderDtos;
    }

    public async Task MarkAsChecked(Guid orderId)
    {
        var order = await _orderRepository.GetByOrderId(orderId);
        order.IsReminding = false;
        order.ReminderDate = order.ReminderDate?.AddMonths(order.RemindInMounts);
        await _orderRepository.Update(order);
    }
}