using Blazor.Server.Models;

namespace Blazor.Server.Services;

public class MsgboxService
{
    public event Action<MsgboxRequestModel, TaskCompletionSource<MsgboxResultModel>>? OnOpen;
    private TaskCompletionSource<MsgboxResultModel>? tcs;

    public Task<MsgboxResultModel> Open(MsgboxRequestModel request)
    {
        tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
        OnOpen?.Invoke(request, tcs);
        return tcs.Task;
    }

    // public void Close(MsgboxResultModel result)
    // {
    //     var tcs = this.tcs;
    //     this.tcs = null;
    //     tcs?.TrySetResult(result);
    //     // OnClose?.Invoke(result.MsgboxName);
    // }
}
