namespace WorkingCounter
{
    using System.Windows;
    using Prism.Ioc;
    using Prism.Modularity;
    using WorkingCounter.ViewModels;
    using WorkingCounter.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DetailWindow, DetailWindowViewModel>();
        }
    }
}
