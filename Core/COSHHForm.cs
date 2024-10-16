using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;

namespace COSHH_Generator.Core
{
    public static class COSHHForm
    {
        private static string templatePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!, ".template.docx");

        private static void AddSubstance(in WordprocessingDocument doc, in string substanceName, in string massVolume, SafetyData safetyData)
        {
            var SubstanceTable = doc.MainDocumentPart!.Document.Body!.Elements<Table>().ElementAt(2);
            TableRow row = SubstanceTable.Elements<TableRow>().Last();
            SubstanceTable.Descendants<TableRow>().Last().InsertAfterSelf(row.CloneNode(true));
            TableCell substanceNameCell = row.Elements<TableCell>().ElementAt(0);
            substanceNameCell.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First().Text = substanceName;

            TableCell massVolumeCell = row.Elements<TableCell>().ElementAt(1);
            massVolumeCell.Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First().Text = massVolume;
            
            Run run = new Run();
            RunProperties runProperties = new RunProperties();
            runProperties.FontSize = new FontSize() { Val = "20" };
            run.AppendChild(runProperties);
            foreach (string hazardStatement in safetyData.HazardStatements)
            {
                run.AppendChild(new Text(hazardStatement));
                run.AppendChild(new Break());
            }
            Run hazardsCell = row.Elements<TableCell>().ElementAt(2).Elements<Paragraph>().First().AppendChild(run);


            IEnumerable<Run> exposureRouteCell = row.Elements<TableCell>().ElementAt(3).Descendants<Run>();

            var eyeCheckBox = exposureRouteCell.ElementAt(2).GetFirstChild<Text>()!;
            var skinCheckBox = exposureRouteCell.ElementAt(4).GetFirstChild<Text>()!;
            var inhalationCheckBox = exposureRouteCell.ElementAt(6).GetFirstChild<Text>()!;
            var ingestionCheckBox = exposureRouteCell.ElementAt(8).GetFirstChild<Text>()!;

            eyeCheckBox.Text = safetyData.Eyes ? "☒" : "☐";
            skinCheckBox.Text = safetyData.Skin ? "☒" : "☐";
            inhalationCheckBox.Text = safetyData.Inhalation ? "☒" : "☐";
            ingestionCheckBox.Text = safetyData.Ingestion ? "☒" : "☐";

            //eyeCheckBox.Text = "☒a";
            //skinCheckBox.Text = "☒b";
            //inhalationCheckBox.Text = "☒c";
            //ingestionCheckBox.Text = "☒d";

            IEnumerable<Run> controlMeasuresCell = row.Elements<TableCell>().ElementAt(4).Descendants<Run>();

            var consultSpillageCheckBox = controlMeasuresCell.ElementAt(0).GetFirstChild<Text>()!;
            var safetySpecCheckBox = controlMeasuresCell.ElementAt(2).GetFirstChild<Text>()!;
            var labCoatCheckBox = controlMeasuresCell.ElementAt(4).GetFirstChild<Text>()!;
            var glovesCheckBox = controlMeasuresCell.ElementAt(6).GetFirstChild<Text>()!;
            var fumehoodCheckBox = controlMeasuresCell.ElementAt(9s).GetFirstChild<Text>()!;
            var noNakedFlamesCheckBox = controlMeasuresCell.ElementAt(11).GetFirstChild<Text>()!;
            var useWaterBathCheckBox = controlMeasuresCell.ElementAt(13).GetFirstChild<Text>()!;
            var pregnantCheckBox = controlMeasuresCell.ElementAt(15).GetFirstChild<Text>()!;
            var notNearWaterCheckBox = controlMeasuresCell.ElementAt(17).GetFirstChild<Text>()!;
            var dropwiseCheckBox = controlMeasuresCell.ElementAt(19).GetFirstChild<Text>()!;
            var notExposeToAirCheckBox = controlMeasuresCell.ElementAt(21).GetFirstChild<Text>()!;

            consultSpillageCheckBox.Text = safetyData.ConsultSpill ? "☒" : "☐";
            safetySpecCheckBox.Text = safetyData.Goggles ? "☒" : "☐";
            labCoatCheckBox.Text = safetyData.LabCoat ? "☒" : "☐";
            glovesCheckBox.Text = safetyData.Gloves ? "☒" : "☐";
            fumehoodCheckBox.Text = safetyData.Fumehood ? "☒" : "☐";
            noNakedFlamesCheckBox.Text = safetyData.NoNakedFlames ? "☒" : "☐";
            useWaterBathCheckBox.Text = safetyData.UseWaterBath ? "☒" : "☐";
            pregnantCheckBox.Text = safetyData.NotUseWhenPregnant ? "☒" : "☐";
            notNearWaterCheckBox.Text = safetyData.NotNearWater ? "☒" : "☐";
            dropwiseCheckBox.Text = safetyData.Dropwise ? "☒" : "☐";
            notExposeToAirCheckBox.Text = safetyData.NotExposeToAir ? "☒" : "☐";

            //consultSpillageCheckBox.Text = "☒a";
            //safetySpecCheckBox.Text = "☒b";
            //labCoatCheckBox.Text = "☒c";
            //glovesCheckBox.Text = "☒d";
            //fumehoodCheckBox.Text = "☒e";
            //noNakedFlamesCheckBox.Text = "☒f";
            //useWaterBathCheckBox.Text = "☒g";
            //pregnantCheckBox.Text = "☒h";
            //notNearWaterCheckBox.Text = "☒i";
            //dropwiseCheckBox.Text = "☒j";
            //notExposeToAirCheckBox.Text = "☒k";



            //if (safetyData.Flammable | safetyData.Explosive)
            //{
            //    fireExplosionCheckBox.Text = "☒";
            //    //List<string> preventions = new List<string>(){
            //    //"Keep flammable materials away from sources of ignition.",
            //    //"Keep away from sources of ignition / sparks",
            //    //"Keep away from sources of ignition"
            //    //"Keep away from naked flames and sources of ignition"
            //    //"Keep solvents away from sources of ignition"
            //    //"Do not heat flammable solvents directly on a hot plate. Keep electrolysis in fumehood."
            //    //}

            //    fireExplosionPreventionCell.Text = "Keep away from ignition sources.";
            //}
            //if (safetyData.ThermalRunaway)
            //{
            //    thermalRunawayCheckBox.Text = "☒";
            //    thermalRunawayPreventionCell.Text = "Dropwise addition by dropping funnel.";
            //}
            //if (safetyData.GasRelease)
            //{
            //    gasReleaseCheckBox.Text = "☒";
            //    //"Ensure ventilation (guard tube)."
            //    //"Hydrogen gas release on small scale. Ensure sufficient ventilation and use of open systems."
            //    //"Ventilation, use of open reaction flask"
            //    //"Keep in fumehood"
            //    //"Ensure test tubes are pointed away from others and into fume hood when reactions are taking place."
            //    //"Avoid contact of sodium hypochlorite solutions with acids"
            //    gasReleasePreventionCell.Text = "Keep the fumehood sash pulled down.";
            //}
            //if (safetyData.Malodorous)
            //{
            //    malodorousCheckBox.Text = "☒";
            //    //"Contained in a fume hood."
            //    //"Perform all reactions in fume hood where possible."
            //    //"Keep in fumehood"
            //    //"Use a fume hood for all experiments"
            //    malodorousPreventionCell.Text = "Perform all reactions in fume hood where possible and keep the fumehood sash pulled down.";
            //}
            //// special measures
            //// Dispose of all transition metal waste in designated waste containers.
            //// "Ensure all reactions are performed on a small (test tube) scale. Assume unknown samples to analyse are highly toxic and hazardous."
        }

