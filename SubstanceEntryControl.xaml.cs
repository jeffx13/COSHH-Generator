using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using COSHH_Generator.Core;
using static COSHH_Generator.Core.SigmaAldrich;
using System.Linq;


namespace COSHH_Generator
{
    public class SubstanceEntry : INotifyPropertyChanged
    {
        public SubstanceEntry()
        {
            OnPropertyChanged("DisplayName");
        }
        static ObservableCollection<SDSProvider> _SDSProviders = new ObservableCollection<SDSProvider> { new SigmaAldrich(), new Fisher() };
        public static ObservableCollection<SDSProvider> SDSProviders
        {
            get
            {
                return _SDSProviders;
            }
        }
        public SDSProvider Provider = _SDSProviders[0];
        public string _DisplayName = "";
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
                OnPropertyChanged("DisplayName");

            }
        }

        public string DisplayNameAndAmount
        {
            get
            {
                return $"{_DisplayName} {Amount}";
            }
        }

        public string _Amount = "";
        public string Amount
        {
            get => _Amount;
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public string Odour
        {
            get => _safetyData != null ? _safetyData.Odour : _extractionTask != null ? "Extracting..." : "";
        }

        private Task<SafetyData>? _extractionTask = null;
        public Task<SafetyData>? ExtractionTask {
            get => _extractionTask;
            set
            {
                _extractionTask = value;
                OnPropertyChanged("Odour");
            }
        }

        private ObservableCollection<Result> _Results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results
        {
            get
            {
                return _Results;
            }
        }


        public void invalidateResult(string link)
        {
            var found = _Results.FirstOrDefault(x => x.Link == link);
            found.Link = null;
            Trace.WriteLine(_Results.FirstOrDefault(x => x.Link == null));
            OnPropertyChanged("Results");
        }

        

        public void SetResults(List<Result> results)
        {
            Trace.WriteLine("Setting results");
            _Results.Clear();
            _Results = new ObservableCollection<Result>(results);
            OnPropertyChanged("Results");
        }

        private SafetyData? _safetyData = null;
        public SafetyData? safetyData
        {
            get => _safetyData;
            set
            {
                _safetyData = value;
                OnPropertyChanged("Odour");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    
    public class Result: INotifyPropertyChanged
    {
        public string SubstanceName { get; set; }
        public string ProductName { get; set; }
        public string? Link {
            get => _Link;
            set  
            {
                _Link = value;
                OnPropertyChanged("IsSelectable");
            }
        }
        private string? _Link = null;

        public bool IsSelectable
        {
            get
            {
                return Link != null;
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    }

    public partial class SubstanceEntryControl : UserControl, IDisposable
    {
        public SubstanceEntryControl()
        {
            DataContext = substanceEntry;
            InitializeComponent();
            AttachEventHandlers();
            Bind();
        }

        


        private async void Search(string query, bool force = false)
        {
            query = query.Trim();
            if (string.IsNullOrEmpty(query) || (!force && currentQuery == query) ) return;
            
            // Cancel the current search task
            if (searchTask is not null)
            {
                searchTokenSource!.Cancel();
                searchTask.Wait();
                searchTokenSource!.Dispose();
            }
            // Initiate new search task
            currentQuery = query;
            searchTokenSource = new CancellationTokenSource();

            searchTask = Task.Run(() => substanceEntry.Provider.SearchAsync(query, searchTokenSource.Token));
            List<Result> results = await searchTask;
            substanceEntry.SetResults(results);
            
            if (results.Any())
            {
                ResultsComboBox.IsEnabled = true;
                ResultsComboBox.IsDropDownOpen = true;
                ResultsComboBox.Focus();
            } else
            {
                ResultsComboBox.SelectedIndex = 0;
                ResultsComboBox.IsEnabled = false;
            }
            searchTask = null;
            searchTokenSource!.Dispose();

        }

        private async void Extract(Result substanceResult)
        {
            if (substanceEntry.ExtractionTask is not null)
            {
                extractionTokenSource!.Cancel();
                substanceEntry.ExtractionTask.Wait();
                extractionTokenSource!.Dispose();
            }

            substanceEntry.safetyData = null;
            extractionTokenSource = new CancellationTokenSource();
            bool success = true;
            substanceEntry.ExtractionTask = Task.Run(() => substanceEntry.Provider.ExtractAsync(substanceResult.Link!, extractionTokenSource.Token,
                (string errorMessage) => {
                    MessageBox.Show(errorMessage, $"Extraction failed: \"{substanceResult.ProductName}\"", MessageBoxButton.OK);
                    success = false;
                }));

            substanceEntry.safetyData = await substanceEntry.ExtractionTask;
            substanceEntry.ExtractionTask = null;
            extractionTokenSource!.Dispose();
            if (substanceEntry.safetyData.SubstanceName.Any())
            {
                substanceEntry.DisplayName = substanceEntry.safetyData.SubstanceName;
            }
            

            if (!success)
            {
                if (_SelectedResult != null)
                {
                    substanceEntry.invalidateResult(_SelectedResult.Link!);
                }
                _SelectedResult = null;
                substanceEntry.DisplayName = "";
                ResultsComboBox.SelectedIndex = -1;
            } 
        }

        private void Bind()
        {
            SDSProviderComboBox.SelectedIndex = 0;
            ResultsComboBox.KeyDown += (sender, e) =>
            {
                if ((e.Key == Key.Enter || e.Key == Key.Space) && substanceEntry.Results.Count > 0)
                {
                    ((ComboBox)sender).IsDropDownOpen = !((ComboBox)sender).IsDropDownOpen;
                }
            };

            ResultsComboBox.ItemContainerStyle = new Style(typeof(ComboBoxItem))
            {
                Setters =
                  {
                    new Setter(ComboBoxItem.IsEnabledProperty, new Binding("IsSelectable"))
                  }
            };

            ResultsComboBox.SelectionChanged += (sender, args) =>
            {
                var addedItems = args.AddedItems;
                if (addedItems.Count > 0)
                {
                    SelectedResult = (Result)args.AddedItems[0]!;
                }
            };

            SDSProviderComboBox.SelectionChanged += (sender, args) =>
            {
                var addedItems = args.AddedItems;
                if (addedItems.Count > 0)
                {
                    substanceEntry.Provider = (SDSProvider)args.AddedItems[0]!;
                    if (SearchQueryTextBox.Text.Length > 0)
                    {
                        if (substanceEntry.ExtractionTask is not null)
                        {
                            extractionTokenSource!.Cancel();

                            substanceEntry.ExtractionTask.Wait();
                            extractionTokenSource!.Dispose();
                        }
                        Search(SearchQueryTextBox.Text, true);
                    }
                }
            };

            SearchQueryTextBox.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => {
                if (e.Key == Key.Enter)
                {
                    Search(((TextBox)sender).Text);
                }
            });

            SearchQueryTextBox.LostFocus += (object sender, RoutedEventArgs e) =>
            {
                Search(SearchQueryTextBox.Text);
            };

            ChemicalPoolComboBox.SelectionChanged += (sender, args) =>
            {
                var addedItems = args.AddedItems;
                if (addedItems.Count > 0)
                {
                    var chemicalPoolSubstance = (SubstanceEntry)args.AddedItems[0]!;
                    substanceEntry.safetyData = chemicalPoolSubstance.safetyData;
                    substanceEntry.DisplayName = chemicalPoolSubstance.DisplayName;
                    substanceEntry.Amount = chemicalPoolSubstance.Amount;
                }
            };
        }

        private void AttachEventHandlers()
        {
            SearchRadioButton.Checked += SearchRadioButton_Checked;
            UsePoolRadioButton.Checked += UsePoolRadioButton_Checked;
            UsePoolRadioButton.GroupName = $"SearchOrPool{SearchOrPoolGroupIndex}";
            SearchRadioButton.GroupName = $"SearchOrPool{SearchOrPoolGroupIndex++}";

            ChemicalPoolComboBox.PreviewKeyDown += ChemicalPoolComboBox_PreviewKeyDown;
            ChemicalPoolComboBox.PreviewMouseWheel += (s, e) => { e.Handled = !((ComboBox)s).IsDropDownOpen; };
            ResultsComboBox.PreviewMouseWheel += (s, e) => { e.Handled = !((ComboBox)s).IsDropDownOpen; };

        }

        private void ChemicalPoolComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                TextBox cmbTextBox = (TextBox) ChemicalPoolComboBox.Template.FindName("PART_EditableTextBox", ChemicalPoolComboBox);
                cmbTextBox.CaretIndex = cmbTextBox.Text.Length;
            }
            else if (!ChemicalPoolComboBox.IsDropDownOpen)
            {
                ChemicalPoolComboBox.IsDropDownOpen = true;
            }
        }

        private void SearchRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ChemicalPoolComboBox.IsEnabled = false;
            SearchQueryTextBox.IsEnabled = true;
            ResultsComboBox.IsEnabled = true;
            SDSProviderComboBox.IsEnabled = true;

            substanceEntry.safetyData = null;
            substanceEntry.Amount = "";
            
            if (_SelectedResult != null && cachedSafetyData != null)
            {
                substanceEntry.DisplayName = _SelectedResult.SubstanceName;
                substanceEntry.safetyData = cachedSafetyData;
            }
            
        }

        private void UsePoolRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ChemicalPoolComboBox.IsEnabled = true;
            SearchQueryTextBox.IsEnabled = false;
            SDSProviderComboBox.IsEnabled = false;
            ResultsComboBox.IsEnabled = false;
            
            if (_SelectedResult != null)
            {
                cachedSafetyData = substanceEntry.safetyData;
            }

            if (ChemicalPoolComboBox.ItemsSource == null)
            {
                ChemicalPoolComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("Substances")
                {
                    Source = chemicalPool.Value,
                });
            }
            
            if (ChemicalPoolComboBox.SelectedIndex != -1)
            {
                var chemicalPoolSubstance = (SubstanceEntry) ChemicalPoolComboBox.SelectedItem;
                substanceEntry.safetyData = chemicalPoolSubstance.safetyData;
                substanceEntry.DisplayName = chemicalPoolSubstance.DisplayName;
                substanceEntry.Amount = chemicalPoolSubstance.Amount;
            }
            
        }

        private void onEnterTextBox(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.IBeam;
        }

        private void onLeaveTextBox(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public void Dispose()
        {
            Trace.WriteLine("disposed");
        }

        static Lazy<ChemicalPool> chemicalPool = new Lazy<ChemicalPool>();
        public static int SearchOrPoolGroupIndex = 0;

        string currentQuery = string.Empty;
        public SubstanceEntry substanceEntry = new SubstanceEntry();
        private Task<List<Result>>? searchTask;

        private CancellationTokenSource? extractionTokenSource = null;
        private CancellationTokenSource? searchTokenSource = null;

        private Result? _SelectedResult = null;
        public Result SelectedResult
        {
            set
            {
                if (value.Link == null) return;
                Extract(value);
                _SelectedResult = value;
                substanceEntry.DisplayName = value.SubstanceName;
            }
        }

        private SafetyData? cachedSafetyData = null;


    }
}
