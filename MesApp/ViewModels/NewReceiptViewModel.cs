using System.ComponentModel.DataAnnotations;

namespace MesApp.ViewModels;

public class NewReceiptViewModel
{
    [Required(ErrorMessage = "Выберите материал")]
    public int? ItemId { get; set; }

    public int? SupplierId { get; set; }

    [Required(ErrorMessage = "Укажите количество")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
    public decimal Qty { get; set; }

    [Required(ErrorMessage = "Укажите единицу измерения")]
    public string Unit { get; set; } = "";

    public string? HeatNumber { get; set; }
    public string? Grade { get; set; }
    public string? Size { get; set; }
    public string? CertNumber { get; set; }
}