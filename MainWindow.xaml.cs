using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using COSHH_Generator.Core;


namespace COSHH_Generator
{
    
    public partial class MainWindow : Window
    {
        private string cachePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, ".cache");
        List<SubstanceEntry> substanceEntries = new List<SubstanceEntry>();
        Config config = new Config();
        
        public MainWindow()
        {
            Task.Run(() =>
            {
                Assembly.Load("iTextSharp");
                Assembly.Load("System.Linq.Expressions");
                Assembly.Load("DocumentFormat.OpenXml");
                Assembly.Load("System.IO.Compression");
            });
            InitializeComponent();
            DataContext = config;
            config.Date = DateTime.Today.ToString("dd/MM/yyyy");
            if (File.Exists(cachePath))
            {
                try
                {
                    var text = File.ReadAllText(cachePath).Trim().Split(';');
                    config.Name = text[0];
                    config.College = text[1];
                    config.Year = text[2];
                }
                catch { }
            }
            Deactivated += (sender, e) =>
            {
                popupWasOpen = editPopup.IsOpen;
                editPopup.IsOpen = false;
                deactivated = true;
            };

            Activated += (sender, e) =>
            {
                deactivated = false;
                if (popupWasOpen)
                {
                    if (reopenEditPopupTask != null)
                    {
                        reopenEditPopupTask.Wait();
                    }
                    else
                    {
                        reopenEditPopupTask = Task.Run(() => {
                            Task.Delay(300).Wait();
                            Dispatcher.Invoke(() => {
                                if (!deactivated)
                                { editPopup.IsOpen = true; editTextBox.Focus(); }
                            });
                            reopenEditPopupTask = null;
                        });
                    }
                }
            };
        }
        bool deactivated = false;
        Task? reopenEditPopupTask = null;
        bool popupWasOpen = false;
        
        

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            AddNewSubstance();
            
