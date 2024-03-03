using ChromaResolver.Models.ECM;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Wpf.Ui;

namespace ChromaResolver.ViewModels.ECMViewModels
{
    public partial class ECMSamplesViewModel : ObservableObject
    {
        private readonly ECMSampleViewModel _sampleViewModel;

        private readonly INavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<Sample> samples;

        [ObservableProperty]
        private Sample selectedSample;

        public ECMSamplesViewModel(INavigationService navigationService, ECMSampleViewModel sampleViewModel)
        {
            Samples = [];
            _navigationService = navigationService;
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
    }
}
