namespace WorkingCounter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;
    using WorkingCounter.Views;

    public class MainWindowViewModel : BindableBase
    {
        private readonly WorkingDbContext workingDbContext;
        private readonly IDialogService dialogService;

        private string title = "Prism Application";
        private ObservableCollection<Work> works;
        private string inputText;

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
            var work = new Work
            {
                Name = InputText,
                AdditionDate = System.DateTime.Now,
                StartDate = System.DateTime.Today,
                LimitDate = System.DateTime.Today.AddDays(1),
                Unit = "1p"
            };

            workingDbContext.Insert(work);
            InputText = string.Empty;
            ReloadWorks();
        });

        public DelegateCommand<ListViewItem> AddWorkingUnitCommand => new DelegateCommand<ListViewItem>((ListViewItem l) =>
        {
            if (l.Content is Work currentWork)
            {
                var unit = new WorkingUnit
                {
                    ParentWorkId = currentWork.Id,
                    AdditionDate = System.DateTime.Now
                };

                workingDbContext.Insert(unit);
                ReloadWorks();
            }
        });

        public DelegateCommand<Work> ShowDetailWindowCommand => new DelegateCommand<Work>((Work w) =>
        {
            if (w != null)
            {
                var param = new DialogParameters
                {
                    { nameof(Work), w },
                    { nameof(WorkingDbContext), workingDbContext }
                };

                dialogService.ShowDialog(nameof(DetailWindow), param, (IDialogResult result) => { });
                ReloadWorks();
            }
        });

        public DelegateCommand ShowAdditionWindowCommand => new DelegateCommand(() =>
        {
            var param = new DialogParameters
            {
                { nameof(WorkingDbContext), workingDbContext }
            };

            dialogService.ShowDialog(nameof(WorkAdditionWindow), param, (IDialogResult result) => { });
            ReloadWorks();
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
