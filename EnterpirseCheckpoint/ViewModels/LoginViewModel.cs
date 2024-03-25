using Autofac;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IComponentContext _componentContext;
        private string _login = string.Empty;
        private string _password = string.Empty;

        public LoginViewModel(IUserService userService, IComponentContext componentContext)
        {
            _userService = userService;
            _componentContext = componentContext;
            LoginCommand = ReactiveCommand.CreateFromTask(LoginCommandHandler);
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

        public ICommand LoginCommand { get; }

        public async Task LoginCommandHandler()
        {
            if (string.IsNullOrEmpty(Login)) return;
            if (string.IsNullOrEmpty(Password)) return;

            try
            {
                var user = await _userService.LoginAsync(Login, Password);
                var homeViewModel = _componentContext.Resolve<HomeViewModel>();
                homeViewModel.SetAdditionalParameter(user);
                ChangeView(homeViewModel);
                homeViewModel.InitializeTabs();
            }
            catch
            {
                return;
            }
        }
    }
}
