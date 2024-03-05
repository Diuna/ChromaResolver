using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ChromaResolver.Models.ECM
{
    public partial class Sample : ObservableObject
    {

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public int daysAgo;

        [ObservableProperty]
        public DateOnly date;

        [ObservableProperty]
        public string creator;

        [ObservableProperty]
        public int ah;

        [ObservableProperty]
        public double aboveHeight;

        [ObservableProperty]
        private double fePercent;

        [ObservableProperty]
        private double crPercent;

        [ObservableProperty]
        private double niPercent;

        [ObservableProperty]
        private double cuPercent;

        public Sample(string name, int id, DateOnly date, string creator, int ah, double aboveHeight)
        {
            Name = name;
            Id = id;
            Date = date;
            Creator = creator;
            Ah = ah;
            AboveHeight = aboveHeight;
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        }

        public Sample()
        {

        }
    }
}
