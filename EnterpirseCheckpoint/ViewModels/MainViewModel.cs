using Autofac;
using ReactiveUI;
using System;

namespace EnterpirseCheckpoint.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IComponentContext _componentContext;

        private ViewModelBase _viewModel = new ViewModelBase();
        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _viewModel, value);
                _viewModel.OnChangeViewModel -= InternalChangeView;
                _viewModel.OnChangeViewModel += InternalChangeView;
            }
        }

        public MainViewModel(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        private void InternalChangeView(ViewModelBase viewModel)
        {
            this.RaisePropertyChanging(nameof(ViewModel));

            ViewModel = viewModel;

            ViewModel.OnChangeViewModel += InternalChangeView;

            this.RaisePropertyChanged(nameof(ViewModel));
        }
    }
}
