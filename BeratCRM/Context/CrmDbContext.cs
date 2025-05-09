using BeratCRM.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeratCRM.Context;

public class CrmDbContext(DbContextOptions<CrmDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentHistory> PaymentHistories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Debt> Debts { get; set; }
}