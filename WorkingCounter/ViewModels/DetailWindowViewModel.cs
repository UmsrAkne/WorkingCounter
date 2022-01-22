namespace WorkingCounter.ViewModels
{
    using System;
    using Prism.Services.Dialogs;

    public class DetailWindowViewModel : IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public string Title => "Detail window";

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
