namespace Blazor.Server.Models;

public enum MsgboxResult
{
    None,
    Ok,
    Cancel
}

public record MsgboxResultModel(string MsgboxName, MsgboxResult MsgboxResult);
