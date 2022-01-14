namespace WorkingCounter.ViewModels
{
    using System.Collections.Generic;
    using Prism.Mvvm;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private List<Work> works;
        private WorkingDbContext workingDbContext;

        public MainWindowViewModel()
        {
            Works = new List<Work>();
            workingDbContext = new WorkingDbContext();
            workingDbContext.CreateDatabase();
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<Work> Works { get => works; set => SetProperty(ref works, value); }
    }
}
