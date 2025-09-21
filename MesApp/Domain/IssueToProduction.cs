namespace MesApp.Domain;

public class IssueToProduction
{
    public int Id { get; set; }
    public int ReceiptId { get; set; }
    public MaterialReceipt Receipt { get; set; } = null!;
    public decimal Qty { get; set; }
    public DateTime IssuedAt { get; set; }
    public string IssuedBy { get; set; } = string.Empty;
}