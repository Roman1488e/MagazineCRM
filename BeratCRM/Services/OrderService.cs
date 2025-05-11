using BeratCRM.Entities;
using BeratCRM.Models;
using BeratCRM.Repositories.Abstract;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BeratCRM.Services;

public class OrderService(IOrderRepository orderRepository, IDebtRepository debtRepository, IPaymentHistoryRepository paymentHistoryRepository, IClientRepository clientRepository)
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IDebtRepository _debtRepository = debtRepository;
    private readonly IPaymentHistoryRepository _paymentHistoryRepository = paymentHistoryRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        return orders;
    }

    public async Task<Order> GetOrder(Guid id)
    {
        var order = await _orderRepository.GetByOrderId(id);
        return order;
    }

    public async Task<Order> CreateOrder(CreateOrderModel model)
    {
        var client = await _clientRepository.GetClientById(model.CustomerId);
        var orders = await _orderRepository.GetAllOrders();
        var order = new Order()
        {
            OrderNumber = orders.Max(x=> x.OrderNumber)+1,
            CustomerId = model.CustomerId,
            ProductName = model.ProductName,
            CustomerFullName = $"{client.Name} - {client.Surname}",
            TotalPrice = model.TotalPrice,
            OrderDate = DateTime.UtcNow,
            PaidAmount = model.PaidAmount,
            ReminderDate = DateTime.UtcNow.AddMonths(model.RemindInMonths),
            IsPaid = (model.TotalPrice - model.PaidAmount) == 0
        };
        await _orderRepository.Create(order);
        var realOrder = await _orderRepository.GetByOrderId(order.OrderId);

        if (!realOrder.IsPaid)
        {
            var debt = new Debt()
            {
                CreationDate = DateTime.UtcNow,
                LastPaymentDate = DateTime.UtcNow,
                Amount = order.TotalPrice - model.PaidAmount,
                ClientId = order.CustomerId,
                LastPaymentAmount = model.PaidAmount,
                OrderId = realOrder.OrderId
            };
            await _debtRepository.Create(debt);
        }

        var paymentHist = new PaymentHistory()
        {
            ClientId = order.CustomerId,
            PaymentDate = DateTime.UtcNow,
            PaymentAmount = order.PaidAmount,
            OrderId = realOrder.OrderId
        };
        await _paymentHistoryRepository.Create(paymentHist);
        return realOrder;
    }

    public async Task<Order> UpdateGeneralInfo(Guid id, UpdateOrderGeneralInfoModel model)
    {
        var order = await _orderRepository.GetByOrderId(id);
        if (model.ProductName is not null)
            order.ProductName = model.ProductName;
        if (model.ReminderDate is not null && model.ReminderDate > DateTime.UtcNow)
            order.ReminderDate = model.ReminderDate.Value;
        await _orderRepository.Update(order);
        return order;
    }

    public async Task<string> DeleteOrder(Guid id)
    {
        await _orderRepository.Delete(id);
        return "Order deleted";
    }

    public async Task<List<Order>> GetOrdersByCustomerId(Guid customerId)
    {
        var orders = await _orderRepository.GetOrdersByClientId(customerId);
        return orders;
    }
}