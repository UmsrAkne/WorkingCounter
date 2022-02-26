namespace WorkingCounter.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NameScope.SetNameScope(menu, NameScope.GetNameScope(this));
        }
    }
}
