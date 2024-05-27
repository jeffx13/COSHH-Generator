using DocumentFormat.OpenXml.Office2019.Excel.ThreadedComments;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace COSHH_Generator.Core
{
    class ChemicalPool
    {
        public ChemicalPool()
        {
            Trace.WriteLine("Initialised pool");
            _Substances = new ObservableCollection<SubstanceEntry>(_Substances.OrderBy(s => s.DisplayName));

        }

        private ObservableCollection<SubstanceEntry> _Substances = new ObservableCollection<SubstanceEntry>()
        {
            new SubstanceEntry()
            {
                DisplayName = "Ba(NO\x2083)\x2082",
                Amount = "1 g",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302+H332", "H319" },
                    new List<string> { "P210", "P220+P221", "P261", "P305+P351+P338", "P370+P378" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Inhalation = true,
                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Na\x2082SO\x2084",
                Amount = "1 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "BaSO\x2084",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaNO\x2083",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H319" },
                    new List<string> { "P220", "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium",
                Amount = "0.04 g",
                safetyData = new SafetyData(
                    new List<string> { "H250", "H260" },
                    new List<string> { "P222", "P223", "P231+P232", "P370+P378" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 6M",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "MgCl\x2082",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hydrogen",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H220" },
                    new List<string> { "P210", "P377", "P381", "P410+P403" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 1M",
                Amount = "150 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 1M",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Chloride",
                Amount = "100 g",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H319" },
                    new List<string> { "P301+P312+P330", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Eugenol",
                Amount = "(Extracted)",
                safetyData = new SafetyData(
                    new List<string> { "H317", "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Dichloromethane",
                Amount = "45 mL",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H336", "H351" },
                    new List<string> { "P201", "P261", "P264+P280", "P304+P340+P312", "P308+P313" })
                {
                    Eyes = true,
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulphate (Anhydrous)",
                Amount = "(For drying)",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Deuterated Chloroform",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H331", "H351", "H361", "H372" },
                    new List<string> { "P201", "P260", "P264+P280", "P304+P340+P312", "P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Petroleum Ether 60/80",
                Amount = "(TLC Solvent)",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H411" },
                    new List<string> { "P210", "P240", "P273", "P301+P330+P331", "P302+P352", "P403+P233" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "(TLC Solvent)",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "(TLC Solvent)",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetate",
                Amount = "124 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P233", "P261", "P280", "P303+P361+P353", "P370+P378" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 2M",
                Amount = "30 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 2M",
                Amount = "35 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate (Anhydrous)",
                Amount = "(For drying)",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Trans-anethole",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H317" },
                    new List<string> { "P280" })
                {
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Para-anisaldehyde",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzoic Acid",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H318", "H372" },
                    new List<string> { "P260", "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Fluorenone",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H319", "H411" },
                    new List<string> { "P273", "P264+P280", "P337+P313", "P391", "P501" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Para-toluidine",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H301+H311+H331", "H317", "H317", "H319", "H334", "H351", "H410" },
                    new List<string> { "P261", "P280", "P284", "P301+P310+P330", "P304+P340+P312", "P342+P311", "P403+P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Thymol",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H314", "H411" },
                    new List<string> { "P260", "P280", "P301+P312+P330", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methyl benzoate",
                Amount = "(TLC Sample)",
                safetyData = new SafetyData(
                    new List<string> { "H302" },
                    new List<string> { "P301+P312+P330",  })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "(TLC Solvent)",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetic Acid",
                Amount = "(TLC Solvent)",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "FeCl\x2083",
                Amount = "(Dip)",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H302", "H315", "H318" },
                    new List<string> { "P280", "P301+P312+P330", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methanol",
                Amount = "(Dip Solvent)",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H301+H311+H331", "H370" },
                    new List<string> { "P210", "P280", "P302+P352+P312", "P304+P340+P312", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Vanillin",
                Amount = "(Dip)",
                safetyData = new SafetyData(
                    new List<string> { "H319" },
                    new List<string> { "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Fluorene",
                Amount = "140 mg",
                safetyData = new SafetyData(
                    new List<string> { "H410" },
                    new List<string> { "P273", "P501" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Fluorenone",
                Amount = "Few mg",
                safetyData = new SafetyData(
                    new List<string> { "H319", "H411" },
                    new List<string> { "P273", "P264+P280", "P337+P313", "P391", "P501" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Dichloromethane",
                Amount = "3 mL",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H336", "H351" },
                    new List<string> { "P201", "P261", "P264+P280", "P304+P340+P312", "P308+P313" })
                {
                    Eyes = true,
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Petroleum Ether 40-60",
                Amount = "55 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H411" },
                    new List<string> { "P210", "P240", "P273", "P301+P330+P331", "P302", "P352", "P403+P233" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 10M",
                Amount = "10 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "15 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Stark’s Catalyst",
                Amount = "3 Drops",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H314", "H310" },
                    new List<string> { "P260", "P280", "P301+P312+P330", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "5% w/v HCl",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Saturated NaCl",
                Amount = "15 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulphate",
                Amount = "Few spatulas",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silica Gel",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Amberlite IR120(H) Resin",
                Amount = "30 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 2M",
                Amount = "15 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Anti-bumping Granules",
                Amount = "2",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NH\x2084VO\x2083",
                Amount = "100 mg",
                safetyData = new SafetyData(
                    new List<string> { "H301", "H319", "H332", "H335", "H372", "H411" },
                    new List<string> { "P260", "P305+P351+P338", "P301+P310" })
                {
                    Eyes = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 0.15M",
                Amount = "160 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Phosphoric Acid 0.1M",
                Amount = "70 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methyl Orange Indicator",
                Amount = "Drops",
                safetyData = new SafetyData(
                    new List<string> { "H301" },
                    new List<string> { "P308+P310" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Phenolphthalein Indicator",
                Amount = "Drops",
                safetyData = new SafetyData(
                    new List<string> { "H341+H350" },
                    new List<string> { "P201", "P280", "P308+P313" })
                {
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Deionised Water",
                Amount = "500 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethylenediaminetetraacetic Acid Disodium Salt Dihydrate",
                Amount = "0.95 g",
                safetyData = new SafetyData(
                    new List<string> { "H332", "H373" },
                    new List<string> {  })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "EDTA 0.01M",
                Amount = "250 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Murexide Indicator",
                Amount = "Drops",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 1M",
                Amount = "Drops",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "50% w/v NaOH",
                Amount = "30 Drops",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Mg(OH)\x2082",
                Amount = "Ppt",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hydroxynaphthol Blue Indicator",
                Amount = "Drops",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Saturated Ammonium Oxalate Solution",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302+H312" },
                    new List<string> { "P301+P312+P330", "P302+P352+P312" })
                {
                    Skin = true,
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Calcium Oxalate",
                Amount = "Ppt",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonia/Ammonium Chloride Buffer",
                Amount = "10 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H314", "H335", "H412" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Eriochrome Black T Indicator",
                Amount = "3 Drops",
                safetyData = new SafetyData(
                    new List<string> { "H319", "H411" },
                    new List<string> { "P273", "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lead(II) Nitrate 0.5M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302+H332", "H318", "H360", "H373", "H410" },
                    new List<string> { "P201", "P210", "P220", "P280", "P305+P351+P338+P310", "P308+P313" })
                {
                    Ingestion = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Iron(II) Sulphate Hexahydrate 0.1M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "H\x2082SO\x2084 2M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Barium Chloride Dihydrate 0.2M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H301", "H332" },
                    new List<string> { "P308+P310" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silver(I) Nitrate 0.1M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H290", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P273", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338", "P370+P378", "P391" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Carbonate 0.5M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 0.5M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaI 0.5M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H319", "H400" },
                    new List<string> { "P273", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 0.5M",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3% (v/v) H₂O₂",
                Amount = "8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H318", "H412" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lead(II) Sulphate",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H302+H332", "H360", "H373", "H410" },
                    new List<string> { "P201", "P308+P313" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lead(II) Iodide",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H302+H332", "H360", "H373", "H410" },
                    new List<string> { "P201" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Barium Sulphate",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonia",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H221", "H314", "H331", "H410" },
                    new List<string> { "P210", "P280", "P304+P340+P310", "P305+P351+P338", "P377", "P403" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iron(II) Hydroxide",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silver(I) Chloride",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H410" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silver(I) Iodide",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H410" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "CO\x2082",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iodine",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaCl",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Na Metal",
                Amount = "1 g",
                safetyData = new SafetyData(
                    new List<string> { "H260", "H314" },
                    new List<string> { "P223", "P231+P232", "P280", "P305+P351+P338", "P370+P378" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetate",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P233", "P261", "P280", "P303+P361+P353", "P370+P378" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl 3-oxobutanoate",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "2 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 2M",
                Amount = "Acidify to pH4",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Brine",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate Anhydrous",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzyl Alcohol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302+H332", "H319" },
                    new List<string> { "P271", "P305+P351+P338" })
                {
                    Eyes = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1,3 Dibromopropane",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302", "H319", "H411" },
                    new List<string> { "P210", "P301+P312+P330", "P305+P351+P338", "P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diphenylmethane",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H410" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Adipate",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzaldehyde",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302+H312", "H315" },
                    new List<string> { "P264+P270+P280", "P301+P312+P330", "P302+P352+P312", "P501" })
                {
                    Ingestion = true,
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethylene Glycol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H373" },
                    new List<string> { "P260", "P301+P312+P330" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Pentamethyl Benzene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H228" },
                    new List<string> { "P210", "P261" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. H\x2082SO\x2084",
                Amount = "10 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-Benzylpyridine",
                Amount = "1.6 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HNO\x2083",
                Amount = "2 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H315+H319" },
                    new List<string> { "P234" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH Pellets",
                Amount = "20 g",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "130 mL",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate (Anhydrous)",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-(2,4-dinitrobenzyl)pyridine",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Chloride",
                Amount = "5 g",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H319" },
                    new List<string> { "P301+P312+P330", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. Ammonia",
                Amount = "30 cm\x00B3",
                safetyData = new SafetyData(
                    new List<string> { "H221", "H314", "H331", "H410" },
                    new List<string> { "P210", "P280", "P304+P340+P310", "P305+P351+P338", "P377", "P403" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cobalt(II) Chloride Hexahydrate",
                Amount = "10 g",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H317", "H334", "H341+H350+H360", "H410" },
                    new List<string> { "P201", "P273", "P280", "P302+P352", "P304+P340", "P342+P311" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "H\x2082O\x2082 (Aqueous, 30%)",
                Amount = "8 cm³",
                safetyData = new SafetyData(
                    new List<string> { "H318", "H412" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "30 cm³",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Pentaaminechlorocobalt(III) Dichloride",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetonitrile",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302+H312+H332", "H319" },
                    new List<string> { "P210", "P261", "P280", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Phenyl acetylene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H304", "H315", "H319" },
                    new List<string> { "P301+P310+P331", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diphenyl acetylene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hex-1-yne",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H319", "H335" },
                    new List<string> { "P210", "P261", "P301+P330+P331", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4-nitro toluene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H301+H311+H331", "H373", "H411" },
                    new List<string> { "P261", "P273", "P280", "P301+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Triethylamine",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302", "H311+H331", "H314", "H335" },
                    new List<string> { "P210", "P261", "P280", "P303+P361+P353", "P305+P351+P338", "P370+P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-phenylethylamine",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H301", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "N-(2-methylprop-2-yl) benzamide",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-aminopropanoic acid",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzamide",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H341" },
                    new List<string> { "P281" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzaldehyde",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302+H312", "H315" },
                    new List<string> { "P264+P270+P280", "P301+P312+P330", "P302+P352+P312", "P501" })
                {
                    Ingestion = true,
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Phenylmethanol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302+H332", "H319" },
                    new List<string> { "P271", "P305+P351+P338" })
                {
                    Eyes = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzoic acid",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H318", "H372" },
                    new List<string> { "P260", "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium benzoate",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H319" },
                    new List<string> { "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methyl benzoate",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302" },
                    new List<string> { "P301+P312+P330" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-methyltoluene / o-xylene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H304", "H312+H332", "H315", "H319", "H335", "H412" },
                    new List<string> { "P210", "P261", "P273", "P301+P310", "P302+P352+P312+P331" })
                {
                    Ingestion = true,
                    Inhalation = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-methyltoluene / m-xylene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H304", "H312+H332", "H315", "H319", "H335", "H412" },
                    new List<string> { "P261", "P273", "P280", "P301+P310", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Inhalation = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4-methyltoluene / p-xylene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H312+H332", "H315" },
                    new List<string> { "P280" })
                {
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-methylbenzaldehyde / o-tolualdehyde",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315", "H318", "H335" },
                    new List<string> { "P280", "P301+P312+P330", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1,4-epoxybutane / tetrahydrofuran",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302", "H319", "H335", "H351" },
                    new List<string> { "P210", "P280", "P301+P312+P330", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Pentan-2-one",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302", "H319" },
                    new List<string> { "P210", "P301+P312+P330", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-methylbutan-2-one",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225" },
                    new List<string> { "P210", "P403+P235" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Pentan-3-one",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H335", "H336" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-methyl-3-buten-1-ol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4-penten-2-ol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-methyl-2-buten-1-ol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302", "H315+H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-methylbutanal",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H317", "H319", "H335", "H411" },
                    new List<string> { "P210", "P261", "P273", "P280", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cyclobutylmethanol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Pentanal",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H317", "H319", "H332", "H335" },
                    new List<string> { "P210", "P240", "P280", "P302+P352", "P305+P351+P338", "P403+P233" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cyclopentanol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H226" },
                    new List<string> { "P210", "P370+P378" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1-methyl-1,4-epoxybutane / 2-methyltetrahydrofuran",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302", "H315", "H318" },
                    new List<string> { "P210", "P280", "P301+P312+P330", "P305+P351+P338+P310", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Tetramethylsilane",
                Amount = "Standard",
                safetyData = new SafetyData(
                    new List<string> { "H224" },
                    new List<string> { "P210" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Salicylate 0.05M",
                Amount = "100 mL",//"(100 mL of 0.05M) mass",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H319" },
                    new List<string> { "P301+P312+P330", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ferric Nitrate 10mM",
                Amount = "70 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Nitric Acid 60mM",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H315+H319" },
                    new List<string> { "P234", "P264+P280", "P332+P313", "P337+P313", "P390" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Reichard’s Dye 0.005M",
                Amount = "2.8 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Brooker’s Dye 0.005M",
                Amount = "1.6 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetonitrile",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302+H312+H332", "H319" },
                    new List<string> { "P210", "P261", "P280", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "90 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetate",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P233", "P261", "P280", "P303+P361+P353", "P370+P378" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Chloroform",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H331", "H336", "H351", "H361", "H372" },
                    new List<string> { "P201", "P260", "P264+P280", "P304+P340+P312", "P403+P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225" },
                    new List<string> { "P210", "P351+P338", "P370+P378", "P403+P235", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methanol",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H301+H311+H331", "H370" },
                    new List<string> { "P210", "P280", "P302+P352+P312", "P304+P340+P312", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-propanol",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "LiClO\x2084 0.1M",
                Amount = "50uL",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H315", "H319", "H335" },
                    new List<string> { "P220", "P261", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaI 0.1M",
                Amount = "50uL",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H319", "H400" },
                    new List<string> { "P273", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KHSO\x2084",
                Amount = "17.5 g",
                safetyData = new SafetyData(
                    new List<string> { "H314", "H335" },
                    new List<string> { "P261", "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "K\x2082S\x2082O\x2088",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H315+H319", "H317", "H334", "H335" },
                    new List<string> { "P220", "P261", "P280", "P305+P351+P338", "P342+P311" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl ether",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iron(II) ammonium sulphate",
                Amount = "10 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sulphuric acid 2M",
                Amount = "335 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KMnO\x2084 0.02M",
                Amount = "Titrate",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338+P310", "P370+P378", "P391" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ferroin indicator",
                Amount = "Indicator",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Orthophosphoric acid",
                Amount = "5 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Vanadyl sulphate",
                Amount = "25 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302" },
                    new List<string> {  })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "Wash",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium iron(III) sulphate",
                Amount = "3 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(NH\x2084)V(SO\x2084)\x2082",
                Amount = "0.6 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Chlorine Water",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H270" },
                    new List<string> { "P220", "P261", "P304+P340+P312", "P403+P233", "P410+P403" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Fe²⁺ compound (chloride data)",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H314" },
                    new List<string> { "P280" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Fe³⁺ compound (chloride data)",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H302", "H315", "H318" },
                    new List<string> { "P280", "P301+P312+P330", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KI 0.1M",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H372" },
                    new List<string> { "P260", "P264+P270", "P314" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iodine water 0.1M",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Mn²⁺ solution 0.1M (chloride data)",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H318", "H373", "H411" },
                    new List<string> { "P273", "P280", "P301+P312+P330" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "AgNO\x2083 0.1M",
                Amount = "Test tube",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H290", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P273", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338", "P370+P378", "P391" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Borohydride",
                Amount = "25 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H260", "H301", "H314", "H360" },
                    new List<string> { "P201", "P231+P232", "P280", "P308+P313", "P370+P378", "P402+P404" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Sulphate",
                Amount = "25 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "THF",
                Amount = "Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H302", "H319", "H335" },
                    new List<string> { "P210", "P280", "P301+P312+P330", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonia Borane",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulphate",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hydrogen Gas",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H220" },
                    new List<string> { "P210", "P377", "P381", "P410+P403" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "RuCl\x2083",
                Amount = "Catalyst",
                safetyData = new SafetyData(
                    new List<string> { "H314" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NH\x2084BO\x2082",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Vegetable Oil",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Potassium Hydroxide",
                Amount = "0.2 g",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H302", "H314" },
                    new List<string> { "P260", "P280", "P301+P312+P330", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sulphuric Acid",
                Amount = "5-6 drops",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235", "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Petroleum Ether 60/80",
                Amount = "25 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H411" },
                    new List<string> { "P210", "P240", "P273", "P301+P330+P331", "P302+P352", "P403+P233" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Saturated Brine",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetate",
                Amount = "TLC Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P233", "P261", "P280", "P303+P361+P353", "P370+P378" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iodine",
                Amount = "Spatula",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Benzoic Acid",
                Amount = "1 g",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H318", "H372" },
                    new List<string> { "P260", "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetoacetate",
                Amount = "12.7 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethane-1,2-diol",
                Amount = "5.8 mL",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H373" },
                    new List<string> { "P260", "P301+P312+P330" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "p-Toluenesulphonic acid",
                Amount = "0.05 g",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Hydroxide 2M",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Potassium Carbonate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H335" },
                    new List<string> { "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "115 mL",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium",
                Amount = "1.34 g",
                safetyData = new SafetyData(
                    new List<string> { "H250", "H260" },
                    new List<string> { "P222", "P223", "P231+P232", "P370+P378" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Calcium Chloride",
                Amount = "Drying Tube",
                safetyData = new SafetyData(
                    new List<string> { "H319" },
                    new List<string> { "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iodine",
                Amount = "Few Crystals",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Bromobenzene",
                Amount = "5.25 mL",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H315", "H411" },
                    new List<string> { "P273" })
                {
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Petroleum Ether 60-80",
                Amount = "3 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H411" },
                    new List<string> { "P210", "P240", "P273", "P301+P330+P331", "P302+P352", "P403+P233" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "25 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "1 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304", "P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Saturated Sodium Bicarbonate",
                Amount = "15 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1,1-diphenyl-1-hydroxy-butan-3-one",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cyclohexanone",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302+H312+H332", "H315", "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cycloheptanone",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302", "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-Cyclopenten-1-one",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H226" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetic Acid",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetamide",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H351" },
                    new List<string> { "P281" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Maleic Anhydride",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H314", "H317", "H334", "H372" },
                    new List<string> { "P260", "P280", "P284", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetic Anhydride",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302", "H314", "H330" },
                    new List<string> { "P210", "P260", "P280", "P304+P340+P310", "P305+P351+P338", "P370+P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acrylic Acid",
                Amount = "IR Spectrum",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302+H312+H332", "H314", "H335", "H400" },
                    new List<string> { "P210", "P260", "P280", "P304+P340+P310", "P305+P351+P338", "P370+P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HNO\x2083 3M",
                Amount = "Wash",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H315+H319" },
                    new List<string> { "P234", "P264+P280", "P332+P313", "P337+P313", "P390" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Zn(NO\x2083)\x2082 1M",
                Amount = "250 mL",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H315+H319", "H335", "H410" },
                    new List<string> { "P210", "P220", "P261", "P273", "P370+P378", "P391" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cu(NO\x2083)\x2082 1M",
                Amount = "250 mL",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H315", "H318", "H410" },
                    new List<string> { "P210", "P220+P221", "P280", "P305+P351+P338+P310", "P370+P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Chloroplatinic Acid",
                Amount = "Platinising Solution",
                safetyData = new SafetyData(
                    new List<string> { "H301", "H314", "H317", "H334" },
                    new List<string> { "P261", "P280", "P301+P310", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lead(II) Acetate",
                Amount = "Platinising Solution",
                safetyData = new SafetyData(
                    new List<string> { "H360", "H373", "H410" },
                    new List<string> { "P201", "P260", "P280", "P308+P313" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 1.18M",
                Amount = "Electrode Immersion",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304", "P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KCl",
                Amount = "Spatula",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sulphuric Acid 0.1M",
                Amount = "Electrode Immersion",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "AgCl",
                Amount = "Layer",
                safetyData = new SafetyData(
                    new List<string> { "H410" },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hydrogen Gas",
                Amount = "Electrode",
                safetyData = new SafetyData(
                    new List<string> { "H220" },
                    new List<string> { "P210", "P377", "P381", "P410+P403" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-nitrophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-nitrophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315", "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4-nitrophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H301", "H312+H332", "H373" },
                    new List<string> { "P261", "P301+P310+P330", "P302+P352+P312", "P304+P340+P312" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2-cyanophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H317", "H318" },
                    new List<string> { "P280", "P301+P312+P330", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3- cyanophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315", "H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4- cyanophenol",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315", "H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "4-hydroxybenzamide",
                Amount = "60 mg",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 0.1M",
                Amount = "100 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 0.1M",
                Amount = "100 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-nitroacetophenone",
                Amount = "10 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Tin Metal (Sn)",
                Amount = "28 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H319", "H335" },
                    new List<string> { "P305+P351+P338" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "9 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304", "P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 10M",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "Wash",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305+P351+P338", "P370+P378", "P403+P235" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Deuterated Chloroform (CDCl\x2083)",
                Amount = "NMR",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H331", "H351", "H361", "H372" },
                    new List<string> { "P201", "P260", "P264+P280", "P304+P340+P312", "P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Borohydride",
                Amount = "10 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H260", "H301", "H314", "H360" },
                    new List<string> { "P201", "P231+P232", "P280", "P308+P313", "P370+P378", "P402+P404" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "Recrystallisation",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "3-aminoacetophenone",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H319" },
                    new List<string> { "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1-(3-nitrophenyl) ethan-1-ol",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Iodine",
                Amount = "4 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KMnO\x2084",
                Amount = "5 g",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338", "P370+P378", "P391" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 1M",
                Amount = "200 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "KCl",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "MnCl\x2082",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H318", "H373", "H411" },
                    new List<string> { "P273", "P280", "P301+P312+P330", "P305+P351+P338+P310", "P391", "P501" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cl\x2082",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H270", "H315+H319", "H331", "H335", "H400" },
                    new List<string> { "P220", "P261", "P304+P340+P312", "P403+P233", "P410+P403" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "ICl",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H314", "H334" },
                    new List<string> { "P261", "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "ICl\x2083",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hex-1-ene",
                Amount = "4.0 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304" },
                    new List<string> { "P210", "P301+P310+P331" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Styrene",
                Amount = "4.4 mL",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H315", "H319", "H332", "H361", "H372" },
                    new List<string> { "P210", "P260", "P280", "P305+P351+P338", "P370+P378" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "DCM",
                Amount = "15 mL",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H336", "H351" },
                    new List<string> { "P201", "P261", "P264+P280", "P304+P340+P312", "P308+P313" })
                {
                    Eyes = true,
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulphite",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Deuterated Chloroform",
                Amount = "NMR",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H331", "H351", "H361", "H372" },
                    new List<string> { "P201", "P260", "P264+P280", "P304+P340+P312", "P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Caesium Chloride",
                Amount = "3 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cs[ICl\x2082]",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cs[ICl\x2084]",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Caesium Iodide",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H317", "H319", "H335", "H410" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Tetraphenylborate",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> { "H301" },
                    new List<string> { "P301+P310" })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Caesium Tetraphenylborate",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H319", "H335" },
                    new List<string> { "P261", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaI",
                Amount = "0.1 g",
                safetyData = new SafetyData(
                    new List<string> { "H315", "H319", "H400" },
                    new List<string> { "P273", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Thiosulphate 0.1M",
                Amount = "10 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lithium Iodide",
                Amount = "0.5 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silver Nitrate",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H290", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P273", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338", "P370+P378", "P391" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonia",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H221", "H314", "H331", "H410" },
                    new List<string> { "P210", "P280", "P304+P340+P310", "P305+P351+P338", "P377", "P403" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cobalt(II) Chloride, Hexahydrate (Solid)",
                Amount = "6 g",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H317", "H334", "H341+H350+H360", "H410" },
                    new List<string> { "P201", "P273", "P280", "P302+P352", "P304+P340", "P342+P311" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethylenediamine (Solid)",
                Amount = "13.3 g",
                safetyData = new SafetyData(
                    new List<string> { "H226", "H302+H332", "H311", "H314", "H317", "H334", "H412" },
                    new List<string> { "P261", "P273", "P280", "P305+P351+P338+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH (Solid)",
                Amount = "8.5 g",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hydrogen Peroxide (3% w/v)",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H318", "H412" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethanol",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319" },
                    new List<string> { "P210", "P305" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "[Co(en)₃]Cl₃.2H₂O",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "L-Tartaric Acid (Solid)",
                Amount = "0.87 g",
                safetyData = new SafetyData(
                    new List<string> { "H318" },
                    new List<string> { "P280", "P305+P351+P338" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "D- Tartaric Acid (Solid)",
                Amount = "0.87 g",
                safetyData = new SafetyData(
                    new List<string> { "H318" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone (50% v/v) Aqueous",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Acetone",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P280", "P304+P340+P312", "P305+P351+P338", "P337+P313", "P403+P235" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1,2-diaminocyclohexane",
                Amount = "5.3 mL",
                safetyData = new SafetyData(
                    new List<string> { "H314" },
                    new List<string> { "P280", "P305+P351+P338+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "D\x2082O",
                Amount = "NMR Solvent",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Hex-1-ene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304" },
                    new List<string> { "P210", "P301+P310+P331" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(2E,4E)-hexa-2,4-dien-1-ol",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H311", "H315", "H318" },
                    new List<string> { "P280", "P305+P351+P338", "P312" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "2,6-dimethyl-2,4,6-octatriene",
                Amount = "Sample",
                safetyData = new SafetyData(
                    new List<string> { "H302" },
                    new List<string> {  })
                {
                    Ingestion = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cyclohexane",
                Amount = "Few mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H410" },
                    new List<string> { "P210", "P261", "P273", "P301+P310+P331", "P501" })
                {
                    Ingestion = true,
                    Inhalation = true,
                    Skin = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methanol",
                Amount = "25 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H301+H311+H331", "H370" },
                    new List<string> { "P210", "P280", "P302+P352+P312", "P304+P340+P312", "P370+P378" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Dichloromethane",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H336", "H351" },
                    new List<string> { "P201", "P261", "P264+P280", "P304+P340+P312", "P308+P313" })
                {
                    Eyes = true,
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate (Anhydrous)",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silica Gel",
                Amount = "40 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Petroleum Ether 60-80",
                Amount = "Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H411" },
                    new List<string> { "P210", "P240", "P273", "P301+P330+P331", "P302+P352", "P403+P233" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Toluene",
                Amount = "Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H304", "H315", "H336", "H361", "H373" },
                    new List<string> { "P260", "P280", "P301+P310", "P370+P378", "P403+P235" })
                {
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Lycopene",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Chloride",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H319" },
                    new List<string> { "P301+P312+P330", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Disodium EDTA",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H332", "H373" },
                    new List<string> { "P260" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Potassium Iodide",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H372" },
                    new List<string> { "P260", "P264+P270", "P314", "P501" })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Potassium Thiocyanate",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H302+H312+H332", "H412" },
                    new List<string> { "P273", "P280" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulfite",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "SO\x2082",
                Amount = "Acidified Sodium Sulfite",
                safetyData = new SafetyData(
                    new List<string> { "H280", "H314", "H331" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338+P310", "P403+P233" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Zinc",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. Ammonia",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H221", "H314", "H331", "H410" },
                    new List<string> { "P210", "P280", "P304+P340+P310", "P305+P351+P338", "P377", "P403" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonia 1M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H221", "H314", "H331", "H410" },
                    new List<string> { "P210", "P280", "P304+P340+P310", "P305+P351+P338", "P377", "P403" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ammonium Polysulfide 10% w/v",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Barium Chloride 1M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H301", "H332" },
                    new List<string> { "P308+P310" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 1M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310","P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Silver Nitrate 0.05M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H290", "H314", "H410" },
                    new List<string> { "P210", "P220", "P260", "P273", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338", "P370+P378", "P391" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. Nitric Acid",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H315+H319" },
                    new List<string> { "P234", "P264+P280", "P332+P313", "P337+P313", "P390" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 1M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Hypochlorite",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H314", "H400" },
                    new List<string> { "P273", "P280", "P301+P330+P331", "P305+P351+P338", "P308+P310" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Chlorine Gas",
                Amount = "Acidified Sodium Hypochlorite",
                safetyData = new SafetyData(
                    new List<string> { "H270", "H315+H319", "H331", "H335", "H400" },
                    new List<string> { "P220", "P261", "P304+P340+P312", "P403+P233", "P410+P403" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Starch",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Perborate 1M",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H318", "H331", "H335", "H360" },
                    new List<string> { "P201", "P220", "P261", "P280", "P305+P351+P338+P311" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Unknowns",
                Amount = "Test Tube",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Products",
                Amount = "Test Tubes",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Chloride",
                Amount = "Ice Bath",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Brine",
                Amount = "Ice Bath",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(S)-alanine",
                Amount = "45 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(S)-leucine",
                Amount = "45 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(S)-phenylalanine",
                Amount = "45 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(S)-phenylglycine",
                Amount = "45 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Nitrite",
                Amount = "67.5 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H301", "H319", "H400" },
                    new List<string> { "P220", "P273", "P301+P310", "P305+P351+P338" })
                {
                    Ingestion = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Bicarbonate",
                Amount = "To Be Measured",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "To Be Measured",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Ethyl Acetate",
                Amount = "Washing",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H319", "H336" },
                    new List<string> { "P210", "P233", "P261", "P280", "P303+P361+P353", "P370+P378" })
                {
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "NaOH 1.5M",
                Amount = "Optical Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Methanol",
                Amount = "Optical Solvent",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H301+H311+H331", "H370" },
                    new List<string> { "P210", "P280", "P302+P352+P312", "P304+P340+P312", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "(S)-1-phenylethylamine",
                Amount = "45 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H311", "H314" },
                    new List<string> { "P280", "P301+P330+P331", "P302+P352", "P305+P351+P338", "P308+P310" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sulphuric Acid 0.5M",
                Amount = "34.5 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Dichloromethane",
                Amount = "Washing",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H336", "H351" },
                    new List<string> { "P201", "P261", "P264+P280", "P304+P340+P312", "P308+P313" })
                {
                    Eyes = true,
                    Skin = true,
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "1-phenylethanol",
                Amount = "Product",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sulphuric Acid 1.25M",
                Amount = "31.3 mmol",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Bromide",
                Amount = "145.5 mmol",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "Washing",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Conc. HCl",
                Amount = "26 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Bromide",
                Amount = "3.26 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Cinnamic Acid",
                Amount = "2.0 g",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Sodium Perborate",
                Amount = "2.29 g",
                safetyData = new SafetyData(
                    new List<string> { "H272", "H302", "H318", "H331", "H335", "H360" },
                    new List<string> { "P201", "P220", "P261", "P280", "P305+P351+P338+P311" })
                {
                    Ingestion = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Glacial Acetic Acid",
                Amount = "25 mL",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "HCl 2M",
                Amount = "50 mL",
                safetyData = new SafetyData(
                    new List<string> { "H290", "H314", "H335" },
                    new List<string> { "P260", "P280", "P303+P361+P353", "P304+P340+P310", "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Diethyl Ether",
                Amount = "Washing",
                safetyData = new SafetyData(
                    new List<string> { "H224", "H302", "H336" },
                    new List<string> { "P210", "P261" })
                {
                    Ingestion = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Magnesium Sulphate",
                Amount = "Drying",
                safetyData = new SafetyData(
                    new List<string> {  },
                    new List<string> {  })
                {

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Chloroform",
                Amount = "Washing",
                safetyData = new SafetyData(
                    new List<string> { "H302", "H315+H319", "H331", "H336", "H351", "H361", "H372" },
                    new List<string> { "P201", "P260", "P264+P280", "P304+P340+P312", "P403+P233" })
                {
                    Ingestion = true,
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Butanone",
                Amount = "20 mL",
                safetyData = new SafetyData(
                    new List<string> { "H225", "H301+H311+H331", "H370" },
                    new List<string> { "P210", "P280", "P302+P352+P312", "P304+P340+P312", "P370+P378", "P403+P235" })
                {
                    Ingestion = true,
                    Skin = true,
                    Inhalation = true,

                }
            },
            new SubstanceEntry()
            {
                DisplayName = "Potassium Carbonate",
                Amount = "TBC",
                safetyData = new SafetyData(
                    new List<string> { "H315+H319", "H335" },
                    new List<string> { "P305+P351+P338" })
                {
                    Skin = true,
                    Eyes = true,
                    Inhalation = true,

                }
            },


        };
        public ObservableCollection<SubstanceEntry> Substances { get => _Substances; }
    }
}
