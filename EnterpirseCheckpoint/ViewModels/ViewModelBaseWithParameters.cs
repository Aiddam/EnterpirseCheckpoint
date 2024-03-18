using ReactiveUI;

namespace EnterpirseCheckpoint.ViewModels;

public class ViewModelBaseWithParameters<T> : ViewModelBase
{
    protected T? _parameter;

    public void SetAdditionalParameter(T parameter)
    {
        _parameter = parameter;
    }
}
