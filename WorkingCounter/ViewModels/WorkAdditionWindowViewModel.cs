namespace WorkingCounter.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class WorkAdditionWindowViewModel : BindableBase, IDialogAware
    {
        private readonly Work work = new Work();
        private ObservableCollection<Work> works = new ObservableCollection<Work>();
        private WorkingDbContext workingDbContext;
        private string name;
        private int dateCountToLimit;
        private int dateCountToStart;
        private int quota;

        public event Action<IDialogResult> RequestClose;

        public ObservableCollection<Work> Works { get => works; set => SetProperty(ref works, value); }

        public string Title => "New work window";

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                work.Name = value;
            }
        }

        public int DateCountToLimit
        {
            get => dateCountToLimit;
            set
            {
                SetProperty(ref dateCountToLimit, value);
            }
        }

        public int DateCountToStart
        {
            get => dateCountToStart;
            set
            {
                SetProperty(ref dateCountToStart, value);
            }
        }

        public int Quota
        {
            get => quota;
            set
            {
                SetProperty(ref quota, value);
                work.Quota = value;
            }
        }

        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            Works.ToList().ForEach((w) =>
            {
                workingDbContext.Works.Add(w);
            });

            workingDbContext.SaveChanges();
            RequestClose.Invoke(new DialogResult());
        });

        public DelegateCommand CloseAndDisposeCommand => new DelegateCommand(() =>
        {
            RequestClose.Invoke(new DialogResult());
        });

        public DelegateCommand AddWorkCommand => new DelegateCommand(() =>
        {
            Works.Add(new Work());
        });

        public DelegateCommand SaveTemplateCommand => new DelegateCommand(() =>
        {
            var dtiConv = new DateToIntConverter();
            Works.ToList().ForEach(w =>
            {
                var t = new WorkTemplate
                {
                    DayCountToLimit = Convert.ToInt32(dtiConv.Convert(w.LimitDate, typeof(double), null, null)),
                    DayCountToStart = Convert.ToInt32(dtiConv.Convert(w.StartDate, typeof(double), null, null)),
                    Name = w.Name,
                    Quota = w.Quota
                };
            });
        });

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            workingDbContext = parameters.GetValue<WorkingDbContext>(nameof(WorkingDbContext));
            Works.Add(new Work());
        }
    }
}
