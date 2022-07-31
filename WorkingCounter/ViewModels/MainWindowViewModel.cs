namespace WorkingCounter.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
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

        private string title = "Working counter";
        private ObservableCollection<Work> works;
        private DateTime filteringStartDate = DateTime.Now;
        private int filteringDuration = 3;
        private bool filtering;
        private int displayCountLimit = 200;
        private bool isRestrictedDisplay = true;
        private WorkingUnit selectedUnit;

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
                    AdditionDate = DateTime.Now,
                };

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
                param.Add(nameof(WorkingDbContext), workingDbContext);

                dialogService.ShowDialog(nameof(DetailWindow), param, (IDialogResult result) => { });
                ReloadWorks();
            }
        });

        public DelegateCommand ShowAdditionWindowCommand => new DelegateCommand(() =>
        {
            var param = new DialogParameters();
            param.Add(nameof(WorkingDbContext), workingDbContext);

            dialogService.ShowDialog(nameof(WorkAdditionWindow), param, (IDialogResult result) => { });
            ReloadWorks();
        });

        public DelegateCommand ForwardFilteringDateCommand =>
            new DelegateCommand(() => FilteringStartDate = FilteringStartDate.AddDays(1));

        public DelegateCommand BackFilteringDateCommand =>
            new DelegateCommand(() => FilteringStartDate = FilteringStartDate.AddDays(-1));

        public DelegateCommand DurationWidenCommand =>
             new DelegateCommand(() => FilteringDuration++);

        public DelegateCommand DurationNarrowCommand =>
             new DelegateCommand(() =>
             {
                 FilteringDuration--;
                 FilteringDuration = FilteringDuration <= 0 ? 0 : FilteringDuration;
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
            set
            {
                if (value == default(DateTime))
                {
                    value = filteringStartDate;
                }

                SetProperty(ref filteringStartDate, value);
                ReloadWorks();
            }
        }

        public int FilteringDuration
        {
            get => filteringDuration;
            set
            {
                SetProperty(ref filteringDuration, value);
                ReloadWorks();
            }
        }

        public bool Filtering
        {
            get => filtering;
            set
            {
                SetProperty(ref filtering, value);
                ReloadWorks();
            }
        }

        public int DisplayCountLimit
        {
            get => displayCountLimit;
            set
            {
                SetProperty(ref displayCountLimit, value);
                ReloadWorks();
            }
        }

        public bool IsRestrictedDisplay
        {
            get => isRestrictedDisplay;
            set
            {
                SetProperty(ref isRestrictedDisplay, value);
                ReloadWorks();
            }
        }

        public DelegateCommand DeleteWorkUnitCommand
        {
            get => new DelegateCommand(() =>
            {
                if (SelectedUnit != null)
                {
                    workingDbContext.Delete(SelectedUnit);
                    ReloadWorks();
                }
            });
        }

        public WorkingUnit SelectedUnit { get => selectedUnit; set => SetProperty(ref selectedUnit, value); }

        private void ReloadWorks()
        {
            var workList = filtering
                ? workingDbContext.GetWorks(FilteringStartDate.Date, new TimeSpan(FilteringDuration, 0, 0, 0))
                : workingDbContext.GetWorks();

            workList = IsRestrictedDisplay ? workList.Take(DisplayCountLimit).ToList() : workList;

            workList.ForEach(w =>
            {
                w.Units = workingDbContext.GetWorkingUnits(w.Id);
            });

            Works = new ObservableCollection<Work>(workList);
        }
    }
}
