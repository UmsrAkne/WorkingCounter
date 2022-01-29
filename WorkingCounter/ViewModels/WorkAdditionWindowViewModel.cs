namespace WorkingCounter.ViewModels
{
    using System;
    using Prism.Services.Dialogs;

    public class WorkAdditionWindowViewModel : IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public string Title => "New work window";

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
