using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

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
        public string additionalInfo;

        [ObservableProperty]
        private BaseElement fe;

        [ObservableProperty]
        private BaseElement cr;

        [ObservableProperty]
        private BaseElement ni;

        [ObservableProperty]
        private BaseElement cu;

        [ObservableProperty]
        ObservableCollection<EStemType> stemsItemSource;

        [ObservableProperty]
        private StemResolver stemResolver;

        public Sample(string name, int id, DateOnly date, string creator, int ah, double aboveHeight)
        {
            Name = name;
            Id = id;
            Date = date;
            Creator = creator;
            Ah = ah;
            AboveHeight = aboveHeight;
            StemsItemSource =
            [
                EStemType.Stem1,
                EStemType.Stem2,
                EStemType.Stem3
            ];
            StemResolver = new StemResolver();
            Fe = new(StemResolver, EBaseElement.Fe);
            Cr = new(StemResolver, EBaseElement.Cr);
            Ni = new(StemResolver, EBaseElement.Ni);
            Cu = new(StemResolver, EBaseElement.Cu);
            var dateTime = date.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc);
            DaysAgo = (int)DateTime.UtcNow.Date.Subtract(dateTime).TotalDays;
            if (DaysAgo < 0)
            {
                DaysAgo = 0;
            }
        }

        public Sample()
        {

        }
    }

    public partial class BaseElement : ObservableObject
    {
        private readonly StemResolver _stemResolver;

        [ObservableProperty]
        private EStemType type;

        [ObservableProperty]
        private EBaseElement element;

        [ObservableProperty]
        private double value;

        [ObservableProperty]
        private double percentageValue;

        [ObservableProperty]
        private double icpValue;

        public BaseElement(StemResolver stemResolver, EBaseElement element)
        {
            _stemResolver = stemResolver;
            Element = element;
            switch (Element)
            {
                case EBaseElement.Fe:
                    Type = EStemType.Stem2;
                    break;
                case EBaseElement.Cr:
                    Type = EStemType.Stem2;
                    break;
                case EBaseElement.Ni:
                    Type = EStemType.Stem3;
                    break;
                case EBaseElement.Cu:
                    Type = EStemType.Stem3;
                    break;
            }
        }

        partial void OnTypeChanged(EStemType value)
        {
            OnIcpValueChanged(IcpValue);
        }

        partial void OnIcpValueChanged(double value)
        {
            Value = value * _stemResolver.GetValue(Type);
        }

        partial void OnValueChanged(double value)
        {
            PercentageValue = value / 100;
        }
    }

    public partial class Stem : ObservableObject
    {
        [ObservableProperty]
        private EStemType type;

        [ObservableProperty]
        private double bathMass;

        [ObservableProperty]
        private double sampleMass;

        [ObservableProperty]
        private double value;

        public Stem(EStemType type)
        {
            Type = type;
        }

        partial void OnBathMassChanged(double value)
        {
            CalculateValue();
        }

        partial void OnSampleMassChanged(double value)
        {
            CalculateValue();
        }

        private void CalculateValue()
        {
            Value = SampleMass / BathMass;
        }
    }


    public partial class StemResolver : ObservableObject
    {
        [ObservableProperty]
        private Stem stem1;

        [ObservableProperty]
        private Stem stem2;

        [ObservableProperty]
        private Stem stem3;

        public StemResolver()
        {
            Stem1 = new Stem(EStemType.Stem1);
            Stem2 = new Stem(EStemType.Stem2);
            Stem3 = new Stem(EStemType.Stem3);
        }

        public double GetValue(EStemType type)
        {
            switch (type)
            {
                case EStemType.Stem1:
                    return Stem1.Value;
                case EStemType.Stem2:
                    return Stem2.Value;
                case EStemType.Stem3:
                    return Stem3.Value;
                default:
                    return 1;
            }
        }
    }

    public enum EBaseElement
    {
        Fe,
        Cr,
        Ni,
        Cu
    }

    public enum EStemType
    {
        Stem1,
        Stem2,
        Stem3
    }
}
