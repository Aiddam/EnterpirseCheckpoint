using Avalonia.Controls;
using Avalonia.Input;
using EnterpirseCheckpoint.ViewModels;
using EnterpriseCheckpoint.Models.DTOs;

namespace EnterpirseCheckpoint.Views
{
    public partial class ScheduleView : UserControl
    {
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
        {
            var viewModel = DataContext as ScheduleViewModel;
            if (viewModel is null) return;

            var dataGrid = sender as DataGrid;
            if(dataGrid is null) return;

            var selectedItem = dataGrid.SelectedItem as EmployeeDto;
            if (selectedItem is null) return;
            viewModel.GoToScheduleSetUp(selectedItem.Id);
        }

        private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var viewModel = DataContext as ScheduleViewModel;
            if (viewModel is null) return;
            await viewModel.LoadEmployeesAsync();
        }
    }
}
