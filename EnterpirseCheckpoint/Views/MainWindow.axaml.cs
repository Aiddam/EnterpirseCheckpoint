using Avalonia.Controls;
using EnterpirseCheckpoint.ViewModels;

namespace EnterpirseCheckpoint.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
