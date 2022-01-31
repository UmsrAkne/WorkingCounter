namespace WorkingCounter.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;

    public class WorkAdditionWindowViewModel : BindableBase, IDialogAware
    {
        private Work work = new Work();
        private ObservableCollection<Work> works = new ObservableCollection<Work>();
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
            var result = new DialogResult();
            result.Parameters.Add(nameof(Work), work);
            RequestClose.Invoke(result);
        });

        public DelegateCommand CloseAndDisposeCommand => new DelegateCommand(() =>
        {
            RequestClose.Invoke(new DialogResult());
        });

        public DelegateCommand AddWorkCommand => new DelegateCommand(() =>
        {
            Works.Add(new Work());
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
            Works.Add(new Work());
        }
    }
}
