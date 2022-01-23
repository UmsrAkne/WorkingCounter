namespace WorkingCounter.ViewModels
{
    using System;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using WorkingCounter.Models;

    public class DetailWindowViewModel : BindableBase, IDialogAware
    {
        private Work work;

        public event Action<IDialogResult> RequestClose;

        public string Title => "Detail window";

        public Work Work { get => work; set => SetProperty(ref work, value); }

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
