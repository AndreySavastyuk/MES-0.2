using System.ComponentModel.DataAnnotations;

namespace MesApp.ViewModels;

public class PrepJobViewModel
{
    [Required(ErrorMessage = "Выберите материал")]
    public int MaterialReceiptId { get; set; }

    [Required(ErrorMessage = "Выберите тип")]
    public string Kind { get; set; } = "";

    public string? Notes { get; set; }
}