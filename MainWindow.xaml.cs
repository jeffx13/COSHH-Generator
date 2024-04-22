using com.itextpdf.text.pdf;
using COSHH_Generator.Scrapers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shell;

namespace COSHH_Generator
{

    class SubstanceEntry : INotifyPropertyChanged
    {
        public SubstanceEntry()
        {
            OnPropertyChanged("DisplayName");
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

        public string DisplayNameAndAmount
        {
            get
            {
                return $"{_DisplayName} {Amount}";
            }
        }

        public ObservableCollection<Result> _Results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Results
        {
            get
            {
                return _Results;
            }
        }

        public string _Amount = "";
        public string Amount {
            get => _Amount;
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        string _Odour = "";
        public string Odour
        {
            get => _Odour;
            set
            {
                _Odour = value;
                OnPropertyChanged("Odour");
            }
        }

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

        void SetResults(List<SigmaAldrich.Result> results)
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

        public void Bind(ref TextBox amountTextBox, ref ComboBox resultsComboBox, ref TextBox substanceTextBox, ref ComboBox chemicalPoolComboBox, ref TextBox odourTextBox)
        {
            amountTextBox.SetBinding(TextBox.TextProperty, new Binding("Amount")
            {
                Source = this,
                Mode = BindingMode.TwoWay,
            });
            
            odourTextBox.SetBinding(TextBox.TextProperty, new Binding("Odour")
            {
                Source = this,
                Mode = BindingMode.OneWay,
            });
            

            resultsComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("Results"),
                Mode = BindingMode.OneWay
            });

            resultsComboBox.IsEnabled = false;

            resultsComboBox.SetBinding(ComboBox.IsEnabledProperty, new Binding("IsNotSearching")
            {
                Source = this, 
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
                if (addedItems.Count > 0)
                {
                    SelectedResult = (Result)args.AddedItems[0]!;
                }
            };

            substanceTextBox.KeyDown += new KeyEventHandler((object sender, KeyEventArgs e) => {
                if (e.Key == Key.Enter)
                {
                    Search(((TextBox)sender).Text);
                }
            });


            substanceTextBox.LostFocus += (object sender, RoutedEventArgs e) =>
            {
                Search(((TextBox)sender).Text);

            };

            substanceTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            substanceTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;

            chemicalPoolComboBox.SelectionChanged += (sender, args) =>
            {
                var addedItems = args.AddedItems;
                if (addedItems.Count > 0)
                {
                    chemicalPoolSubstance = (SubstanceEntry)args.AddedItems[0]!;
                    extractionTask = chemicalPoolSubstance.extractionTask;
                    DisplayName = chemicalPoolSubstance.DisplayName;
                    Amount = chemicalPoolSubstance.Amount;
                }
            };
            OnPropertyChanged("IsNotSearching");
        }

        string currentQuery = string.Empty;

        public async void Search(string query)
        {
            query = query.Trim();
            if (string.IsNullOrEmpty(query) || currentQuery == query) return;

            if (searchTask is not null)
            {
                searchTokenSource!.Cancel();
                searchTask.Wait();
                searchTokenSource!.Dispose();
            }
            currentQuery = query;
            DisplayName = query;
            searchTokenSource = new CancellationTokenSource();
            searchTask = Task.Run(() => SigmaAldrich.SearchAsync(query, searchTokenSource.Token));
            OnPropertyChanged("IsNotSearching");
            List<SigmaAldrich.Result> results = await searchTask;
            SetResults(results);
            searchTask = null;
            searchTokenSource!.Dispose();
            OnPropertyChanged("IsNotSearching");
        }

        public async void Extract(Result substance)
        {
            if (extractionTask is not null)
            {
                extractionTokenSource!.Cancel();
                extractionTask.Wait();
                extractionTokenSource!.Dispose();
            }
            Odour = "";
            safetyData = null;
            extractionTokenSource = new CancellationTokenSource();
            extractionTask = Task.Run(() => SDSParser.Extract(substance.Link!, extractionTokenSource.Token));
            safetyData = await extractionTask;
            extractionTask = null;
            extractionTokenSource!.Dispose();
            DisplayName = substance.SubstanceName;
        }

       
        public bool IsNotSearching
        {
            get => searchTask == null;
        }

        public SubstanceEntry? chemicalPoolSubstance = null;
        public bool UseChemicalPool = false;
        private Task<List<SigmaAldrich.Result>>? searchTask;
        private CancellationTokenSource? extractionTokenSource = null;
        private CancellationTokenSource? searchTokenSource = null;
        public Task<SafetyData>? extractionTask = null;
        public SafetyData? safetyData = null;
        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        
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
            Task.Run(SigmaAldrich.init);
            yearTextBox.MouseEnter    += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            yearTextBox.MouseLeave    += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            collegeTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            collegeTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            nameTextBox.MouseEnter    += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            nameTextBox.MouseLeave    += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            titleTextBox.MouseEnter   += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            titleTextBox.MouseLeave   += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            dateTextBox.MouseEnter    += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            dateTextBox.MouseLeave    += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;

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
            }catch{ }
        }

