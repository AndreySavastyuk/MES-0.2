using System.ComponentModel.DataAnnotations;
using MesApp.Domain;

namespace MesApp.ViewModels;

public class QcInspectViewModel
{
    public bool RequiresUltrasonic { get; set; }
    public bool RequiresPpsd { get; set; }

    public Decision? Decision { get; set; }
    public bool ToLabSelected { get; set; }

    public string LabTests { get; set; } = "CHEM,HARDNESS,UT";
    public string Remarks { get; set; } = "";
}