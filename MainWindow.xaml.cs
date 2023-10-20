using COSHH_Generator.Scrapers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace COSHH_Generator
{
    class SubstanceEntry : INotifyPropertyChanged
    {
        public SubstanceEntry()
        {
            DisplayName = "";
        }
        public string _DisplayName = "";
        public string DisplayName { 
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
        public string query;
        public ObservableCollection<Result> _Results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results
        {
            get
            {
                return _Results;
            }
        }

        public string Amount { get; set; }
        public string AmountUnit { get; set; }

        public Result? _SelectedResult = null;
        public Result SelectedResult
        {
            set
            {
                Extract(value);
                _SelectedResult = value;
                DisplayName = value.SubstanceName;
            }
        }


        public void Search(in string query)
        {
            if (string.IsNullOrEmpty(query) || this.query == query) return;
            SigmaAldrich.SearchAsync(query, SetResults);
            
            //Trace.WriteLine("searhcing");

        }
        void SetResults(List<SigmaAldrich.Result> results)
        {
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
                    ProductName = result.name,
                    Link = null
                });

                for (int j = 0; j < result.products.Count; j++)
                {
                    SigmaAldrich.Result.Product product = result.products[j];
                    _Results.Add(new Result
                    {
                        ProductName = $"{j + 1}. {product.description}",
                        SubstanceName = result.name,
                        Link = product.link,
                    });
                }
            }
            OnPropertyChanged("Results");
        }

        public void Bind(ref TextBox amount, ref ComboBox amountUnit, ref ComboBox resultsComboBox, ref TextBox substance)
        {
            amount.TextChanged += (sender, e) => {
                TextBox? textBox = sender as TextBox;
                if (textBox != null)
                {
                    //MessageBox.Show(textBox.Text, textBox.Text, MessageBoxButton.OK);
                    Amount = textBox.Text;
                }

            };

            amountUnit.SelectionChanged += (sender, args) =>
            {
                //MessageBox.Show("dsads", ((Result)args.AddedItems[0]!).Name, MessageBoxButton.OK);
                AmountUnit = args.AddedItems[0]!.ToString()!;
            };

            resultsComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("Results"),
                Mode = BindingMode.OneWay

            });

            resultsComboBox.ItemContainerStyle = new Style(typeof(ComboBoxItem))
            {
                Setters =
                  {
                    new Setter(ComboBoxItem.IsEnabledProperty, new Binding("IsSelectable"))
                  }
            };

            resultsComboBox.SelectionChanged += (sender, args) =>
            {
                var addedItems = args.AddedItems;
                if(addedItems.Count > 0)
                {
                    SelectedResult = (Result)args.AddedItems[0]!;
                    
                }
 
            };

            substance.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => {
                if (e.Key == Key.Enter)
                {
                    Search(((TextBox)sender).Text);
                }
            });

            substance.LostFocus += (object sender, RoutedEventArgs e) =>
            {
                Search(((TextBox)sender).Text);
                
            };

        }

        public void Extract(in Result substance)
        {
            extractionTask = SDSParser.Extract(substance.Link);
            DisplayName = substance.SubstanceName;
        }
        public Task<SafetyData>? extractionTask = null;
        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        //public CancellationToken cancellationToken;   
    }
    struct Result
    {
        public string SubstanceName { get; set; }
        public string ProductName { get; set; }
        public string? Link { get; set; }
        public bool IsSelectable { get
            {
                return Link != null;
            }
        }

    }
    
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddNewSubstance();
            dateTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
            cachePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, ".cache");

            dateTextBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
            };
            substanceListBox.SelectionChanged += (sender, e) =>
            {
                substanceListBox.UnselectAll();
            };

            fireExplosionCheckBox.Checked += (sender, e) =>
            {
                fireExplosionTextBox.IsEnabled = true;
            };
            fireExplosionCheckBox.Unchecked += (sender, e) =>
            {
                fireExplosionTextBox.IsEnabled = false;
            };

            thermalRunawayCheckBox.Checked += (sender, e) =>
            {
                thermalRunawayTextBox.IsEnabled = true;
            };
            thermalRunawayCheckBox.Unchecked += (sender, e) =>
            {
                thermalRunawayTextBox.IsEnabled = false;
            };

            gasReleaseCheckBox.Checked += (sender, e) =>
            {
                gasReleaseTextBox.IsEnabled = true;
            };
            gasReleaseCheckBox.Unchecked += (sender, e) =>
            {
                gasReleaseTextBox.IsEnabled = false;
            };

            malodorousSubstancesCheckBox.Checked += (sender, e) =>
            {
                malodorousSubstancesTextBox.IsEnabled = true;
            };
            malodorousSubstancesCheckBox.Unchecked += (sender, e) =>
            {
                malodorousSubstancesTextBox.IsEnabled = false;
            };


            specialMeasuresCheckBox.Checked += (sender, e) =>
            {
                specialMeasuresTextBox.IsEnabled = true;
            };
            specialMeasuresCheckBox.Unchecked += (sender, e) =>
            {
                specialMeasuresTextBox.IsEnabled = false;
            };

            try
            { 
                if(File.Exists(cachePath))
                {
                    var text = File.ReadAllText(cachePath).Trim().Split(';');
                    nameTextBox.Text = text[0];
                    collegeTextBox.Text = text[1];
                    yearTextBox.Text = text[2];
                }
            }catch(Exception ex) { }
        }

        List<SubstanceEntry> substanceEntries = new List<SubstanceEntry>();

        private void OnAddNewSubstancePressed(object sender, RoutedEventArgs e)
        {
            AddNewSubstance();
        }

        private void AddNewSubstance(in TextBox? prevDisplayName = null)
        {
            substanceEntries.Add(new SubstanceEntry());
            var index = substanceEntries.Count - 1;
            var substance = substanceEntries.Last();
            ListBoxItem item = new ListBoxItem();
            var grid = new Grid();
            grid.Width = 1000;
            grid.Margin = new Thickness(10);
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.06, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.44, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.1, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.1, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.1, GridUnitType.Star)
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.2, GridUnitType.Star)
            });


            var substanceQueryLabel = new TextBlock();
            substanceQueryLabel.Focusable = false;
            substanceQueryLabel.Text = "Query:";
            grid.Children.Add(substanceQueryLabel);
            Grid.SetRow(substanceQueryLabel, 0);
            Grid.SetColumn(substanceQueryLabel, 0);

            var substanceQuery = new TextBox();
            substanceQuery.Focusable = true;
            substanceQuery.Margin = new Thickness(0, 0, 10, 5);
            grid.Children.Add(substanceQuery);
            Grid.SetRow(substanceQuery, 0);
            Grid.SetColumn(substanceQuery, 1);

            var amountLabel = new TextBlock();
            amountLabel.Text = "Mass/Volume:";
            grid.Children.Add(amountLabel);
            Grid.SetRow(amountLabel, 0);
            Grid.SetColumn(amountLabel, 2);

            var amount = new TextBox();
            amount.Margin = new Thickness(0, 0, 5, 5);
            //amount.PreviewGotKeyboardFocus += (sender, e) =>
            //{
            //    if (index + 2 > substanceEntries.Count)
            //    {
            //        AddNewSubstance(null, null);
            //    }
            //};
            grid.Children.Add(amount);
            Grid.SetRow(amount, 0);
            Grid.SetColumn(amount, 3);

            var amountUnit = new ComboBox();
            amountUnit.IsTabStop = false;
            amountUnit.Items.Add("mg");
            amountUnit.Items.Add("mL");
            amountUnit.Items.Add("g");
            amountUnit.Items.Add("cm³");
            amountUnit.Items.Add("L");
            amountUnit.Margin = new Thickness(0, 0, 5, 5);
            grid.Children.Add(amountUnit);
            Grid.SetRow(amountUnit, 0);
            Grid.SetColumn(amountUnit, 4);

            var resultsLabel = new TextBlock();
            resultsLabel.Text = "Results:";
            grid.Children.Add(resultsLabel);
            Grid.SetRow(resultsLabel, 1);
            Grid.SetColumn(resultsLabel, 0);
            

            var resultsComboBox = new ComboBox();
            //resultsComboBox.IsTabStop = false;
            resultsComboBox.KeyDown += (sender, e) =>
            {
                if(e.Key == Key.Enter || e.Key == Key.Space)
                {
                    resultsComboBox.IsDropDownOpen = true;
                }
            };
            resultsComboBox.DisplayMemberPath = "ProductName";
            resultsComboBox.Margin = new Thickness(0, 0, 5, 5);
            grid.Children.Add(resultsComboBox);
            Grid.SetRow(resultsComboBox, 1);
            Grid.SetColumn(resultsComboBox, 1);
            Grid.SetColumnSpan(resultsComboBox, 4);

            var displayNameGrid = new Grid();
            displayNameGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.15, GridUnitType.Star)
            });
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.85, GridUnitType.Star)
            });
            var displayNameLabel = new TextBlock { Text = "Display Name:" };
            displayNameGrid.Children.Add(displayNameLabel);
            Grid.SetColumn(displayNameLabel, 0);
            grid.Children.Add(displayNameGrid);
            Grid.SetColumn(displayNameGrid, 0);
            Grid.SetRow(displayNameGrid, 2);
            Grid.SetColumnSpan(displayNameGrid, 5);

            var displayName = new TextBox();
            displayName.Margin = new Thickness(0, 0, 5, 0);
            displayNameGrid.Children.Add(displayName);
            Grid.SetColumn(displayName, 1);
            Binding binding = new Binding("DisplayName");
            
            binding.Source = substance;
            binding.Mode = BindingMode.TwoWay;
            displayName.SetBinding(TextBox.TextProperty, binding);
            displayName.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                    if (index + 2 > substanceEntries.Count)
                    {
                        AddNewSubstance();
                    }
                }
                
            };
            
            Button deleteSubstanceButton = new Button()
            {
                Content = "Delete",
                IsTabStop = false
            };
            deleteSubstanceButton.Click += (sender, e) =>
            {
                substanceEntries.Remove(substance);
                substanceListBox.Items.Remove(item);
            };
            grid.Children.Add(deleteSubstanceButton);
            Grid.SetColumn(deleteSubstanceButton, 6);
            Grid.SetRow(deleteSubstanceButton, 0);
            Grid.SetRowSpan(deleteSubstanceButton, 3);

            substanceEntries.Last().Bind(ref amount, ref amountUnit, ref resultsComboBox, ref substanceQuery);
            
            
            substanceListBox.Items.Insert(substanceListBox.Items.Count - 1, grid);
            
        }

        private void Clear(object? sender, RoutedEventArgs? e)
        {
            substanceEntries.Clear();
            substanceListBox.Items.Clear();
            AddNewSubstance();
        }

        Task? generateTask = null;
        string cachePath;
        private void Generate(object? sender, RoutedEventArgs? e)
        {
            generateButton.IsEnabled = false;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "COSHH.docx");
            generateTask = COSHHForm.Generate(titleTextBox.Text, nameTextBox.Text, collegeTextBox.Text, yearTextBox.Text, dateTextBox.Text,
                fireExplosionCheckBox.IsChecked, thermalRunawayCheckBox.IsChecked, gasReleaseCheckBox.IsChecked, malodorousSubstancesCheckBox.IsChecked, specialMeasuresCheckBox.IsChecked,
                fireExplosionTextBox.Text, thermalRunawayTextBox.Text, gasReleaseTextBox.Text, malodorousSubstancesTextBox.Text, specialMeasuresTextBox.Text,
                halogenatedCheckBox.IsChecked, hydrocarbonCheckBox.IsChecked, contaminatedCheckBox.IsChecked, aqueousCheckBox.IsChecked, namedWasteCheckBox.IsChecked, silicaTLCCheckBox.IsChecked,
                substanceEntries, path, () =>
                {
                    generateButton.IsEnabled = true;
                    generateTask = null;
                });
            
            using (StreamWriter sw = new StreamWriter(File.Open(cachePath, FileMode.OpenOrCreate)))
            {
                sw.WriteLine($"{nameTextBox.Text};{collegeTextBox.Text};{yearTextBox.Text}");
            }
            if (!File.GetAttributes(cachePath).HasFlag(FileAttributes.Hidden))
            {
                File.SetAttributes(cachePath, FileAttributes.Hidden);
            }
            
        }
        
    }
}
