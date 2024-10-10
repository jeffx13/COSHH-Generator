using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        }

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
            
        private void Clear(object? sender, RoutedEventArgs? e)
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
            Task<string>  generateTasks = Task.Run(() =>
                COSHHForm.Generate(config, substances, outputPath)
            );

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
