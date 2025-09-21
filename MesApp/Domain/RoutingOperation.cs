namespace MesApp.Domain;

public class RoutingOperation
{
    public int Id { get; set; }
    public int Seq { get; set; }
    public string Name { get; set; } = string.Empty;
    public int WorkCenterId { get; set; }
    public bool QcGate { get; set; }
}