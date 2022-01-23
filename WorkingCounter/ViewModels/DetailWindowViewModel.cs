namespace WorkingCounter.ViewModels
{
    using System;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;

    public class DetailWindowViewModel : IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public string Title => "Detail window";

        public Work Work { get; set; }

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
            Work = parameters.GetValue<Work>(nameof(Work));
            System.Diagnostics.Debug.WriteLine(Work);
        }
    }
}
