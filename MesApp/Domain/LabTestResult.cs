namespace MesApp.Domain;

public class LabTestResult
{
    public int Id { get; set; }
    public int LabTestRequestId { get; set; }
    public LabTestRequest Request { get; set; } = null!;
    public bool Pass { get; set; }
    public string? ProtocolNo { get; set; }
    public string? ReportFilePath { get; set; }
    public DateTime CompletedAt { get; set; }
}