namespace MesApp.Domain;

public class MaterialReceipt
{
    public int Id { get; set; }
    public int? SupplierId { get; set; }
    public BusinessPartner? Supplier { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public decimal Qty { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? CertNumber { get; set; }
    public string? HeatNumber { get; set; }
    public string? Grade { get; set; }
    public string? Size { get; set; }
    public DateTime ReceivedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public ReceiptStatus Status { get; set; }
    public string? CertFilePath { get; set; }
    public decimal AllocatedQty { get; set; } = 0;
}