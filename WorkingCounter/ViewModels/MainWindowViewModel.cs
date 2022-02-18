namespace WorkingCounter.ViewModels
{
    using System;
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
        private DateTime filteringStartDate = DateTime.Now;
        private int filteringDuration = 3;

        public MainWindowViewModel(IDialogService dialogService)
        {
            Works = new ObservableCollection<Work>();
            workingDbContext = new WorkingDbContext();
            workingDbContext.CreateDatabase();
            ReloadWorks();

            this.dialogService = dialogService;
        }

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

        public DateTime FilteringStartDate
        {
            get => filteringStartDate;
            set => SetProperty(ref filteringStartDate, value);
        }

        public int FilteringDuration { get => filteringDuration; set => SetProperty(ref filteringDuration, value); }

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
