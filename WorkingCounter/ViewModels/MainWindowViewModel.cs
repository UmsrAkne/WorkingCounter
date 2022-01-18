namespace WorkingCounter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
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
            ReloadWorks();
        }

        public DelegateCommand AddWorkCommand => new DelegateCommand(() =>
        {
            var work = new Work();
            work.Name = InputText;
            work.AdditionDate = System.DateTime.Now;
            work.StartDate = System.DateTime.Today;
            work.LimitDate = System.DateTime.Today.AddDays(1);
            work.Unit = "1p";
            workingDbContext.Insert(work);

            InputText = string.Empty;
            ReloadWorks();
        });

        public DelegateCommand<ListViewItem> AddWorkingUnitCommand => new DelegateCommand<ListViewItem>((ListViewItem l) =>
        {
            Work currentWork = l.Content as Work;

            if (currentWork != null)
            {
                var unit = new WorkingUnit();
                unit.ParentWorkId = currentWork.Id;
                workingDbContext.Insert(unit);
                ReloadWorks();
            }
        });

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ObservableCollection<Work> Works { get => works; set => SetProperty(ref works, value); }

        public string InputText { get => inputText; set => SetProperty(ref inputText, value); }

        private void ReloadWorks()
        {
            Works = new ObservableCollection<Work>(workingDbContext.GetWorks());
        }
    }
}
