namespace WorkingCounter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        private ObservableCollection<Work> works;
        private WorkingDbContext workingDbContext;
        private string inputText;
        private IDialogService dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            Works = new ObservableCollection<Work>();
            workingDbContext = new WorkingDbContext();
            workingDbContext.CreateDatabase();
            ReloadWorks();

            this.dialogService = dialogService;
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
                unit.AdditionDate = System.DateTime.Now;
                workingDbContext.Insert(unit);
                ReloadWorks();
            }
        });

        public DelegateCommand<Work> ShowDetailWindowCommand => new DelegateCommand<Work>((Work w) =>
        {
            if (w != null)
            {
                var param = new DialogParameters();
                param.Add(nameof(Work), w);
                dialogService.ShowDialog(nameof(DetailWindow), param, (IDialogResult result) => { });
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
            var workList = workingDbContext.GetWorks();
            workList.ForEach(w =>
            {
                w.Units = workingDbContext.GetWorkingUnits(w.Id);
            });

            Works = new ObservableCollection<Work>(workList);
        }
    }
}
