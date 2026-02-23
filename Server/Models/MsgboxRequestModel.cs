namespace Blazor.Server.Models;

public class MsgboxRequestModel
{
    public string MsgboxName { get; set; } = "default";
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? Ok { get; set; }
    public string? Cancel { get; set; }
    public int Width { get; set; } = 400;
}
