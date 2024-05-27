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


namespace COSHH_Generator
{
    public class SubstanceEntry : INotifyPropertyChanged
    {
        public SubstanceEntry()
        {
            OnPropertyChanged("DisplayName");
        }
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

        private string _Odour = "";
        public string Odour
        {
            get => _Odour;
            set
            {
                _Odour = value;
                OnPropertyChanged("Odour");
            }
        }

        public Task<SafetyData>? extractionTask = null;
        private ObservableCollection<Result> _Results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results
        {
            get
            {
                return _Results;
            }

        }

        public void SetResults(List<SigmaAldrich.Result> results)
        {
            Trace.WriteLine("Setting results");
            _Results.Clear();
            if (results.Count == 0)
            {
                _Results.Add(new Result { ProductName = "No Results" });
                return;
            }

            foreach (var result in results)
            {
                _Results.Add(new Result
                {
                    ProductName = result.Name,
                    Link = null
                });

                for (int j = 0; j < result.Products.Count; j++)
                {
                    SigmaAldrich.Result.Product product = result.Products[j];
                    _Results.Add(new Result
                    {
                        ProductName = $"{j + 1}. {product.Description}",
                        SubstanceName = result.Name,
                        Link = product.Link,
                    });
                }
            }
            OnPropertyChanged("Results");
        }

        public SafetyData? safetyData = null;
        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    public struct Result
    {
        public string SubstanceName { get; set; }
        public string ProductName { get; set; }
        public string? Link { get; set; }
        public bool IsSelectable
        {
            get
            {
                return Link != null;
            }
        }

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

        public async void Search(string query)
        {
            query = query.Trim();
            if (currentQuery == query) return;
            else if (string.IsNullOrEmpty(query))
            {
                substanceEntry.SetResults(new List<SigmaAldrich.Result>());
                return;
            }
            // Cancel the current search task
            if (searchTask is not null)
            {
                searchTokenSource!.Cancel();
                searchTask.Wait();
                searchTokenSource!.Dispose();
            }
            // Initiate new search task
            currentQuery = query;
            substanceEntry.DisplayName = query;
            searchTokenSource = new CancellationTokenSource();

            searchTask = Task.Run(() => SigmaAldrich.SearchAsync(query, searchTokenSource.Token));
            List<SigmaAldrich.Result> results = await searchTask;
            substanceEntry.SetResults(results);
            ResultsComboBox.IsDropDownOpen = true;
            ResultsComboBox.Focus();
            searchTask = null;
            searchTokenSource!.Dispose();

        }

        private async void Extract(Result substance)
        {
            if (substanceEntry.extractionTask is not null)
            {
                extractionTokenSource!.Cancel();
                substanceEntry.extractionTask.Wait();
                extractionTokenSource!.Dispose();
            }

            substanceEntry.Odour = "";
            substanceEntry.safetyData = null;
            extractionTokenSource = new CancellationTokenSource();
            bool success = true;
            substanceEntry.extractionTask = Task.Run(() => SDSParser.Extract(substance.Link!, extractionTokenSource.Token,
                () => {
                    success = false;
                }));
            
            var safetyData = await substanceEntry.extractionTask;
            substanceEntry.extractionTask = null;
            extractionTokenSource!.Dispose();
            if (success)
            {
                substanceEntry.safetyData = safetyData;
                substanceEntry.DisplayName = substance.SubstanceName;
                substanceEntry.Odour = substanceEntry.safetyData.Odour;
            } else
            {
                MessageBox.Show($"Failed to extract {substance.ProductName}", "Extraction Failure", MessageBoxButton.OK);
                _SelectedResult = null;
                substanceEntry.DisplayName = "";
                ResultsComboBox.SelectedIndex = -1;
            }
            
            
        }

        private void Bind()
        {
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

            SearchQueryTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            SearchQueryTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;

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
            DisplayNameTextBox.Clear();
            AmountTextBox.Clear();
        }

        private void UsePoolRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ChemicalPoolComboBox.IsEnabled = true;
            SearchQueryTextBox.IsEnabled = false;
            ResultsComboBox.IsEnabled = false;
 
            if (ChemicalPoolComboBox.ItemsSource == null)
            {
                ChemicalPoolComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("Substances")
                {
                    Source = chemicalPool.Value,
                });
            }
            // Assuming `substanceEntries` and `substance` are defined and accessible in your context
            // substanceEntries.Last().UseChemicalPool = true;
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
        private Task<List<SigmaAldrich.Result>>? searchTask;

        private CancellationTokenSource? extractionTokenSource = null;
        private CancellationTokenSource? searchTokenSource = null;

        private Result? _SelectedResult = null;
        public Result SelectedResult
        {
            set
            {
               Extract(value);
               _SelectedResult = value;
               substanceEntry.DisplayName = value.SubstanceName;
            }
        }

        
    }
}
