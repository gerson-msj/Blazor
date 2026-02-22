using System;
using Microsoft.AspNetCore.Components;

namespace Blazor.Server.Models;

public enum MsgboxResult
{
    none,
    ok,
    cancel
}

public record MsgboxRequest(
    string? Title = null,
    string? MsgText = null,
    RenderFragment? MsgFragment = null
);
