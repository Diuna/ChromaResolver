using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChromaResolver.Models.ECM
{
    [Table("ECM")]
    public partial class Sample : ObservableObject
    {
        [Column("guid")]
        public Guid Guid { get; init; }

        [Column("name")]
        [ObservableProperty]
        public string name;

        [Column("id")]
        [ObservableProperty]
        public int id;

        [NotMapped]
        private int _daysAgo;

        [NotMapped]
        public int DaysAgo
        {
            get => _daysAgo;
            set
            {
                _daysAgo = value;
                OnPropertyChanged();
            }
        }

        [Column("date")]
        [ObservableProperty]
        public DateOnly date;

        [Column("creator")]
        [ObservableProperty]
        public string creator;

        [Column("ah")]
        [ObservableProperty]
        public int ah;

        [Column("above_height")]
        [ObservableProperty]
        public double aboveHeight;

        [Column("additional_info")]
        [ObservableProperty]
        public string? additionalInfo;

        [Column("stem1")]
        [ObservableProperty]
        public double stem1;

        [Column("stem2")]
        [ObservableProperty]
        public double stem2;

        [Column("stem3")]
        [ObservableProperty]
        public double stem3;

        [ForeignKey("fe")]
        [Column("Fe")]
        public Guid FeElementId { get; set; }

        [ForeignKey("cr")]
        [Column("Cr")]
        public Guid CrElementId { get; set; }

        [ForeignKey("ni")]
        [Column("Ni")]
        public Guid NiElementId { get; set; }

        [ForeignKey("cu")]
        [Column("Cu")]
        public Guid CuElementId { get; set; }

        [NotMapped]
        [ObservableProperty]
        private BaseElement fe;

        [NotMapped]
        [ObservableProperty]
        private BaseElement cr;

        [NotMapped]
        [ObservableProperty]
        private BaseElement ni;

        [NotMapped]
        [ObservableProperty]
        private BaseElement cu;

        private ObservableCollection<EStemType> _stemsItemSource;

        [NotMapped]
        public ObservableCollection<EStemType> StemsItemSource
        {
            get => _stemsItemSource;
            set
            {
                _stemsItemSource = value;
                OnPropertyChanged();
            }
        }

        private StemResolver _stemResolver;

        [NotMapped]
        public StemResolver StemResolver
        {
            get => _stemResolver;
            set
            {
                _stemResolver = value;
                OnPropertyChanged();
            }
        }

        public Sample(Sample sample, BaseElement[] elements)
        {
            _stemResolver = new StemResolver();
            Guid = sample.Guid;
            Name = sample.Name;
            Id = sample.Id;
            Date = sample.Date;
            Creator = sample.Creator;
            Ah = sample.Ah;
            AboveHeight = sample.AboveHeight;
            Fe = elements.First(x => x.Element == EBaseElement.Fe);
            Cr = elements.First(x => x.Element == EBaseElement.Cr);
            Ni = elements.First(x => x.Element == EBaseElement.Ni);
            Cu = elements.First(x => x.Element == EBaseElement.Cu);
            Fe.SetStemResolver(_stemResolver);
            Cr.SetStemResolver(_stemResolver);
            Ni.SetStemResolver(_stemResolver);
            Cu.SetStemResolver(_stemResolver);
            FeElementId = Fe.Guid;
            CrElementId = Cr.Guid;
            CuElementId = Cu.Guid;
            NiElementId = Ni.Guid;
            _stemResolver.Stem1.Value = sample.Stem1;
            _stemResolver.Stem2.Value = sample.Stem2;
            _stemResolver.Stem3.Value = sample.Stem3;
            Fe.NotifyChanged();
            Cr.NotifyChanged();
            Cu.NotifyChanged();
            Ni.NotifyChanged();
            UpdateDateTime();
            InitializeItemSource();
        }

        public Sample(string name, int id, DateOnly date, string creator, int ah, double aboveHeight)
        {
            _stemResolver = new StemResolver();
            Guid = Guid.NewGuid();
            Name = name;
            Id = id;
            Date = date;
            Creator = creator;
            Ah = ah;
            AboveHeight = aboveHeight;
            Fe = new(_stemResolver, EBaseElement.Fe);
            Cr = new(_stemResolver, EBaseElement.Cr);
            Ni = new(_stemResolver, EBaseElement.Ni);
            Cu = new(_stemResolver, EBaseElement.Cu);

            FeElementId = Fe.Guid;
            CrElementId = Cr.Guid;
            CuElementId = Cu.Guid;
            NiElementId = Ni.Guid;
            UpdateDateTime();
            InitializeItemSource();
        }

        public Sample()
        {
            InitializeItemSource();
        }

        public void UpdateStems()
        {
            Stem1 = _stemResolver.Stem1.Value;
            Stem2 = _stemResolver.Stem2.Value;
            Stem3 = _stemResolver.Stem3.Value;
        }

        private void UpdateDateTime()
        {
            var dateTime = Date.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc);
            DaysAgo = (int)DateTime.UtcNow.Date.Subtract(dateTime).TotalDays;
            if (DaysAgo < 0)
            {
                DaysAgo = 0;
            }
        }

        private void InitializeItemSource()
        {
            StemsItemSource =
            [
                EStemType.Stem1,
                EStemType.Stem2,
                EStemType.Stem3
            ];
        }
    }

    [Table("ECMBase")]
    public partial class BaseElement : ObservableObject, INotifyPropertyChanged
    {
        [NotMapped]
        private StemResolver _stemResolver;

        [Column("guid")]
        public Guid Guid { get; init; }

        [Column("type")]
        [ObservableProperty]
        private EStemType type;

        [Column("element")]
        [ObservableProperty]
        private EBaseElement element;

        [Column("value")]
        [ObservableProperty]
        private double value;

        [NotMapped]
        private double _percentageValue;

        [NotMapped]
        public double PercentageValue
        {
            get => _percentageValue;
            set
            {
                _percentageValue = value;
                OnPropertyChanged();
            }
        }

        [Column("icp_value")]
        [ObservableProperty]
        private double icpValue;

        public BaseElement(StemResolver stemResolver, EBaseElement element)
        {
            Guid = Guid.NewGuid();
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

        public BaseElement() { }

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

        public void NotifyChanged()
        {
            OnValueChanged(Value);
        }

        public void SetStemResolver(StemResolver stemResolver)
        {
            _stemResolver = stemResolver;
        }
    }

    [NotMapped]
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

    [NotMapped]
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
            return type switch
            {
                EStemType.Stem1 => Stem1.Value,
                EStemType.Stem2 => Stem2.Value,
                EStemType.Stem3 => Stem3.Value,
                _ => 1,
            };
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