        ChemicalPool chemicalPool = new ChemicalPool();

        List<SubstanceEntry> substanceEntries = new List<SubstanceEntry>();

        private void OnAddNewSubstancePressed(object sender, RoutedEventArgs e)
        {
            AddNewSubstance();
        }
        int searchOrPoolGroupIndex = 0;
        private void AddNewSubstance()
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

            var searchOrPoolRow = new Grid();
            searchOrPoolRow.RowDefinitions.Add(new RowDefinition());
            searchOrPoolRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.1, GridUnitType.Star)
            });
            searchOrPoolRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.1, GridUnitType.Star)
            });
            searchOrPoolRow.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.8, GridUnitType.Star)
            });
            
            var searchRadioButton = new RadioButton()
            {
                Content = new TextBlock()
                {
                    Text = "Search"
                },
                GroupName = $"searchOrPool{searchOrPoolGroupIndex}",
                IsChecked = true
            };
            searchOrPoolRow.Children.Add(searchRadioButton);
            Grid.SetRow(searchRadioButton, 0);
            Grid.SetColumn(searchRadioButton, 0);

            


            var usePoolRadioButton = new RadioButton()
            {
                Content = new TextBlock()
                {
                    Text = "Use Pool"
                },
                GroupName = $"searchOrPool{searchOrPoolGroupIndex++}"
            };
            
            searchOrPoolRow.Children.Add(usePoolRadioButton);
            Grid.SetRow(usePoolRadioButton, 0);
            Grid.SetColumn(usePoolRadioButton, 1);
            

            var chemicalPoolComboBox = new ComboBox()
            {
                IsEnabled = false,
                IsTextSearchEnabled = true,
                IsTextSearchCaseSensitive = false,
                IsEditable = true,
                DisplayMemberPath = "DisplayNameAndAmount"
            };
            chemicalPoolComboBox.IsTabStop = false;
            chemicalPoolComboBox.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                    var cmbTextBox = (TextBox)chemicalPoolComboBox.Template.FindName("PART_EditableTextBox", chemicalPoolComboBox);
                    cmbTextBox.CaretIndex = cmbTextBox.Text.Length;
                }
                else
                {
                    if(chemicalPoolComboBox.IsDropDownOpen == false)
                    {
                        chemicalPoolComboBox.IsDropDownOpen = true;
                    }
                }

            };
            chemicalPoolComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("Substances")
            {
                Source = chemicalPool,
            });
            var ItemsPanel = new ItemsPanelTemplate();
            var stackPanelTemplate = new FrameworkElementFactory(typeof(VirtualizingStackPanel));
            ItemsPanel.VisualTree = stackPanelTemplate;
            chemicalPoolComboBox.ItemsPanel = ItemsPanel;
            chemicalPoolComboBox.PreviewMouseWheel += (sender, e) => { e.Handled = !((ComboBox)sender).IsDropDownOpen; };

            searchOrPoolRow.Children.Add(chemicalPoolComboBox);
            Grid.SetRow(chemicalPoolComboBox, 0);
            Grid.SetColumn(chemicalPoolComboBox, 2);

            grid.Children.Add(searchOrPoolRow);
            Grid.SetRow(searchOrPoolRow, 0);
            Grid.SetColumn(searchOrPoolRow, 0);
            Grid.SetColumnSpan(searchOrPoolRow, 5);

            searchOrPoolRow.Margin = new Thickness(0, 0, 5, 5);

            var seachQueryLabel = new TextBlock();
            seachQueryLabel.Focusable = false;
            seachQueryLabel.Text = "Query:";
            grid.Children.Add(seachQueryLabel);
            Grid.SetRow(seachQueryLabel, 1);
            Grid.SetColumn(seachQueryLabel, 0);

            var searchQueryTextBox = new TextBox();
            searchQueryTextBox.Focusable = true;
            searchQueryTextBox.Margin = new Thickness(0, 0, 10, 5);
            grid.Children.Add(searchQueryTextBox);
            Grid.SetRow(searchQueryTextBox, 1);
            Grid.SetColumn(searchQueryTextBox, 1);

            var amountLabel = new TextBlock();
            amountLabel.Text = "Mass/Volume:";
            grid.Children.Add(amountLabel);
            Grid.SetRow(amountLabel, 1);
            Grid.SetColumn(amountLabel, 2);

            var amountTextBox = new TextBox();
            amountTextBox.Margin = new Thickness(0, 0, 5, 5);
            grid.Children.Add(amountTextBox);
            Grid.SetRow(amountTextBox, 1);
            Grid.SetColumn(amountTextBox, 3);

            

            var resultsLabel = new TextBlock();
            resultsLabel.Text = "Results:";
            grid.Children.Add(resultsLabel);
            Grid.SetRow(resultsLabel, 2);
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
            resultsComboBox.PreviewMouseWheel += (sender, e) =>
            {
                e.Handled = !((ComboBox)sender).IsDropDownOpen;
            };
            

            grid.Children.Add(resultsComboBox);
            Grid.SetRow(resultsComboBox, 2);
            Grid.SetColumn(resultsComboBox, 1);
            Grid.SetColumnSpan(resultsComboBox, 4);

            var displayNameGrid = new Grid();
            displayNameGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.10, GridUnitType.Star)
            });
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.60, GridUnitType.Star)
            });
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.05, GridUnitType.Star)
            });
            displayNameGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(0.25, GridUnitType.Star)
            });
            var displayNameLabel = new TextBlock { Text = "Display Name:" };
            displayNameGrid.Children.Add(displayNameLabel);
            Grid.SetColumn(displayNameLabel, 0);

            var odourLabel = new TextBlock { Text = "Odour:" };
            displayNameGrid.Children.Add(odourLabel);
            Grid.SetColumn(odourLabel, 2);

            var odourTextBox = new TextBox { IsReadOnly = true };
            odourTextBox.Margin = new Thickness(0, 0, 5, 0);
            displayNameGrid.Children.Add(odourTextBox);
            Grid.SetColumn(odourTextBox, 3);

            grid.Children.Add(displayNameGrid);
            Grid.SetRow(displayNameGrid, 3);
            Grid.SetColumn(displayNameGrid, 0);
            Grid.SetColumnSpan(displayNameGrid, 5);

            var displayNameTextBox = new TextBox();
            displayNameTextBox.Margin = new Thickness(0, 0, 5, 0);
            displayNameGrid.Children.Add(displayNameTextBox);
            Grid.SetColumn(displayNameTextBox, 1);

            displayNameTextBox.SetBinding(TextBox.TextProperty, new Binding("DisplayName")
            {
                Source = substance,
                Mode = BindingMode.TwoWay,
            });
            displayNameTextBox.PreviewKeyDown += (sender, e) =>
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
                substanceListBox.Items.Remove(grid);
            };
            grid.Children.Add(deleteSubstanceButton);
            Grid.SetColumn(deleteSubstanceButton, 6);
            Grid.SetRow(deleteSubstanceButton, 0);
            Grid.SetRowSpan(deleteSubstanceButton, 4);

            amountTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            amountTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            odourTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            odourTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            displayNameTextBox.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.IBeam;
            displayNameTextBox.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
            

            substanceEntries.Last().Bind(ref amountTextBox, ref resultsComboBox, ref searchQueryTextBox, ref chemicalPoolComboBox, ref odourTextBox);
            usePoolRadioButton.Checked += (sender, e) =>
            {
                chemicalPoolComboBox.IsEnabled = true;
                searchQueryTextBox.IsEnabled = false;
                resultsComboBox.IsEnabled = false;
                substanceEntries.Last().UseChemicalPool = true;
            };
            searchRadioButton.Checked += (sender, e) =>
            {
                chemicalPoolComboBox.IsEnabled = false;
                searchQueryTextBox.IsEnabled = true;
                resultsComboBox.IsEnabled = true;
                displayNameTextBox.Clear();
                amountTextBox.Clear();
                substanceEntries.Last().UseChemicalPool = false;
            };
            
            
            substanceListBox.Items.Insert(substanceListBox.Items.Count - 1, grid);
            
        }
        void s_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is ListBoxItem)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }
        void listbox1_Drop(object sender, DragEventArgs e)
        {
            var droppedData = e.Data.GetData(typeof(SubstanceEntry)) as SubstanceEntry;
            SubstanceEntry target = ((ListBoxItem)(sender)).DataContext as SubstanceEntry;

            int removedIdx = substanceListBox.Items.IndexOf(droppedData);
            int targetIdx = substanceListBox.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                substanceEntries.Insert(targetIdx + 1, droppedData);
                substanceEntries.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (substanceEntries.Count + 1 > remIdx)
                {
                    substanceEntries.Insert(targetIdx, droppedData);
                    substanceEntries.RemoveAt(remIdx);
                }
            }
        }
        private void Clear(object? sender, RoutedEventArgs? e)
        {
            substanceEntries.Clear();
            var delete = substanceListBox.Items[substanceListBox.Items.Count-1];
            substanceListBox.Items.Clear();
            substanceListBox.Items.Add(delete);
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
