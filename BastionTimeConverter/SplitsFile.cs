using System;
using System.Collections.Generic;
using System.Xml;

namespace BastionTimeConverter
{
    class SplitsFile
    {
        //PROPERTIES

        public XmlDocument File
        { get; private set; }

	    public string GameName
        { get; private set; }

        public string CatName
        { get; private set; }

	    public List<String> Levels
        { get; private set; }

	    public Dictionary<String, String> Splits
        { get; private set; }

	    public bool IsSkyway
        { get; private set; }

        public bool IsLoad
        { get; private set; }

        public bool SoleRegret
        { get; private set; }

        public bool Tazal
        { get; private set; }

        public bool Ram
        { get; private set; }

        public string Comparison
        { get; private set; }

        //CONSTRUCTOR

        public SplitsFile(XmlDocument doc, string comparison)
        {
            this.File = doc;
            this.GameName = File.GetElementsByTagName("GameName").Item(0).InnerText;
            ValidateGameName();
            this.CatName = File.GetElementsByTagName("CategoryName").Item(0).InnerText;
            this.IsSkyway = Boolean.Parse(File.GetElementsByTagName("Skyway_Mode").Item(0).InnerText);
            this.IsLoad = Boolean.Parse(File.GetElementsByTagName("Load_Mode").Item(0).InnerText);
            this.SoleRegret = Boolean.Parse(File.GetElementsByTagName("SoleRegret").Item(0).InnerText);
            this.Tazal = Boolean.Parse(File.GetElementsByTagName("Tazal").Item(0).InnerText);
            this.Ram = Boolean.Parse(File.GetElementsByTagName("Ram").Item(0).InnerText);
            this.Comparison = comparison;
            this.Levels = BuildLevelList();
            this.Splits = SetSplits(this.Comparison);
        }

        //METHODS

        public void ValidateGameName()
        {
            if (!this.GameName.ToUpper().Equals("BASTION"))
            {
                Console.WriteLine("Error: Splits file is not for Bastion (Exit code: 1");
                Console.ReadKey();
                Environment.Exit(1);
            }
            return;
        }
        
        public bool ValidateTiming()
        {
            if (this.IsSkyway == this.IsLoad)
            {
                return false;
            }
            return true;
        }

        public string CurrentTiming() {
            if(!ValidateTiming())
            {
                return "";
            }
            if(this.IsSkyway)
            {
                return "Skyway";
            }
            return "Load";
        }
        private List<String> BuildLevelList()
        {
            this.Levels = new List<string>();
            string category = this.CatName.ToUpper();

            if (category.Contains("NO MS") || category.Contains("NMS") || category.Contains("NO MENU STORAGE"))
            {
                Console.WriteLine("Category Detected: Any% No MS");
                if (this.SoleRegret)
                {
                    this.Levels.Add("Sole Regret");
                }
                this.Levels.Add("Wharf District");
                this.Levels.Add("Workmen Ward");
                this.Levels.Add("Hanging Gardens");
                this.Levels.Add("Cinderbrick Fort");
                this.Levels.Add("Roathus Lagoon");
                this.Levels.Add("Colford Cauldron");
                this.Levels.Add("Burstone Quarry");
                this.Levels.Add("End");
            }
            else if (category.Contains("ASL") || category.Contains("ALL STORY LEVELS"))
            {
                Console.WriteLine("Category Detected: ASL");
                if(this.SoleRegret)
                {
                    this.Levels.Add("Sole Regret");
                }
                this.Levels.Add("Wharf District");
                this.Levels.Add("Workmen Ward");
                this.Levels.Add("Melting Pot");
                this.Levels.Add("Sundown Path");
                this.Levels.Add("Hanging Gardens");
                this.Levels.Add("Cinderbrick Fort");
                this.Levels.Add("Pyth Orchard");
                this.Levels.Add("Langston River");
                this.Levels.Add("Prosper Bluff");
                this.Levels.Add("Wild Outskirts");
                this.Levels.Add("Jawson Bog");
                this.Levels.Add("Roathus Lagoon");
                this.Levels.Add("Point Lemaign");
                this.Levels.Add("Colford Cauldron");
                this.Levels.Add("Mount Zand");
                this.Levels.Add("Burstone Quarry");
                this.Levels.Add("Urzendra Gate");
                this.Levels.Add("Zulten's Hollow");
                if(this.Tazal)
                {
                    this.Levels.Add("Tazal Terminals 1");
                    this.Levels.Add("Tazal Terminals 2");
                }
                else if (this.Ram)
                {
                    this.Levels.Add("Battering Ram");
                    this.Levels.Add("Tazal Terminals 2");
                }
                else
                {
                    this.Levels.Add("Tazal Terminals");
                }
                this.Levels.Add("End");
            }
            else if (category.Contains("WEAPONS") || category.Contains("AW"))
            {
                Console.WriteLine("Category Detected: All Weapons");
                if(this.SoleRegret)
                {
                    this.Levels.Add("Sole Regret");
                }
                this.Levels.Add("Wharf District");
                this.Levels.Add("Workmen Ward");
                this.Levels.Add("Hanging Gardens");
                this.Levels.Add("Cinderbrick Fort");
                this.Levels.Add("Roathus Lagoon");
                this.Levels.Add("Prosper Bluff");
                this.Levels.Add("Wild Outskirts");
                this.Levels.Add("Point Lemaign");
                this.Levels.Add("Mount Zand");
                this.Levels.Add("Urzendra Gate");
                this.Levels.Add("Zulten's Hollow");
                this.Levels.Add("Colford Cauldron");
                this.Levels.Add("End");
            }
            else if (category.Contains("ANY%"))
            {
                Console.WriteLine("Category Detected: Any%");
                if(this.SoleRegret)
                {
                    this.Levels.Add("Sole Regret");
                }
                this.Levels.Add("Wharf District");
                this.Levels.Add("End");
            }
            else
            {
                Console.WriteLine("No valid category detected. This program currently supports: Any%, Any% No MS, ASL, All Weapons (Exit code: 3)");
                Console.ReadKey();
                Environment.Exit(3);
            }
            
            return Levels;
        }

        private Dictionary<string, string> SetSplits(string comparison)
        {
            this.Splits = new Dictionary<string, string>();

            XmlNodeList splitNames = File.GetElementsByTagName("Name");
            XmlNodeList splitTimes;

            if (comparison.Equals("SOB"))
            {
                splitTimes = File.GetElementsByTagName("BestSegmentTime");
            }
            else
            {
                splitTimes = File.DocumentElement.SelectNodes("./Segments/Segment/SplitTimes/SplitTime[@name='Personal Best']");
            }
            
            
            string name, time;

            for (int k = 0; k < splitNames.Count; k++)
            {
                name = splitNames.Item(k).InnerText;
                if(!name.Equals(this.Levels[k]))
                {
                    name = this.Levels[k];
                }

                time = splitTimes.Item(k).InnerText.Trim();
                if(!time.Contains("."))
                {
                    time = time.Substring(0, 8);
                    time += ".00";
                }
                time = time.Substring(3, 8);

                this.Splits.Add(name, time);
            }
            return this.Splits;
        }
    }
}