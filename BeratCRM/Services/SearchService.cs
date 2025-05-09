using BeratCRM.DTOs;
using BeratCRM.Entities;
using BeratCRM.Repositories.Abstract;

namespace BeratCRM.Services;

public class SearchService(IClientRepository clientRepository, IOrderRepository orderRepository, IDebtRepository debtRepository)
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IDebtRepository _debtRepository = debtRepository;

    public async Task<List<Client>> Search(string searchString, bool ascordesc)
    {
        var clients = await _clientRepository.SearchClient(searchString, ascordesc);
        return clients;
    }

    public async Task<List<DebtDto>> SearchDebts()
    {
        var debts = await _debtRepository.GetAllDebts();
        var clients =await _clientRepository.GetAllClients();
        var orders = await _orderRepository.GetAllOrders();
        var debtDtos = new List<DebtDto>();
        foreach (var debt in debts)
        {
            var debtDto = new DebtDto()
            {
                amount = debt.Amount,
                lastPaymentDate = debt.LastPaymentDate,
                lastPaymentAmount = debt.LastPaymentAmount,
                clientFullName = clients.First(x => x.Id == debt.ClientId).Name,
                productName = orders.First(x => x.OrderId == debt.OrderId).ProductName
            };
            debtDtos.Add(debtDto);
        }
        return debtDtos;
    }
}