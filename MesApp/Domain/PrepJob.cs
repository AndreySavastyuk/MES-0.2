namespace MesApp.Domain;

public class PrepJob
{
    public int Id { get; set; }
    public PrepKind Kind { get; set; }
    public PrepStatus Status { get; set; }
    public int? MaterialReceiptId { get; set; }
    public MaterialReceipt? Receipt { get; set; }
    public string? Notes { get; set; }
    public string Owner { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}