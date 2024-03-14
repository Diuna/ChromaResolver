using ChromaResolver.Models.ECM;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;

namespace ChromaResolver.ViewModels.ECMViewModels
{
    public partial class ECMSampleViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        public Sample sample;

        public ECMSampleViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void ReceiveSample(Sample? sample = null)
        {
            if (sample == null)
            {
                Sample = new();

            }
            else
            {
                Sample = sample;
            }
        }

        [RelayCommand]
        private void Save()
        {
            _navigationService.Navigate(typeof(ECMSamplesView));
        }
    }
}
