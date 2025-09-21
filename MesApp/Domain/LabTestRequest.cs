namespace MesApp.Domain;

public class LabTestRequest
{
    public int Id { get; set; }
    public int MaterialReceiptId { get; set; }
    public MaterialReceipt Receipt { get; set; } = null!;
    public bool External { get; set; }
    public string? ExternalLabName { get; set; }
    public string Tests { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string RequestedBy { get; set; } = string.Empty;
}