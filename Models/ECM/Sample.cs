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
        public DateTime date;

        [ObservableProperty]
        public string creator;

        [ObservableProperty]
        public int ah;

        [ObservableProperty]
        public double aboveHeight;

        public Sample(string name, int id, DateTime date, string creator, int ah, double aboveHeight)
        {
            Name = name;
            Id = id;
            Date = date;
            Creator = creator;
            Ah = ah;
            AboveHeight = aboveHeight;
        }
    }
}
