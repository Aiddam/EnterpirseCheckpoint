using Autofac;
using EnterpirseCheckpoint.ViewModelParameters;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnterpirseCheckpoint.ViewModels
{
    public class ScheduleViewModel : ViewModelBaseWithParameters<User>
    {
        private User? _user = null;
        private readonly IEmployeeService _employeeService;
        private readonly IOrganizationService _organizationService;
        private readonly IComponentContext _componentContext;

        public ScheduleViewModel(IEmployeeService employeeService, IOrganizationService organizationService, IComponentContext componentContext)
        {
            _employeeService = employeeService;
            _organizationService = organizationService;
            _componentContext = componentContext;
        }

        public ObservableCollection<EmployeeDto> Employees { get; set; } = new ObservableCollection<EmployeeDto>();
        
        public override User? CurrentUser 
        { 
            get => _user; 
            set => _user = value; 
        }

        public override Task SetAdditionalParameter(User parameter)
        {
            _user = parameter;
            return Task.CompletedTask;
        }

        public async Task LoadEmployeesAsync()
        {
            while (true)
            {
                var organization = await _organizationService.GetOrganizationByUserAsync(_user!);
                Employees.Clear();
                foreach (var employee in await _employeeService.GetOrganizationEmployeesAsync(organization.Id))
                {
                    Employees.Add(employee);
                }
                await Task.Delay(2000);
            }
        }

        public void GoToScheduleSetUp(int employeeId)
        {
            var changeScheduleViewModel = _componentContext.Resolve<ChangeScheduleViewModel>();
            changeScheduleViewModel.SetAdditionalParameter(new ChangeScheduleParameter
            {
                CurrentUser = _user!,
                EmployeeId = employeeId
            });
            ChangeView(changeScheduleViewModel);
        }
    }
}
