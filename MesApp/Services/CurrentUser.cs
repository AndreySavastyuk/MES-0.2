namespace MesApp.Services;

public class CurrentUser
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsAuthenticated => !string.IsNullOrEmpty(Name);
}