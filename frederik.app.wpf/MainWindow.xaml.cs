using frederik.app.wpf.ViewModels;
using System.Windows;

namespace frederik.app.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new WorkflowViewModel();
            DataContext = _viewModel;
        }
    }
}