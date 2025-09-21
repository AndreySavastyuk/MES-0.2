using System.ComponentModel.DataAnnotations;

namespace MesApp.ViewModels;

public class LabTestResultViewModel
{
    [Required(ErrorMessage = "Укажите результат")]
    public bool Pass { get; set; }

    [Required(ErrorMessage = "Укажите номер протокола")]
    public string ProtocolNo { get; set; } = "";

    public string? Notes { get; set; }
}