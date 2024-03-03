using ChromaResolver.Views;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;

namespace ChromaResolver.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void Navigate(string navigateTag)
        {
            if (navigateTag.Equals("ECM"))
            {
                _navigationService.Navigate(typeof(ECMSamplesView));
            }
            if (navigateTag.Equals("LAB"))
            {
                _navigationService.Navigate(typeof(LABView));
            }
        }
    }
}