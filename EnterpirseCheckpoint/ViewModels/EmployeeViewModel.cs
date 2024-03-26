using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class EmployeeViewModel : ViewModelBaseWithParameters<User>
    {
        private Employee _currentEmployee = null!;
        private readonly IEmployeeAssignmentService _employeeAssignmentService;

        private bool _isNone = false;
        public bool IsNone 
        {
            get => _isNone;
            set
            {
                this.RaiseAndSetIfChanged(ref _isNone, value);
            }
        }
        private bool _isEnter = false;
        public bool IsEnter 
        {
            get => _isEnter;
            set 
            {
                this.RaiseAndSetIfChanged(ref _isEnter, value);
            }
        }
        private bool _isLeave = false;
        public bool IsLeave
        {
            get => _isLeave;
            set
            {
                this.RaiseAndSetIfChanged(ref _isLeave, value);
            }
        }
        private bool _isNoWorkDay = false;
        public bool IsNoWorkDay 
        {
            get => _isNoWorkDay;
            set 
            {
                this.RaiseAndSetIfChanged(ref _isNoWorkDay, value);
            }
        }

        public ICommand EnterCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        private User? _currentUser = null;
        public override User? CurrentUser
        { 
            get => _currentUser;
            set => _currentUser = value;
        }

        public EmployeeViewModel(IEmployeeAssignmentService employeeAssignmentService)
        {
            _employeeAssignmentService = employeeAssignmentService;

            EnterCommand = ReactiveCommand.Create(EnterAsync);
            ExitCommand = ReactiveCommand.Create(ExitAsync);
        }

        public override async Task SetAdditionalParameter(User parameter)
        {
            _currentUser = parameter;
            _currentEmployee = parameter.Employee;

            var isWorkDay = await _employeeAssignmentService.IsWorkDayAsync(_currentEmployee.Id);
            if (!isWorkDay)
            {
                IsNoWorkDay = true;
                return;
            }

            var isEntered = await _employeeAssignmentService.IsEnterAssignmentExistsAsync(_currentEmployee.Id);
            var isLeaved = await _employeeAssignmentService.IsExitAssignmentExistsAsync(_currentEmployee.Id);

            if (isEntered)
            {
                IsEnter = true;
            } 
            else if(isLeaved) 
            {
                IsLeave = true;
            }
            else
            {
                IsNone = true;
            }
        }

        public async Task EnterAsync()
        {
            await _employeeAssignmentService.CreateEnterEmployeeAssignmentAsync(_currentEmployee.Id);
            IsEnter = true;
            IsNone = false;
        }

        public async Task ExitAsync()
        {
            await _employeeAssignmentService.CreateExitEmployeeAssignmentAsync(_currentEmployee.Id);
            IsLeave = true;
            IsEnter = false;
        }
    }
}
