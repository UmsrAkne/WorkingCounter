namespace WorkingCounter.ViewModels
{
    using System;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;

    public class WorkAdditionWindowViewModel : BindableBase, IDialogAware
    {
        private Work work = new Work();
        private string name;
        private int dateCountToLimit;
        private int dateCountToStart;
        private int quota;

        public event Action<IDialogResult> RequestClose;

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

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
