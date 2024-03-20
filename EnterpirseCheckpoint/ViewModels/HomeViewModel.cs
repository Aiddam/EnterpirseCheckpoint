using Autofac;
using EnterpriseCheckpoint.Models.Enum;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;

namespace EnterpirseCheckpoint.ViewModels;

public class HomeViewModel : ViewModelBaseWithParameters<User>
{
    public ViewModelBaseWithParameters<User>? ScheduleViewModel { get; set; }
    public ViewModelBaseWithParameters<User>? AnalyticsViewModel { get; set; }
    public ViewModelBaseWithParameters<User>? RegistrationViewModel { get; set; }

    private User _user = null!;
    private readonly IComponentContext _context;

    public bool IsRegistrtionShow
    {
        get => _user.Role == UserRole.Owner;
    }

    public HomeViewModel(IComponentContext context)
    {
        _context = context;
    }

    public void InitializeTabs()
    {
        if (Enum.Equals(_user.Role, UserRole.Owner))
        {
            RegistrationViewModel = _context.Resolve<RegistrationViewModel>();
            RegistrationViewModel.SetAdditionalParameter(_user);
            foreach (var dlgt in GetDelegate())
            {
                RegistrationViewModel.OnChangeViewModel += (Action<ViewModelBase>)dlgt;
            }

            ScheduleViewModel = _context.Resolve<ScheduleViewModel>();
            ScheduleViewModel.SetAdditionalParameter(_user);
            foreach (var dlgt in GetDelegate())
            {
                ScheduleViewModel.OnChangeViewModel += (Action<ViewModelBase>)dlgt;
            }
        }
    }

    public override void SetAdditionalParameter(User parameter)
    {
        _user = parameter;
    }
}
