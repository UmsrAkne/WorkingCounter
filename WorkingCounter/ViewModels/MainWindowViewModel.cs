namespace WorkingCounter.ViewModels
{
    using System.Collections.Generic;
    using Prism.Mvvm;
    using WorkingCounter.Models;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private List<Work> works;

        public MainWindowViewModel()
        {
            Works = new List<Work>();
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<Work> Works { get => works; set => SetProperty(ref works, value); }
    }
}
