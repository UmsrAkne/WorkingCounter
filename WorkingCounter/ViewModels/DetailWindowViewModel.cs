namespace WorkingCounter.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;

    public class DetailWindowViewModel : BindableBase, IDialogAware
    {
        private Work work;
        private ObservableCollection<WorkingUnit> workingUnits;

        public event Action<IDialogResult> RequestClose;

        public string Title => "Detail window";

        public Work Work { get => work; set => SetProperty(ref work, value); }

        public ObservableCollection<WorkingUnit> WorkingUnits { get => workingUnits; set => SetProperty(ref workingUnits, value); }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Work = parameters.GetValue<Work>(nameof(Work));
            WorkingUnits = new ObservableCollection<WorkingUnit>(Work.Units.ToList());
        }
    }
}
