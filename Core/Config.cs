using COSHH_Generator.Core;
using System;
using System.ComponentModel;


namespace COSHH_Generator
{
    public class Config : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _college;
        public string College
        {
            get { return _college; }
            set
            {
                if (_college != value)
                {
                    _college = value;
                    OnPropertyChanged(nameof(College));
                }
            }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _outputName = "COSHH";
        public string OutputName
        {
            get { return _outputName; }
            set
            {
                if (_outputName != value)
                {
                    _outputName = value;
                    OnPropertyChanged(nameof(OutputName));
                }
            }
        }
        private int _connectionTimeout = 5000;
        public int ConnectionTimeout
        {
            get { return _connectionTimeout; }
            set
            {
                if (_connectionTimeout != value)
                {
                    _connectionTimeout = value;
                    foreach (var provider in SubstanceEntry.SDSProviders)
                    {
                        provider.Timeout = value;
                    }
                    OnPropertyChanged(nameof(ConnectionTimeout));

                }
            }
        }

        private bool _fireExplosion;
        public bool FireExplosion
        {
            get { return _fireExplosion; }
            set
            {
                if (_fireExplosion != value)
                {
                    _fireExplosion = value;
                    OnPropertyChanged(nameof(FireExplosion));
                }
            }
        }

        private bool _thermalRunaway;
        public bool ThermalRunaway
        {
            get { return _thermalRunaway; }
            set
            {
                if (_thermalRunaway != value)
                {
                    _thermalRunaway = value;
                    OnPropertyChanged(nameof(ThermalRunaway));
                }
            }
        }

        private bool _gasRelease;
        public bool GasRelease
        {
            get { return _gasRelease; }
            set
            {
                if (_gasRelease != value)
                {
                    _gasRelease = value;
                    OnPropertyChanged(nameof(GasRelease));
                }
            }
        }

        private bool _malodorousSubstances;
        public bool MalodorousSubstances
        {
            get { return _malodorousSubstances; }
            set
            {
                if (_malodorousSubstances != value)
                {
                    _malodorousSubstances = value;
                    OnPropertyChanged(nameof(MalodorousSubstances));
                }
            }
        }

        private bool _specialMeasures = false;
        public bool SpecialMeasures
        {
            get { return _specialMeasures; }
            set
            {
                if (_specialMeasures != value)
                {
                    _specialMeasures = value;
                    OnPropertyChanged(nameof(SpecialMeasures));
                }
            }
        }

        private string _fireExplosionText = "Keep away from naked flames and sources of ignition.";
        public string FireExplosionText
        {
            get { return _fireExplosionText; }
            set
            {
                if (_fireExplosionText != value)
                {
                    _fireExplosionText = value;
                    OnPropertyChanged(nameof(FireExplosionText));
                }
            }
        }

        private string _thermalRunawayText = "Dropwise addition by dropping funnel.";
        public string ThermalRunawayText
        {
            get { return _thermalRunawayText; }
            set
            {
                if (_thermalRunawayText != value)
                {
                    _thermalRunawayText = value;
                    OnPropertyChanged(nameof(ThermalRunawayText));
                }
            }
        }

        private string _gasReleaseText = "Keep the fumehood sash pulled down.";
        public string GasReleaseText
        {
            get { return _gasReleaseText; }
            set
            {
                if (_gasReleaseText != value)
                {
                    _gasReleaseText = value;
                    OnPropertyChanged(nameof(GasReleaseText));
                }
            }
        }

        private string _malodorousSubstancesText = "Perform all reactions in fume hood where possible and keep the fumehood sash pulled down.";
        public string MalodorousSubstancesText
        {
            get { return _malodorousSubstancesText; }
            set
            {
                if (_malodorousSubstancesText != value)
                {
                    _malodorousSubstancesText = value;
                    OnPropertyChanged(nameof(MalodorousSubstancesText));
                }
            }
        }

        private string _specialMeasuresText;
        public string SpecialMeasuresText
        {
            get { return _specialMeasuresText; }
            set
            {
                if (_specialMeasuresText != value)
                {
                    _specialMeasuresText = value;
                    OnPropertyChanged(nameof(SpecialMeasuresText));
                }
            }
        }

        private bool _halogenated;
        public bool Halogenated
        {
            get { return _halogenated; }
            set
            {
                if (_halogenated != value)
                {
                    _halogenated = value;
                    OnPropertyChanged(nameof(Halogenated));
                }
            }
        }

        private bool _hydrocarbon;
        public bool Hydrocarbon
        {
            get { return _hydrocarbon; }
            set
            {
                if (_hydrocarbon != value)
                {
                    _hydrocarbon = value;
                    OnPropertyChanged(nameof(Hydrocarbon));
                }
            }
        }

        private bool _contaminated;
        public bool Contaminated
        {
            get { return _contaminated; }
            set
            {
                if (_contaminated != value)
                {
                    _contaminated = value;
                    OnPropertyChanged(nameof(Contaminated));
                }
            }
        }

        private bool _aqueous;
        public bool Aqueous
        {
            get { return _aqueous; }
            set
            {
                if (_aqueous != value)
                {
                    _aqueous = value;
                    OnPropertyChanged(nameof(Aqueous));
                }
            }
        }

        private bool _named;
        public bool Named
        {
            get { return _named; }
            set
            {
                if (_named != value)
                {
                    _named = value;
                    OnPropertyChanged(nameof(Named));
                }
            }
        }

        private bool _silicaTLC;
        public bool SilicaTLC
        {
            get { return _silicaTLC; }
            set
            {
                if (_silicaTLC != value)
                {
                    _silicaTLC = value;
                    OnPropertyChanged(nameof(SilicaTLC));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
