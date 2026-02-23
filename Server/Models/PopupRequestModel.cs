namespace Blazor.Server.Models;

public class PopupRequestModel
{
    public int Width { get; set; } = 400;
    public string PopupName { get; set; } = "default";
    public bool DisableBackdrop { get; set; }
}
