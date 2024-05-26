using System;
using System.Collections.Generic;
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
        Task? generateTask = null;
        string cachePath;
        List<SubstanceEntry> substanceEntries = new List<SubstanceEntry>();
        
        public MainWindow()
        {
            Task.Run(() => SigmaAldrich.SearchAsync("HCl"));
            InitializeComponent();
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
