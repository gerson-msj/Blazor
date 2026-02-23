using Blazor.Server.Models;

namespace Blazor.Server.Services;

public class PopupService
{
    public event Action<PopupRequestModel>? OnOpen;
    public event Action<string>? OnClose;
    public event Action<string>? OnBackdropClick;

    public void Open(PopupRequestModel request)
    {
        OnOpen?.Invoke(request);
    }

    public void Close(bool isBackdropClick = false, string popupName = "default")
    {
        OnClose?.Invoke(popupName);
        if (isBackdropClick)
            OnBackdropClick?.Invoke(popupName);
    }
}
