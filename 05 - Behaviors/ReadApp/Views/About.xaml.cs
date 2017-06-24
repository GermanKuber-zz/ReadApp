using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ReadApp.Views
{
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
            var v = Package.Current.Id.Version;
            //Se carga la version de mi Package
            tbAppName.Text += $" {v.Major}.{v.Minor}.{v.Revision}.{v.Build}";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;
            //Manejo el evento de retroceso
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            base.OnNavigatedTo(e);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();

            backRequestedEventArgs.Handled = true;
        }

    }
}
