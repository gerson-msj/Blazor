using System;
using Blazor.Server.Models;

namespace Blazor.Server.Services;

public class MsgboxService
{
    public MsgboxRequest? Request { get; private set; }

    public event Action? Changed;
    private TaskCompletionSource<MsgboxResult>? tcs;

    public Task<MsgboxResult> Show(MsgboxRequest request)
    {
        Request = request;
        tcs = new(TaskCreationOptions.RunContinuationsAsynchronously);
        Changed?.Invoke();
        return tcs.Task;
    }

    public void Close()
    {
        var tcs = this.tcs;
        this.tcs = null;
        Request = null;
        tcs?.TrySetResult(MsgboxResult.ok);
        Changed?.Invoke();
    }

    public string Teste { get; set; } = string.Empty;
}
