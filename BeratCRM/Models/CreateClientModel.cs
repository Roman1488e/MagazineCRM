namespace BeratCRM.Models;

public class CreateClientModel
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string? ContactNumber { get; set; }
    public string GovernmentId { get; set; }
    public string Address { get; set; }
}