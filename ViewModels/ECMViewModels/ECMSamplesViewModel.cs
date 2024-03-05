using ChromaResolver.Models.ECM;
using ChromaResolver.Views;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wpf.Ui;

namespace ChromaResolver.ViewModels.ECMViewModels
{
    public partial class ECMSamplesViewModel : ObservableObject
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly ECMSampleViewModel _sampleViewModel;

        private readonly INavigationService _navigationService;

        private readonly IContentDialogService _contentDialogService;

        [ObservableProperty]
        public ObservableCollection<Sample> samples;

        [ObservableProperty]
        private Sample selectedSample;

        public ECMSamplesViewModel(INavigationService navigationService, IContentDialogService contentDialogService, ECMSampleViewModel sampleViewModel)
        {
            Samples = [];
            _cancellationTokenSource = new();
            _navigationService = navigationService;
            _contentDialogService = contentDialogService;
            _sampleViewModel = sampleViewModel;
            for (var i = 0; i < 10; i++)
            {
                Samples.Add(new Sample($"AE{i} I 1:500", i, DateTime.Now.Subtract(new TimeSpan(i, 0, 0, 0)), "MP", i * (i + 2) * 3, 12 % (i + 1)));
            }
            SelectedSample = Samples.Last();
        }

        public void Edit(Sample sample)
        {
            _sampleViewModel.ReceiveSample(sample);
            _navigationService.NavigateWithHierarchy(typeof(ECMSampleView));
        }

        [RelayCommand]
        private async Task AddSample()
        {
            await GetContentDialog().ShowAsync();
            //await _contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions { Title = "Gelo", CloseButtonText = "Close", Content = "daw" });
        }

        private NewSampleContentDialog GetContentDialog()
        {
            return new NewSampleContentDialog(_contentDialogService.GetContentPresenter());
        }
    }
}
