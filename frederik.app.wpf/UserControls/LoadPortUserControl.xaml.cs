using frederik.app.wpf.Models;
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

        public static DependencyProperty LoadPortProperty = DependencyProperty.Register("LoadPort", typeof(LoadPort), typeof(LoadPortUserControl));

        public LoadPort LoadPort
        {
            get { return (LoadPort)GetValue(LoadPortProperty); }
            set { SetValue(LoadPortProperty, value); }
        }
    }
}
