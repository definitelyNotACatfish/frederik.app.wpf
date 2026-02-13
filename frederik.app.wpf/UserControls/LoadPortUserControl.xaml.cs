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

        public static readonly DependencyProperty LoadPortProperty = DependencyProperty.Register(nameof(LoadPort), typeof(LoadPort), typeof(LoadPortUserControl), new PropertyMetadata(default(LoadPort)));

        public LoadPort LoadPort
        {
            get { return (LoadPort)GetValue(LoadPortProperty); }
            set { SetValue(LoadPortProperty, value); }
        }
    }
}