        private static async Task<string> TryGenerate(Config info, List<SubstanceEntry> substanceEntries, string outputPath)
        {
            const int bufferSize = 81920;
            using (FileStream sourceStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
            using (FileStream destinationStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }


            using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
            {
                FillForm(doc, info);
                List<SubstanceEntry> substances;
                if (info.Shuffle)
                {
                    do
                    {
                        substances = ShuffleListInSections(substanceEntries, 3);
                    }
                    while (substances.SequenceEqual(substanceEntries));
                }
                else
                {
                    substances = substanceEntries;
                }
                foreach (var substanceEntry in substances)
                {
                    if (substanceEntry.safetyData == null)
                    {
                        await substanceEntry.ExtractionTask!;
                        await Task.Delay(100);
                    }
                    if (substanceEntry.safetyData == null)
                    {
                        return $"Generation failed: Failed to extract \"{substanceEntry.DisplayName}\"";
                    }
                    substanceEntry.Amount = string.IsNullOrEmpty(substanceEntry.Amount.Trim()) ? "N/A" : substanceEntry.Amount;
                    AddSubstance(doc, substanceEntry.DisplayName, substanceEntry.Amount, substanceEntry.safetyData);

                }
                var substanceRows = doc.MainDocumentPart!.Document.Body!.Elements<Table>().ElementAt(2).Descendants<TableRow>();
                if (substanceRows.Count() > 2)
                {
                    substanceRows.Last().Remove();
                }
            }

            FileAttributes attributes = File.GetAttributes(outputPath);
            attributes &= ~(FileAttributes.Hidden | FileAttributes.ReadOnly);
            File.SetAttributes(outputPath, attributes);
            return "";
        }

        public static async Task<string> Generate(Config info, List<SubstanceEntry> substanceEntries, string outputPath)
        {
            try
            {
                return await TryGenerate(info, substanceEntries, outputPath); }
            catch (Exception e) {
                return e.Message;
            };

            }

        private static void FillForm(in WordprocessingDocument doc, in Config info)
        {

            // Filling the student info
            IEnumerable<TableRow> rows = doc.MainDocumentPart!.Document.Body!.Elements<Table>().First().Elements<TableRow>();
            var titleCell = rows.ElementAt(0).Elements<TableCell>().ElementAt(1).Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            var nameCell = rows.ElementAt(1).Elements<TableCell>().ElementAt(1).Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            var dateCell = rows.ElementAt(1).Elements<TableCell>().ElementAt(3).Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            var collegeCell = rows.ElementAt(2).Elements<TableCell>().ElementAt(1).Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();
            var yearCell = rows.ElementAt(2).Elements<TableCell>().ElementAt(3).Elements<Paragraph>().First().Elements<Run>().First().Elements<Text>().First();

            titleCell.Text = info.Title;
            nameCell.Text = info.Name;
            collegeCell.Text = info.College;
            yearCell.Text = info.Year;
            dateCell.Text = info.Date;


            // Fill the specific safeties
            Table specificSafetyTable = doc.MainDocumentPart!.Document.Body!.Elements<Table>().ElementAt(3);
            var fireExplosionRow = specificSafetyTable.Elements<TableRow>().ElementAt(1);
            var fireExplosionCheckBox = fireExplosionRow.Elements<TableCell>().ElementAt(1).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var fireExplosionPreventionCell = fireExplosionRow.Elements<TableCell>().ElementAt(2).Descendants<Text>().First();

            var thermalRunawayRow = specificSafetyTable.Elements<TableRow>().ElementAt(2);
            var thermalRunawayCheckBox = thermalRunawayRow.Elements<TableCell>().ElementAt(1).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var thermalRunawayPreventionCell = thermalRunawayRow.Elements<TableCell>().ElementAt(2).Descendants<Text>().First();

            var gasReleaseRow = specificSafetyTable.Elements<TableRow>().ElementAt(3);
            var gasReleaseCheckBox = gasReleaseRow.Elements<TableCell>().ElementAt(1).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var gasReleasePreventionCell = gasReleaseRow.Elements<TableCell>().ElementAt(2).Descendants<Text>().First();

            var malodorousRow = specificSafetyTable.Elements<TableRow>().ElementAt(4);
            var malodorousCheckBox = malodorousRow.Elements<TableCell>().ElementAt(1).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var malodorousPreventionCell = malodorousRow.Elements<TableCell>().ElementAt(2).Descendants<Text>().First();

            var specialRow = specificSafetyTable.Elements<TableRow>().ElementAt(5);
            var specialCheckBox = specialRow.Elements<TableCell>().ElementAt(1).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var specialPreventionCell = specialRow.Elements<TableCell>().ElementAt(2).Descendants<Text>().First();


            if (info.FireExplosion)
            {
                fireExplosionCheckBox.Text = "☒";
                fireExplosionPreventionCell.Text = info.FireExplosionText;
                //"Keep flammable materials away from sources of ignition.",
                //"Keep away from sources of ignition / sparks",
                //"Keep away from sources of ignition"
                //"Keep away from naked flames and sources of ignition"
                //"Keep solvents away from sources of ignition"
                //"Do not heat flammable solvents directly on a hot plate. Keep electrolysis in fumehood."
            }
            if (info.ThermalRunaway)
            {
                thermalRunawayCheckBox.Text = "☒";
                thermalRunawayPreventionCell.Text = info.ThermalRunawayText;
                //"Dropwise addition by dropping funnel."
            }
            if (info.GasRelease == true)
            {
                gasReleaseCheckBox.Text = "☒";
                gasReleasePreventionCell.Text = info.GasReleaseText;
                //"Ensure ventilation (guard tube)."
                //"Hydrogen gas release on small scale. Ensure sufficient ventilation and use of open systems."
                //"Ventilation, use of open reaction flask"
                //"Keep in fumehood"
                //"Ensure test tubes are pointed away from others and into fume hood when reactions are taking place."
                //"Avoid contact of sodium hypochlorite solutions with acids"
            }
            if (info.MalodorousSubstances == true)
            {
                malodorousCheckBox.Text = "☒";
                malodorousPreventionCell.Text = info.MalodorousSubstancesText;
                //"Contained in a fume hood."
                //"Perform all reactions in fume hood where possible."
                //"Keep in fumehood"
                //"Use a fume hood for all experiments"
            }
            if (info.SpecialMeasures == true)
            {
                specialCheckBox.Text = "☒";
                specialPreventionCell.Text = info.SpecialMeasuresText;
            }

            // Filling the waste disposal
            Table wasteDisposalTable = doc.MainDocumentPart!.Document.Body!.Elements<Table>().ElementAt(4);
            var halogenatedCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(1).Descendants<Run>().ElementAt(1).GetFirstChild<Text>()!;
            var aqueousCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(1).Descendants<Run>().ElementAt(7).GetFirstChild<Text>()!;
            var hydrocarbonCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(2).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var namedCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(2).Descendants<Run>().ElementAt(4).GetFirstChild<Text>()!;
            var contaminatedCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(3).Descendants<Run>().ElementAt(0).GetFirstChild<Text>()!;
            var silicaCheckBox = wasteDisposalTable.Elements<TableRow>().ElementAt(3).Descendants<Run>().ElementAt(2).GetFirstChild<Text>()!;

            halogenatedCheckBox.Text = info.Halogenated ? "☒" : "☐";
            hydrocarbonCheckBox.Text = info.Hydrocarbon ? "☒" : "☐";
            contaminatedCheckBox.Text = info.Contaminated ? "☒" : "☐";
            aqueousCheckBox.Text = info.Aqueous ? "☒" : "☐";
            namedCheckBox.Text = info.Named ? "☒" : "☐";
            silicaCheckBox.Text = info.SilicaTLC ? "☒" : "☐";


        }

        public static List<T> ShuffleListInSections<T>(List<T> list, int sectionSize)
        {
            List<T> newList = new List<T>(list);
            int timeMsSinceMidnight = (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            Random rng = new Random(timeMsSinceMidnight);
            int totalSections = (int)Math.Ceiling(list.Count / (double)sectionSize);
            for (int section = 0; section < totalSections; section++)
            {
                int start = section * sectionSize;
                int end = Math.Min(start + sectionSize, newList.Count);
                for (int i = start; i < end - 1; i++)
                {
                    int j = rng.Next(i, end);
                    T temp = newList[i];
                    newList[i] = newList[j];
                    newList[j] = temp;
                }
            }


            return newList;
        }
    }

}
