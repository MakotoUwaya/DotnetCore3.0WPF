using Prism.Mvvm;

namespace MyApps.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return this._message; }
            set { this.SetProperty(ref this._message, value); }
        }

        public MainWindowViewModel()
        {
            this.Message = "Greet.";
        }
    }
}
