using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromaResolver.Models.ECM
{
    [Table("ECM")]
    public partial class Sample : ObservableObject
    {
        [Column("Guid")]
        public Guid Guid { get; init; }

        [Column("Name")]
        [ObservableProperty]
        public string name;

        [Column("Id")]
        [ObservableProperty]
        public int id;

        [NotMapped]
        [ObservableProperty]
        public int daysAgo;

        [Column("Date")]
        [ObservableProperty]
        public DateOnly date;

        [Column("Creator")]
        [ObservableProperty]
        public string creator;

        [Column("Ah")]
        [ObservableProperty]
        public int ah;

        [Column("AboveHeight")]
        [ObservableProperty]
        public double aboveHeight;

        [Column("AdditionalInfo")]
        [ObservableProperty]
        public string? additionalInfo;

        [ForeignKey("Fe")]
        [Column("Fe")]
        public Guid FeElementId { get; set; }

        [ForeignKey("Cr")]
        [Column("Cr")]
        public Guid CrElementId { get; set; }

        [ForeignKey("Ni")]
        [Column("Ni")]
        public Guid NiElementId { get; set; }

        [ForeignKey("Cu")]
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

        [NotMapped]
        [ObservableProperty]
        ObservableCollection<EStemType> stemsItemSource;

        [NotMapped]
        [ObservableProperty]
        private StemResolver stemResolver;

        public Sample(string name, int id, DateOnly date, string creator, int ah, double aboveHeight)
        {
            Guid = Guid.NewGuid();
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

    [Table("ECMBase")]
    public partial class BaseElement : ObservableObject
    {
        [NotMapped]
        private readonly StemResolver _stemResolver;

        [Column("Guid")]
        public Guid Guid { get; init; }

        [Column("Type")]
        [ObservableProperty]
        private EStemType type;

        [Column("Element")]
        [ObservableProperty]
        private EBaseElement element;

        [Column("Value")]
        [ObservableProperty]
        private double value;

        [NotMapped]
        [ObservableProperty]
        private double percentageValue;

        [Column("IcpValue")]
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
