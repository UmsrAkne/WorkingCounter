namespace WorkingCounter.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;
    using WorkingCounter.Models.DBs;

    public class DetailWindowViewModel : BindableBase, IDialogAware
    {
        private Work work;
        private ObservableCollection<WorkingUnit> workingUnits;
        private string name;
        private WorkingDbContext workingDbContext;

        public event Action<IDialogResult> RequestClose;

        public string Title => "Detail window";

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                var targetWork = workingDbContext.Works.Where(w => work.Id == w.Id).First().Name = Name;
                workingDbContext.SaveChanges();
            }
        }

        public Work Work { get => work; set => SetProperty(ref work, value); }

        public ObservableCollection<WorkingUnit> WorkingUnits { get => workingUnits; set => SetProperty(ref workingUnits, value); }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Work = parameters.GetValue<Work>(nameof(Work));
            workingDbContext = parameters.GetValue<WorkingDbContext>(nameof(WorkingDbContext));
            Name = Work.Name;
            WorkingUnits = new ObservableCollection<WorkingUnit>(Work.Units.ToList());
        }
    }
}
