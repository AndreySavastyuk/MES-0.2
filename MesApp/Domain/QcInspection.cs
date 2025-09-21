namespace MesApp.Domain;

public class QcInspection
{
    public int Id { get; set; }
    public int MaterialReceiptId { get; set; }
    public MaterialReceipt Receipt { get; set; } = null!;
    public bool RequiresUltrasonic { get; set; }
    public bool RequiresPpsd { get; set; }
    public QcStage Stage { get; set; }
    public Decision? FinalDecision { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Inspector { get; set; } = string.Empty;
}