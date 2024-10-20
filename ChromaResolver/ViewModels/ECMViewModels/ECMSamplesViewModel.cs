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
using Wpf.Ui.Extensions;

namespace ChromaResolver.ViewModels.ECMViewModels
{
    public partial class ECMSamplesViewModel : ObservableObject
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly ECMSampleViewModel _sampleViewModel;

        private readonly INavigationService _navigationService;

        private readonly ISnackbarService _snackbarService;

        private readonly NewSampleContentDialog _newSampleContent;

        [ObservableProperty]
        private ObservableCollection<Sample> _samples;

        [ObservableProperty]
        private Sample _selectedSample;

        public ECMSamplesViewModel(INavigationService navigationService,
            ISnackbarService snackbarService,
            ECMSampleViewModel sampleViewModel,
            NewSampleContentDialog newSampleContentDialog)
        {
            Samples = [];
            _snackbarService = snackbarService;
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
                var sample = new Sample(
                    _newSampleContent.ViewModel.Name,
                    GetNextId(Samples),
                    _newSampleContent.ViewModel.DateStruct,
                    _newSampleContent.ViewModel.Creator,
                    (int)_newSampleContent.ViewModel.Ah,
                    _newSampleContent.ViewModel.Height);
                Samples.Add(sample);

                var success = true;
                try
                {
                    using var context = new SampleContext();
                    context.Samples.Add(sample);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    success = false;
                }
                finally
                {
                    if (success)
                    {
                        _snackbarService.Show("Success", $"New sample ({sample.Name}) has been successfully added", Wpf.Ui.Controls.ControlAppearance.Success);
                    }
                    else
                    {
                        _snackbarService.Show("Fail", $"Failed to add new sample ({sample.Name}).", Wpf.Ui.Controls.ControlAppearance.Danger);
                    }
                }
            }

        }

        private int GetNextId(ObservableCollection<Sample> samples)
        {
            return samples.Max(x => x.Id) + 1;
        }

        private void LoadSamples()
        {
            using var client = new SampleContext();
            //Mock(client);
            CollectData(client);
        }

        private void CollectData(SampleContext client)
        {
            foreach (var sample in client.Samples)
            {
                var elements = new List<BaseElement>();
                var feResult = GetBaseElementByGuid(client.BaseElements, sample.FeElementId);
                if (feResult.success)
                {
                    elements.Add(feResult.element);
                }
                var crResult = GetBaseElementByGuid(client.BaseElements, sample.CrElementId);
                if (crResult.success)
                {
                    elements.Add(crResult.element);
                }
                var cuResult = GetBaseElementByGuid(client.BaseElements, sample.CuElementId);
                if (cuResult.success)
                {
                    elements.Add(cuResult.element);
                }
                var niResult = GetBaseElementByGuid(client.BaseElements, sample.NiElementId);
                if (niResult.success)
                {
                    elements.Add(niResult.element);
                }
                Samples.Add(new Sample(sample, [.. elements]));
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
                        baseSample.IcpValue = 1;
                        baseSample.Value = 11;
                        sample.FeElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Cr:
                        sample.Cr = baseSample;
                        baseSample.IcpValue = 2;
                        baseSample.Value = 22;
                        sample.CrElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Ni:
                        sample.Ni = baseSample;
                        baseSample.IcpValue = 3;
                        baseSample.Value = 33;
                        sample.NiElementId = baseSample.Guid;
                        break;
                    case EBaseElement.Cu:
                        sample.Cu = baseSample;
                        baseSample.IcpValue = 4;
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
