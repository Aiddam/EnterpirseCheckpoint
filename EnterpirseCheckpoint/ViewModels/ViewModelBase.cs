using ReactiveUI;
using System;

namespace EnterpirseCheckpoint.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public event Action<ViewModelBase>? OnChangeViewModel;

    protected void ChangeView(ViewModelBase nextView)
    {
        OnChangeViewModel?.Invoke(nextView);
    }
}
