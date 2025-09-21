namespace MesApp.Domain;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ItemType Type { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? Spec { get; set; }
}