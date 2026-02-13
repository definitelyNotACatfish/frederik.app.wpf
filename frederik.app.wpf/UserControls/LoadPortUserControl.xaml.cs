using frederik.app.wpf.Models;
using frederik.app.wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace frederik.app.wpf.UserControls
{
    /// <summary>
    /// Interaktionslogik für LoadPortUserControl.xaml
    /// </summary>
    public partial class LoadPortUserControl : UserControl
    {
        public LoadPortUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string Title { get; set; } = "LoadPort";

        public static readonly DependencyProperty LoadPortViewModelProperty = DependencyProperty.Register(nameof(LoadPortViewModel), typeof(LoadPortViewModel), typeof(LoadPortUserControl));

        public LoadPortViewModel LoadPortViewModel
        {
            get { return (LoadPortViewModel)GetValue(LoadPortViewModelProperty); }
            set { SetValue(LoadPortViewModelProperty, value); }
        }
    }
}
