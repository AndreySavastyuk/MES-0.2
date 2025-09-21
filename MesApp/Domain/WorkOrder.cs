namespace MesApp.Domain;

public class WorkOrder
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public decimal Qty { get; set; }
    public DateTime DueDate { get; set; }
    public WoStatus Status { get; set; }
}