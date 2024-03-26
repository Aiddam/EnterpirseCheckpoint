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

        private User? _currentUser = null;
        private Employee? _employee = null!;

        private int _startHour;
        private int _endHour;
        private int _startMinute;
        private int _endMinute;

        private int? _startWeekDay;
        private int? _endWeekDay;

        public ChangeScheduleViewModel(IComponentContext componentContext, IEmployeeService employeeService)
        {
            _componentContext = componentContext;
            _employeeService = employeeService;

            AddScheduleCommand = ReactiveCommand.Create(SetupScheduleAsync);
        }

        public int? StartWeekDay
        {
            get => _startWeekDay;
            set
            {
                this.RaiseAndSetIfChanged(ref _startWeekDay, value);
            }
        }

        public int? EndWeekDay
        {
            get => _endWeekDay;
            set
            {
                this.RaiseAndSetIfChanged(ref _endWeekDay, value);
            }
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

        public override User? CurrentUser 
        { 
            get => _currentUser;
            set => _currentUser = value;
        }

        public async Task LoadEmployeeAsync()
        {
            _employee = await _employeeService.GetEmployeeByIdAsync(_employeeId);

            if (_employee is null)
            {
                await GoBack();
                return;
            }

            StartHour = _employee.Start?.Hour ?? 0;
            StartHour = _employee.Start?.Minute ?? 0;
            StartHour = _employee.End?.Hour ?? 0;
            StartHour = _employee.End?.Minute ?? 0;

            StartWeekDay = _employee.DayOfWeekStart;
            EndWeekDay = _employee.DayOfWeekEnd;
        }

        public async Task SetupScheduleAsync()
        {
            _employee!.Start = new DateTime(1, 1, 1, StartHour, StartMinute, 0, 0);
            _employee.End = new DateTime(1, 1, 1, EndHour, EndMinute, 0, 0);
            _employee.DayOfWeekEnd = EndWeekDay;
            _employee.DayOfWeekStart = StartWeekDay;

            await _employeeService.UpdateEmployeeAsync(_employee);

            await GoBack();
        }

        public async Task GoBack()
        {
            var homeViewModel = _componentContext.Resolve<HomeViewModel>();
            await homeViewModel.SetAdditionalParameter(_currentUser!);
            ChangeView(homeViewModel);
            await homeViewModel.InitializeTabs();
        }

        public override Task SetAdditionalParameter(ChangeScheduleParameter parameter)
        {
            _employeeId = parameter.EmployeeId;
            _currentUser = parameter.CurrentUser;

            return Task.CompletedTask;
        }
    }
}
