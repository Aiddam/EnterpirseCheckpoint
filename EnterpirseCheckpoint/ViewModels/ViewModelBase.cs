using ReactiveUI;
using System;
using System.Collections.Generic;

namespace EnterpirseCheckpoint.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public event Action<ViewModelBase>? OnChangeViewModel;

    protected void ChangeView(ViewModelBase nextView)
    {
        OnChangeViewModel?.Invoke(nextView);
    }

    protected IEnumerable<Delegate> GetDelegate()
    {
        return OnChangeViewModel?.GetInvocationList() ?? new Delegate[0];
    }
}
