﻿namespace BeratCRM.DTOs;

public class ReminderDto
{
    public Guid OrderId { get; set; }
    public DateTime? ReminderDate { get; set; }
    public int OrderNumber { get; set; }
    public string ClientsFullName { get; set; }
    public string? ClientsNumber { get; set; }
    public string ProductName { get; set; }
}