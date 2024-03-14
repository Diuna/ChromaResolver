using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ChromaResolver.ViewModels
{
    public partial class NewSampleContentDialogViewModel : ObservableObject
    {
        [ObservableProperty]
        private double ah;

        [ObservableProperty]
        private double height;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string creator;

        [ObservableProperty]
        private string date;

        public DateOnly DateStruct { get; private set; }

        partial void OnDateChanged(string value)
        {
            DateTimeOffset dateTimeOffset;
            var str = value.Remove(value.Length - 3, 3);
            if (DateTimeOffset.TryParseExact(str, "M/d/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out dateTimeOffset))
            {
                DateStruct = DateOnly.FromDateTime(dateTimeOffset.Date);
            }
        }
    }
}
