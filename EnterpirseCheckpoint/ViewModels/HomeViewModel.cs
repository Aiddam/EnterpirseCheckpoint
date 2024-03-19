using Autofac;
using EnterpriseCheckpoint.Models.Enum;
using EnterpriseCheckpoint.Models.Models;
using System;

namespace EnterpirseCheckpoint.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public ViewModelBase? ScheduleViewModel { get; set; }
    public ViewModelBase? AnalyticsViewModel { get; set; }
    public ViewModelBase? RegistrationViewModel { get; set; }

    private readonly User _user;
    public bool IsRegistrtionShow
    {
        get => _user.Role == UserRole.Owner;
    }

    public HomeViewModel(IComponentContext context, User user)
    {
        InitializeTabs(context, user);
        _user = user;
    }
    private void InitializeTabs(IComponentContext context, User user)
    {
        if (Enum.Equals(user.Role, UserRole.Owner))
        {
            RegistrationViewModel = new RegistrationViewModel(context, user);
        }
    }

}