            DateTextBox.KeyDown += (sender, e) =>
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

            
        }

        private void AddNewSubstance(object? sender=null, RoutedEventArgs? e=null)
        {
            var substanceEntryControl = new SubstanceEntryControl();
            substanceEntries.Add(substanceEntryControl.substanceEntry);
            substanceEntryControl.DeleteSubstanceButton.Click += (sender, e) =>
            {
                substanceListBox.Items.Remove(substanceEntryControl);
                substanceEntries.Remove(substanceEntryControl.substanceEntry);
                if (substanceEntries.Count == 0)
                {
                    AddNewSubstance();
                }
            };
            substanceListBox.Items.Insert(substanceListBox.Items.Count - 1, substanceEntryControl);
        }
        
        private void openEditPopup(object? sender, RoutedEventArgs e)
        {
            if (editPopup.IsOpen)
            {
                editPopup.IsOpen = false;
            } else
            {

                editPopup.IsOpen = true;
                editTextBox.Focusable = true;
                editTextBox.Focus();
                editTextBox.Text = string.Join("\n", substanceEntries.Where(substanceEntry => !string.IsNullOrWhiteSpace(substanceEntry.CurrentQuery))
                    .Select(substanceEntry => $"{substanceEntry.CurrentQuery};{substanceEntry.Amount}"));
                editTextBox.Select(editTextBox.Text.Length, 0);
            }
        }
        private void onEditSaveButtonPressed(object? sender, RoutedEventArgs e)
        {
         
            var lines = editTextBox.Text.Split('\n')
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(';'))
                .ToArray();
            
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var query = line[0].Trim();
                Trace.WriteLine(substanceEntries.Count);
                if (i >= substanceEntries.Count)
                {
                    AddNewSubstance(); 
                }
                SubstanceEntryControl substanceEntryControl = (SubstanceEntryControl) substanceListBox.Items[i];
                if (String.Compare(substanceEntryControl.SearchQueryTextBox.Text.Trim(), query, comparisonType: StringComparison.OrdinalIgnoreCase) == 0) continue;
                substanceEntryControl.SearchQueryTextBox.Text = query;
                substanceEntryControl.Search(query, false, false);
                if (line.Length == 2)
                {
                    substanceEntryControl.substanceEntry.Amount = line[1].Trim();
                }
                
            }
            editPopup.IsOpen = false;

            
        }

        private void closeEditPopup(object? sender, RoutedEventArgs? e)
        {
            editPopup.IsOpen = false;
        }
        private void Clear(object? sender = null, RoutedEventArgs? e = null)
        {
            substanceEntries.Clear();
            var delete = substanceListBox.Items[substanceListBox.Items.Count-1];
            substanceListBox.Items.Clear();
            substanceListBox.Items.Add(delete);
            AddNewSubstance();
        }
        
        private async void Generate(object? sender, RoutedEventArgs? e)
        {
            generateButton.IsEnabled = false;
            for (int i = 0; i < substanceEntries.Count; i++)
            {
                if (substanceEntries[i].ExtractionTask == null && substanceEntries[i].safetyData == null)
                {
                    if (string.IsNullOrEmpty(substanceEntries[i].DisplayName))
                    {
                        substanceListBox.Items.RemoveAt(i);
                        substanceEntries.RemoveAt(i--);
                    } else
                    {
                        generateButton.IsEnabled = true;
                        MessageBox.Show($"Product is not selected for \"{substanceEntries[i].DisplayName}\" at index {i}", "Generation Error", MessageBoxButton.OK);
                        return;
                    }
                } else if (string.IsNullOrEmpty(substanceEntries[i].DisplayName))
                {
                    MessageBox.Show($"Display Name is not set at index {i}", "Generation Error", MessageBoxButton.OK);
                    generateButton.IsEnabled = true;
                    return;
                }
            }
            
            //if (!substanceEntries.Any())
            //{
            //    generateButton.IsEnabled = true;
            //    return;
            //}
            
            Trace.WriteLine("Generating");

            string outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, config.OutputName + ".docx");

            List<SubstanceEntry> substances = new List<SubstanceEntry>(substanceEntries);

            
            Task<string> generateTasks = Task.Run(() => {
                try
                {
                    return COSHHForm.Generate(config, substances, outputPath);
                }
                catch (Exception e)
                {
                    return Task.FromResult(e.Message);
                }
            });


            if (substanceEntries.Count == 0)
            {
                AddNewSubstance();
            }

            // Update cache
            using (StreamWriter sw = new StreamWriter(File.Open(cachePath, FileMode.OpenOrCreate)))
            {
                sw.WriteLine($"{NameTextBox.Text};{CollegeTextBox.Text};{YearTextBox.Text}");
            }

            if (!File.GetAttributes(cachePath).HasFlag(FileAttributes.Hidden))
            {
                File.SetAttributes(cachePath, FileAttributes.Hidden);
            }

            var generationResult = await generateTasks;
            generateButton.IsEnabled = true;
            if (generationResult != "")
            {
                MessageBox.Show(generationResult, "Generation Error", MessageBoxButton.OK);
            } 
            else if (MessageBox.Show("Open file?", "Success!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(outputPath)
                    {
                        UseShellExecute = true
                    }
                }.Start();
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

        private void onCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                bool isEnabled = checkBox.IsChecked == true;

                switch (checkBox.Name)
                {
                    case "FireExplosionCheckBox":
                        FireExplosionTextBox.IsEnabled = isEnabled;
                        break;
                    case "ThermalRunawayCheckBox":
                        ThermalRunawayTextBox.IsEnabled = isEnabled;
                        break;
                    case "GasReleaseCheckBox":
                        GasReleaseTextBox.IsEnabled = isEnabled;
                        break;
                    case "MalodorousSubstancesCheckBox":
                        MalodorousSubstancesTextBox.IsEnabled = isEnabled;
                        break;
                    case "SpecialMeasuresCheckBox":
                        SpecialMeasuresTextBox.IsEnabled = isEnabled;
                        break;
                }
            }
        }
    }




}
