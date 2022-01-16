namespace WorkingCounter.ViewModels
{
    using System.Collections.ObjectModel;
    using Prism.Commands;
    using Prism.Mvvm;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private ObservableCollection<Work> works;
        private WorkingDbContext workingDbContext;
        private string inputText;

        public MainWindowViewModel()
        {
            Works = new ObservableCollection<Work>();
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

        public ObservableCollection<Work> Works { get => works; set => SetProperty(ref works, value); }

        public string InputText { get => inputText; set => SetProperty(ref inputText, value); }
    }
}
