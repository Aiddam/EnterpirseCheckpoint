using Autofac;
using Avalonia.Controls;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Enum;
using EnterpriseCheckpoint.Models.Models;
using EnterpriseCheckpoint.Services.Services;
using ReactiveUI;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmployeeService _employeeService;

        private readonly User _user;

        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _role = string.Empty;
        private string _message = string.Empty;

        public RegistrationViewModel(IComponentContext context, User mainUser)
        {
            _user = mainUser;
            _userService = context.Resolve<UserService>();
            _organizationService = context.Resolve<OrganizationService>();
            _employeeService = context.Resolve<EmployeeService>();
            RegistrationCommand = ReactiveCommand.CreateFromTask(RegistrationCommandHandler);
        }
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
                await _userService.RegistrationAsync(userDto);

                var mainUserOrganization = await _organizationService.GetOrganizationByUserAsync(_user);

                Employee employee = new Employee { OrganizationId = mainUserOrganization.Id, Role = Role};
                await _employeeService.CreateAsync(employee);
            }
            catch (Exception ex)
            {
                Message = $"Не вийшло зареєструватися: {ex}";
                return;
            }
            Message = "Успішна реєстрація";
        }
    }

}
