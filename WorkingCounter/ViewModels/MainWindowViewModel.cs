namespace WorkingCounter.ViewModels
{
    using System.Collections.Generic;
    using Prism.Commands;
    using Prism.Mvvm;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private List<Work> works;
        private WorkingDbContext workingDbContext;
        private string inputText;

        public MainWindowViewModel()
        {
            Works = new List<Work>();
            workingDbContext = new WorkingDbContext();
            workingDbContext.CreateDatabase();
        }

        public DelegateCommand AddWorkCommand => new DelegateCommand(() =>
        {
            var work = new Work();
            work.Name = InputText;
            work.AdditionDate = System.DateTime.Now;
            work.Unit = "1p";
            workingDbContext.Insert(work);
        });

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public List<Work> Works { get => works; set => SetProperty(ref works, value); }

        public string InputText { get => inputText; set => SetProperty(ref inputText, value); }
    }
}
