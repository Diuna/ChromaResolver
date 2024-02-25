using ChromaResolver.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;

namespace ChromaResolver.ViewModels
{
    public partial class ECMViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public ECMViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void Navigate()
        {
            _navigationService.NavigateWithHierarchy(typeof(ECMSamplesView));
        }
    }
}
