using System;
using System.ComponentModel;
using System.Globalization;
using Windows.Storage;
using Windows.UI.Xaml;
using Common.ForViews;

namespace Common.ViewModels
{
    public class ConfigurationsViewModel : ObservableObject
    {
        private bool _notificationsEnabled = false;

        public bool NotificationsEnabled
        {
            get { return _notificationsEnabled; }
            set
            {
                if (Set(ref _notificationsEnabled, value))
                    _localSettings.Values[nameof(NotificationsEnabled)] = value;
            }
        }

        private bool _liveTileEnabled = false;

        public bool LiveTileEnabled
        {
            get { return _liveTileEnabled; }
            set
            {
                if (Set(ref _liveTileEnabled, value))
                    _localSettings.Values[nameof(LiveTileEnabled)] = value;
            }
        }
        private bool _darkTheme = false;
        public bool DarkTheme
        {
            get { return _darkTheme; }
            set
            {
                if (Set(ref _darkTheme, value))
                    _localSettings.Values[nameof(DarkTheme)] = value;
                ChangeTheme();

            }
        }

        private ElementTheme _theme = ElementTheme.Default;

        public ElementTheme Theme
        {
            get { return _theme; }
            set { Set(ref _theme, value); }
        }

        private DateTime _lastSuccess = DateTime.MinValue;

        public DateTime LastSuccess
        {
            get { return _lastSuccess; }
            set
            {
                if (Set(ref _lastSuccess, value))
                    _localSettings.Values[nameof(LastSuccess)] =
                        value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public ConfigurationsViewModel()
        {
            LoadSettings();
            ChangeTheme();
        }

        private readonly ApplicationDataContainer _localSettings =
            ApplicationData.Current.LocalSettings;

        public event PropertyChangedEventHandler PropertyChanged;
        private void ChangeTheme()
        {
            if (this.DarkTheme)
                this.Theme = ElementTheme.Dark;
            else
                this.Theme = ElementTheme.Light;
        }
        private void LoadSettings()
        {
            var notificationsEnabled = _localSettings.Values[nameof(NotificationsEnabled)];
            if (notificationsEnabled != null)
                NotificationsEnabled = (bool)notificationsEnabled;

            var updatingLiveTileEnabled = _localSettings.Values[nameof(LiveTileEnabled)];
            if (updatingLiveTileEnabled != null)
                LiveTileEnabled = (bool)updatingLiveTileEnabled;

            var darkTheme = _localSettings.Values[nameof(DarkTheme)];
            if (darkTheme != null)
                DarkTheme = (bool)darkTheme;

            var lastSuccessfulRun = _localSettings.Values[nameof(LastSuccess)];
            if (lastSuccessfulRun != null)
                LastSuccess = DateTime.Parse(lastSuccessfulRun.ToString(),
                    CultureInfo.InvariantCulture);
        }
    }
}