using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common.Models;
using Common.ViewModels;

namespace ReadApp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            
            this.InitializeComponent();
            //Logic.PropertyChanged += Logic_PropertyChanged;
            //VisualStateManager.GoToState(this, Logic.LoadingState.ToString(), true);
        }


        //private void appBarButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(About));
            
        //}
        //private MainPageDataViewModel Logic => DataContext as MainPageDataViewModel;


       
        //private void Logic_PropertyChanged(object sender,
        //    System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(MainPageDataViewModel.LoadingState))
        //        VisualStateManager.GoToState(this, Logic.LoadingState.ToString(), true);

        //}


    }
}