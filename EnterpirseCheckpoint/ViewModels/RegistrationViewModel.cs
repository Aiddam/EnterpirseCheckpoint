using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Enum;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class RegistrationViewModel : ViewModelBaseWithParameters<User>
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmployeeService _employeeService;

        private User? _user = null;

        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _role = string.Empty;
        private string _message = string.Empty;

        public RegistrationViewModel(IUserService userService, IOrganizationService organizationService, IEmployeeService employeeService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _employeeService = employeeService;
            RegistrationCommand = ReactiveCommand.CreateFromTask(RegistrationCommandHandler);
        }

        public event Func<Task>? UserCreated;

        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public string Password
        {
            get => _password;
            set
            {
                this.RaiseAndSetIfChanged(ref _password, value);
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                this.RaiseAndSetIfChanged(ref _login, value);
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                this.RaiseAndSetIfChanged(ref _surname, value);
            }
        }
        public string Role
        {
            get => _role;
            set
            {
                this.RaiseAndSetIfChanged(ref _role, value);
            }
        }
        public ICommand RegistrationCommand { get; }
        public override User? CurrentUser
        { 
            get => _user;
            set => _user = value;
        }

        public async Task RegistrationCommandHandler()
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                Message = "Лоігн та пароль не можуть бути пустими";
                return;
            }

            try
            {
                UserDto userDto = new UserDto { Login = Login, Name = Name, Role = UserRole.Employee, Password = Password, Surname = Surname};
                var user = await _userService.RegistrationAsync(userDto);

                var mainUserOrganization = await _organizationService.GetOrganizationByUserAsync(_user!);

                Employee employee = new Employee { OrganizationId = mainUserOrganization.Id, Role = Role, UserId = user.Id };
                await _employeeService.CreateAsync(employee);
            }
            catch (Exception ex)
            {
                Message = $"Не вийшло зареєструватися: {ex}";
                return;
            }
            Message = "Успішна реєстрація";

            var task = UserCreated?.Invoke();
            if (task is not null) await task;
        }

        public override Task SetAdditionalParameter(User parameter)
        {
            _user = parameter;
            return Task.CompletedTask;
        }
    }
}
