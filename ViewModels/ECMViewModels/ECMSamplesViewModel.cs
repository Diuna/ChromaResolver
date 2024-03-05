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

        private readonly NewSampleContentDialog _newSampleContent;

        [ObservableProperty]
        public ObservableCollection<Sample> samples;

        [ObservableProperty]
        private Sample selectedSample;

        public ECMSamplesViewModel(INavigationService navigationService,
            ECMSampleViewModel sampleViewModel,
            NewSampleContentDialog newSampleContentDialog)
        {
            Samples = [];
            _newSampleContent = newSampleContentDialog;
            _cancellationTokenSource = new();
            _navigationService = navigationService;
            _sampleViewModel = sampleViewModel;
            for (var i = 0; i < 10; i++)
            {
                Samples.Add(new Sample($"AE{i} I 1:500", i, DateOnly.FromDayNumber(i), "MP", i * (i + 2) * 3, 12 % (i + 1)));
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
            if (await _newSampleContent.ShowAsync() == Wpf.Ui.Controls.ContentDialogResult.Primary)
            {
                Samples.Add(new Sample(
                    _newSampleContent.ViewModel.Name,
                    Samples.Count + 1,
                    _newSampleContent.ViewModel.DateStruct,
                    _newSampleContent.ViewModel.Creator,
                    (int)_newSampleContent.ViewModel.Ah,
                    _newSampleContent.ViewModel.Height));
            }
        }
    }
}
