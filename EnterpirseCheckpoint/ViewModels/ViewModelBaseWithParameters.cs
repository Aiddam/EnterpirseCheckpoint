namespace EnterpirseCheckpoint.ViewModels;

public abstract class ViewModelBaseWithParameters<T> : ViewModelBase
{
    public abstract void SetAdditionalParameter(T parameter);
}
