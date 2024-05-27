using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        Task<string>? generateTask = null;
        string cachePath;
        List<SubstanceEntry> substanceEntries = new List<SubstanceEntry>();
        
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

        private void AddNewSubstance(object? sender=null, RoutedEventArgs? e=null)
        {
            var substanceEntryControl = new SubstanceEntryControl();
            substanceEntries.Add(substanceEntryControl.substanceEntry);
            substanceEntryControl.DeleteSubstanceButton.Click += (sender, e) =>
            {
                substanceListBox.Items.Remove(substanceEntryControl);
                substanceEntries.Remove(substanceEntryControl.substanceEntry);
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
                if (substanceEntries[i].extractionTask == null && substanceEntries[i].safetyData == null)
                {
                    if (string.IsNullOrEmpty(substanceEntries[i].DisplayName))
                    {
                        substanceListBox.Items.RemoveAt(i);
                        substanceEntries.RemoveAt(i--);
                    } 
                }
            }
            if (substanceEntries.Count == 0)
            {
                generateButton.IsEnabled = true;
                return;
            }
            for (int i = 0; i < substanceEntries.Count; i++)
            {
                if (substanceEntries[i].extractionTask == null && substanceEntries[i].safetyData == null)
                {
                    generateButton.IsEnabled = true;
                    MessageBox.Show($"Product is not selected for {substanceEntries[i].DisplayName} at index {i}", "Generation Error", MessageBoxButton.OK);
                    return;
                }
            }

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "COSHH.docx");
            
            generateTask = COSHHForm.Generate(titleTextBox.Text, nameTextBox.Text, collegeTextBox.Text, yearTextBox.Text, dateTextBox.Text,
                fireExplosionCheckBox.IsChecked, thermalRunawayCheckBox.IsChecked, gasReleaseCheckBox.IsChecked, malodorousSubstancesCheckBox.IsChecked, specialMeasuresCheckBox.IsChecked,
                fireExplosionTextBox.Text, thermalRunawayTextBox.Text, gasReleaseTextBox.Text, malodorousSubstancesTextBox.Text, specialMeasuresTextBox.Text,
                halogenatedCheckBox.IsChecked, hydrocarbonCheckBox.IsChecked, contaminatedCheckBox.IsChecked, aqueousCheckBox.IsChecked, namedWasteCheckBox.IsChecked, silicaTLCCheckBox.IsChecked,
                substanceEntries, path);
            
            using (StreamWriter sw = new StreamWriter(File.Open(cachePath, FileMode.OpenOrCreate)))
            {
                sw.WriteLine($"{nameTextBox.Text};{collegeTextBox.Text};{yearTextBox.Text}");
            }
            if (!File.GetAttributes(cachePath).HasFlag(FileAttributes.Hidden))
            {
                File.SetAttributes(cachePath, FileAttributes.Hidden);
            }

            string generationResult = await generateTask;
            if (generationResult != "")
            {
                MessageBox.Show(generationResult, "Generation Error", MessageBoxButton.OK);
            }
            generateButton.IsEnabled = true;
            generateTask = null;
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
                    case "fireExplosionCheckBox":
                        fireExplosionTextBox.IsEnabled = isEnabled;
                        break;
                    case "thermalRunawayCheckBox":
                        thermalRunawayTextBox.IsEnabled = isEnabled;
                        break;
                    case "gasReleaseCheckBox":
                        gasReleaseTextBox.IsEnabled = isEnabled;
                        break;
                    case "malodorousSubstancesCheckBox":
                        malodorousSubstancesTextBox.IsEnabled = isEnabled;
                        break;
                    case "specialMeasuresCheckBox":
                        specialMeasuresTextBox.IsEnabled = isEnabled;
                        break;
                }
            }
        }
    }




}
