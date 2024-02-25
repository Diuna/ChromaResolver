using ChromaResolver.Models.ECM;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChromaResolver.ViewModels.ECMViewModels
{
    public partial class ECMSamplesViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Sample> samples;

        [ObservableProperty]
        private Sample selectedSample;

        public ECMSamplesViewModel()
        {
            Samples = [];
            for (var i = 0; i < 10; i++)
            {
                Samples.Add(new Models.ECM.Sample("AE1 I 1:500", i, DateTime.Now, "MG", i * (i + 2) * 3, 12 % (i + 1)));
            }
            SelectedSample = Samples.Last();
        }
    }
}
