using System.Threading.Tasks;

namespace EnterpirseCheckpoint.ViewModels;

public abstract class ViewModelBaseWithParameters<T> : ViewModelBase
{
    public abstract Task SetAdditionalParameter(T parameter);
}
