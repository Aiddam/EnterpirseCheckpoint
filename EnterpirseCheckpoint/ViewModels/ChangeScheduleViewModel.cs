using Autofac;
using EnterpirseCheckpoint.ViewModelParameters;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class ChangeScheduleViewModel : ViewModelBaseWithParameters<ChangeScheduleParameter>
    {
        private readonly IComponentContext _componentContext;
        private readonly IEmployeeService _employeeService;
        private int _employeeId;

        private User _currentUser = null!;
        private Employee? _employee = null!;

        private int _startHour;
        private int _endHour;
        private int _startMinute;
        private int _endMinute;

        public ChangeScheduleViewModel(IComponentContext componentContext, IEmployeeService employeeService)
        {
            _componentContext = componentContext;
            _employeeService = employeeService;

            AddScheduleCommand = ReactiveCommand.Create(SetupScheduleAsync);
        }

        public int StartHour
        {
            get => _startHour;
            set
            {
                if (value > 23)
                {
                    this.RaiseAndSetIfChanged(ref _startHour, 23);
                } 
                else if(value < 0)
                {
                    this.RaiseAndSetIfChanged(ref _startHour, 0);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref _startHour, value);
                }
            }
        }

        public int StartMinute
        {
            get => _startMinute;
            set
            {
                if (value > 59)
                {
                    this.RaiseAndSetIfChanged(ref _startMinute, 59);
                }
                else if (value < 0)
                {
                    this.RaiseAndSetIfChanged(ref _startMinute, 0);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref _startMinute, value);
                }
            }
        }

        public int EndHour
        {
            get => _endHour;
            set
            {
                if (value > 23)
                {
                    this.RaiseAndSetIfChanged(ref _endHour, 23);
                }
                else if (value < 0)
                {
                    this.RaiseAndSetIfChanged(ref _endHour, 0);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref _endHour, value);
                }
            }
        }

        public int EndMinute
        {
            get => _endMinute;
            set
            {
                if (value > 59)
                {
                    this.RaiseAndSetIfChanged(ref _endMinute, 59);
                }
                else if (value < 0)
                {
                    this.RaiseAndSetIfChanged(ref _endMinute, 0);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref _endMinute, value);
                }
            }
        }

        public ICommand AddScheduleCommand { get; set; }

        public async Task LoadEmployeeAsync()
        {
            _employee = await _employeeService.GetEmployeeByIdAsync(_employeeId);

            if (_employee is null)
            {
                GoBack();
                return;
            }

            StartHour = _employee.Start?.Hour ?? 0;
            StartHour = _employee.Start?.Minute ?? 0;
            StartHour = _employee.End?.Hour ?? 0;
            StartHour = _employee.End?.Minute ?? 0;
        }

        public async Task SetupScheduleAsync()
        {
            _employee!.Start = new DateTime(1, 1, 1, StartHour, StartMinute, 0, 0);
            _employee.End = new DateTime(1, 1, 1, EndHour, EndMinute, 0, 0);

            await _employeeService.UpdateEmployeeAsync(_employee);

            GoBack();
        }

        public void GoBack()
        {
            var homeViewModel = _componentContext.Resolve<HomeViewModel>();
            homeViewModel.SetAdditionalParameter(_currentUser);
            ChangeView(homeViewModel);
            homeViewModel.InitializeTabs();
        }

        public override void SetAdditionalParameter(ChangeScheduleParameter parameter)
        {
            _employeeId = parameter.EmployeeId;
            _currentUser = parameter.CurrentUser;
        }
    }
}
