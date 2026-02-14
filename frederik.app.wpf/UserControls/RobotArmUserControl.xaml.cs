using frederik.app.wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace frederik.app.wpf.UserControls
{
    /// <summary>
    /// Interaktionslogik für RobotArmUserControl.xaml
    /// </summary>
    public partial class RobotArmUserControl : UserControl
    {
        public RobotArmUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        public static readonly DependencyProperty RobotArmViewModelProperty = DependencyProperty.Register(nameof(RobotArmViewModel), typeof(RobotArmViewModel), typeof(RobotArmUserControl));

        public RobotArmViewModel RobotArmViewModel
        {
            get { return (RobotArmViewModel)GetValue(RobotArmViewModelProperty); }
            set { SetValue(RobotArmViewModelProperty, value); }
        }
    }
}
