namespace MesApp.Services;

public static class UserService
{
    public static readonly (string Name, string Role)[] Users = new[]
    {
        ("Иванов (Кладовщик)", "WAREHOUSE"),
        ("Петров (ОТК)", "QC"),
        ("Сидоров (ЦЗЛ)", "LAB"),
        ("Кузнецов (ОПП)", "OPP")
    };
}