using Avalonia.Controls;
using EnterpirseCheckpoint.ViewModels;

namespace EnterpirseCheckpoint.Views
{
    public partial class ChangeScheduleView : UserControl
    {
        public ChangeScheduleView()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var viewModel = DataContext as ChangeScheduleViewModel;
            if (viewModel is null) return;
            await viewModel.LoadEmployeeAsync();
        }
    }
}
