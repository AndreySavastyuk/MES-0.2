namespace MesApp.Services;

public class NotificationService
{
    public List<NotificationItem> Notifications { get; } = new();
    public event Action? OnNotificationsChanged;

    public void ShowSuccess(string message)
    {
        AddNotification(message, "success");
    }

    public void ShowWarning(string message)
    {
        AddNotification(message, "warning");
    }

    public void ShowError(string message)
    {
        AddNotification(message, "danger");
    }

    public void ShowInfo(string message)
    {
        AddNotification(message, "info");
    }

    private void AddNotification(string message, string type)
    {
        var notification = new NotificationItem
        {
            Id = Guid.NewGuid().ToString(),
            Message = message,
            Type = type,
            Timestamp = DateTime.Now
        };

        Notifications.Add(notification);
        OnNotificationsChanged?.Invoke();

        // Автоудаление через 5 секунд
        Task.Delay(5000).ContinueWith(_ => RemoveNotification(notification.Id));
    }

    public void RemoveNotification(string id)
    {
        var notification = Notifications.FirstOrDefault(n => n.Id == id);
        if (notification != null)
        {
            Notifications.Remove(notification);
            OnNotificationsChanged?.Invoke();
        }
    }
}

public class NotificationItem
{
    public string Id { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}