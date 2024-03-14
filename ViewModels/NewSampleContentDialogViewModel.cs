using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ChromaResolver.ViewModels
{
    public partial class NewSampleContentDialogViewModel : ObservableObject
    {
        private bool _isComplete = false;

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

        public Action<bool> UpdateControl { get; internal set; }

        public void ClearDataOnLoaded()
        {
            Date = null;
            DateStruct = new DateOnly();
            Creator = "";
            Ah = 0;
            Height = 0;
        }

        partial void OnNameChanged(string value)
        {
            ValidateData();
        }

        partial void OnCreatorChanged(string value)
        {
            ValidateData();
        }


        partial void OnDateChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTimeOffset dateTimeOffset;
                var str = value.Remove(value.Length - 3, 3);
                if (DateTimeOffset.TryParseExact(str, "M/d/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out dateTimeOffset))
                {
                    DateStruct = DateOnly.FromDateTime(dateTimeOffset.Date);
                }
            }
            ValidateData();
        }

        private void ValidateData()
        {
            _isComplete = !string.IsNullOrEmpty(Date) && !string.IsNullOrEmpty(Creator) && !string.IsNullOrEmpty(Name);
            UpdateControl.Invoke(_isComplete);
        }
    }
}
