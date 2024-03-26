using Autofac;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IComponentContext _componentContext;

        private ViewModelBase _viewModel = null!;
        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _viewModel, value);
                ViewModel.OnChangeViewModel += InternalChangeView;
            }
        }

        public override User? CurrentUser 
        {
            get => ViewModel.CurrentUser; 
            set => ViewModel.CurrentUser = value;
        }

        public ICommand LogoutButton { get; set; }

        public MainViewModel(IComponentContext componentContext)
        {
            _componentContext = componentContext;

            LogoutButton = ReactiveCommand.Create(() => InternalChangeView(_componentContext.Resolve<LoginViewModel>()));
        }

        private void InternalChangeView(ViewModelBase viewModel)
        {
            ViewModel = viewModel;

            this.RaisePropertyChanging(nameof(ViewModel));
            this.RaisePropertyChanged(nameof(CurrentUser));
        }
    }
}
