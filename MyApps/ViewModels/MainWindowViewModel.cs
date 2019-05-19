using System;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.System;

using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

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

        public ICommand GetGeolocation { get; }

        public MainWindowViewModel()
        {
            this.Message = "Hello .NET Core 3.0 WPF World!";
            this.GetGeolocation = new DelegateCommand(this.GetGeolocationAsync);
        }

        private async void GetGeolocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                // Put all your Code here to access location services
                var locator = new Geolocator();
                var location = await locator.GetGeopositionAsync();
                var position = location.Coordinate.Point.Position;
                var latlong = string.Format("lat:{0}, long:{1}", position.Latitude, position.Longitude);
                this.Message = $"Get Geoposition\n{latlong}";
                return;
            }
            else if (accessStatus == GeolocationAccessStatus.Denied)
            {
                // No Accesss
                this.Message = "Geolocation permission denied\nSet to allow apps to access your location.";

            }
            else
            {
                this.Message = "Undefined geolocation settings\nSet to allow apps to access your location.";
            }
            await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
        }
    }
}
