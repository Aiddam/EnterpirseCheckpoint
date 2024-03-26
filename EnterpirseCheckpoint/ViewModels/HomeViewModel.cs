using Autofac;
using EnterpriseCheckpoint.Models.Enum;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace EnterpirseCheckpoint.ViewModels;

public class HomeViewModel : ViewModelBaseWithParameters<User>
{
    public ViewModelBaseWithParameters<User>? ScheduleViewModel { get; set; }
    public ViewModelBaseWithParameters<User>? AnalyticsViewModel { get; set; }
    public ViewModelBaseWithParameters<User>? RegistrationViewModel { get; set; }

    private User? _user = null!;
    private readonly IComponentContext _context;

    public bool IsRegistrtionShow
    {
        get => _user!.Role == UserRole.Owner;
    }

    public override User? CurrentUser 
    {
        get => _user; 
        set => _user = value; 
    }

    public HomeViewModel(IComponentContext context)
    {
        _context = context;
    }

    public async Task InitializeTabs()
    {
        if (Enum.Equals(_user!.Role, UserRole.Owner))
        {
            RegistrationViewModel = _context.Resolve<RegistrationViewModel>();
            await RegistrationViewModel.SetAdditionalParameter(_user);
            foreach (var dlgt in GetDelegate())
            {
                RegistrationViewModel.OnChangeViewModel += (Action<ViewModelBase>)dlgt;
            }

            ScheduleViewModel = _context.Resolve<ScheduleViewModel>();
            await ScheduleViewModel.SetAdditionalParameter(_user);
            foreach (var dlgt in GetDelegate())
            {
                ScheduleViewModel.OnChangeViewModel += (Action<ViewModelBase>)dlgt;
            }
        }
        else
        {
            var employeeViewModel = _context.Resolve<EmployeeViewModel>();
            await employeeViewModel.SetAdditionalParameter(_user);
            ChangeView(employeeViewModel);
        }
    }

    public override Task SetAdditionalParameter(User parameter)
    {
        _user = parameter;
        return Task.CompletedTask;
    }
}
