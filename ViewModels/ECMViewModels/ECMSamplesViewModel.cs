using ChromaResolver.Database;
using ChromaResolver.Models.ECM;
using ChromaResolver.Views;
using ChromaResolver.Views.ECMViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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

        private readonly SampleContext _sampleContext;

        [ObservableProperty]
        public ObservableCollection<Sample> samples;

        [ObservableProperty]
        private Sample selectedSample;

        public ECMSamplesViewModel(INavigationService navigationService,
            SampleContext sampleContext,
            ECMSampleViewModel sampleViewModel,
            NewSampleContentDialog newSampleContentDialog)
        {
            Samples = [];
            _sampleContext = sampleContext;
            _newSampleContent = newSampleContentDialog;
            _cancellationTokenSource = new();
            _navigationService = navigationService;
            _sampleViewModel = sampleViewModel;

            LoadSamples();
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
                    Samples.Count,
                    _newSampleContent.ViewModel.DateStruct,
                    _newSampleContent.ViewModel.Creator,
                    (int)_newSampleContent.ViewModel.Ah,
                    _newSampleContent.ViewModel.Height));
            }
        }

        private void LoadSamples()
        {
            using var client = new SampleContext();
            client.Database.EnsureCreated();
            CollectData(client);
            //Mock(client); 
        }

        private void CollectData(SampleContext client)
        {
            foreach (var sample in client.Samples)
            {
                var feResult = GetBaseElementByGuid(client.BaseElements, sample.FeElementId);
                if (feResult.success)
                {
                    sample.Fe = feResult.element;
                }
                var crResult = GetBaseElementByGuid(client.BaseElements, sample.CrElementId);
                if (crResult.success)
                {
                    sample.Cr = crResult.element;
                }
                var cuResult = GetBaseElementByGuid(client.BaseElements, sample.CuElementId);
                if (cuResult.success)
                {
                    sample.Cu = cuResult.element;
                }
                var niResult = GetBaseElementByGuid(client.BaseElements, sample.NiElementId);
                if (niResult.success)
                {
                    sample.Ni = niResult.element;
                }

                Samples.Add(sample);
            }
        }

        private static (bool success, BaseElement element) GetBaseElementByGuid(IEnumerable<BaseElement> elements, Guid guid)
        {
            var result = elements.Where(x => x.Guid == guid).FirstOrDefault();
            if (result != null)
            {
                return (true, result);
            }
            return (false, null);
        }

        private static void Mock(SampleContext client)
        {
            var sample = new Sample("Test", 1, DateOnly.FromDateTime(DateTime.Now), "MG", 12, 3.2)
            {
                AdditionalInfo = "Test sample generated"
            };
            foreach (var item in new EBaseElement[] { EBaseElement.Fe, EBaseElement.Cr, EBaseElement.Ni, EBaseElement.Cu })
            {
                var baseSample = new BaseElement(new StemResolver(), item);
                switch (item)
                {
                    case EBaseElement.Fe:
                        sample.Fe = baseSample;
                        baseSample.Value = 11;
                        sample.FeElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Cr:
                        sample.Cr = baseSample;
                        baseSample.Value = 22;
                        sample.CrElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Ni:
                        sample.Ni = baseSample;
                        baseSample.Value = 33;
                        sample.NiElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Cu:
                        sample.Cu = baseSample;
                        baseSample.Value = 44;
                        sample.CuElementId = baseSample.Guid;
                        break;
                }
            }
            client.Samples.Add(sample);
            client.SaveChanges();
        }
    }
}
