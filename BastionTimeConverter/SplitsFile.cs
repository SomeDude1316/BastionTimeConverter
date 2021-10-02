using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BastionTimeConverter
{
    class SplitsFile
    {
        //PROPERTIES
        public XmlDocument File { get; private set; }
        public string GameName { get; private set; }
        public bool IsValid { get; private set; }
        public string CatName { get; private set; }
        public string CatDetect { get; private set; }
        public string AttemptCount { get; private set; }
        public List<string> Levels { get; private set; }
        public Dictionary<string, TimeSpan> PBSplits { get; private set; }
        public Dictionary<string, TimeSpan> SOBSplits { get; private set; }
        public bool AutoSplitSet { get; private set; }
        public bool IsSkyway { get; private set; }
        public bool IsLoad { get; private set; }
        public bool SoleRegret { get; private set; }
        public bool Tazal { get; private set; }
        public bool Ram { get; private set; }
        public Timing Time { get; private set; }
        public Timing Target { get; private set; }

        //CONSTRUCTOR
        public SplitsFile(XmlDocument doc)
        {
            File = doc;
            GameName = File.GetElementsByTagName("GameName").Item(0).InnerText;
            IsValid = ValidateGameName();
            CatName = File.GetElementsByTagName("CategoryName").Item(0).InnerText;
            AttemptCount = File.GetElementsByTagName("AttemptCount").Item(0).InnerText;

            CheckAutosplitter();

            Time = SetComparison();
            Target = SetTarget();

            Levels = BuildLevelList();
            PBSplits = SetSplits(Comparison.PersonalBest);
            SOBSplits = SetSplits(Comparison.SumOfBest);
        }

        public SplitsFile()
        {
            IsValid = false;
        }

        //METHODS
        private bool ValidateGameName()
        {
            if (GameName.ToUpper().Equals("BASTION"))
            {
                return true;
            }
            return false;
        }

        private void CheckAutosplitter()
        {
            if (File.GetElementsByTagName("AutoSplitterSettings").Item(0).ChildNodes.Count == 0)
            {
                AutoSplitSet = false;
                IsSkyway = false;
                IsLoad = false;
                SoleRegret = false;
                Tazal = false;
                Ram = false;
            }
            else
            {
                AutoSplitSet = true;
                IsSkyway = Boolean.Parse(File.GetElementsByTagName("Skyway_Mode").Item(0).InnerText);
                IsLoad = Boolean.Parse(File.GetElementsByTagName("Load_Mode").Item(0).InnerText);
                SoleRegret = Boolean.Parse(File.GetElementsByTagName("SoleRegret").Item(0).InnerText);
                Tazal = Boolean.Parse(File.GetElementsByTagName("Tazal").Item(0).InnerText);
                Ram = Boolean.Parse(File.GetElementsByTagName("Ram").Item(0).InnerText);
            }
        }

        private Timing SetComparison()
        {
            if (IsSkyway)
            {
                return Timing.Skyway;
            }
            else if (IsLoad)
            {
                return Timing.Load;
            }
            else
            {
                return Timing.Error;
            }
        }

        private Timing SetTarget()
        {
            if (IsSkyway)
            {
                return Timing.Load;
            }
            else if (IsLoad)
            {
                return Timing.Skyway;
            }
            else
            {
                return Timing.Error;
            }
        }

        private List<string> BuildLevelList()
        {
            Levels = new List<string>();
            string category = CatName.ToUpper();

            //No Menu Storage
            if (category.Contains("NO MS") || category.Contains("NMS") || category.Contains("NO MENU STORAGE"))
            {
                CatDetect = "Any% No Menu Storage";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("Workmen Ward");
                Levels.Add("Hanging Gardens");
                Levels.Add("Cinderbrick Fort");
                Levels.Add("Roathus Lagoon");
                Levels.Add("Colford Cauldron");
                Levels.Add("Burstone Quarry");
                Levels.Add("End");
            }
            //All Story Levels
            else if (category.Contains("ASL") || category.Contains("ALL STORY LEVELS"))
            {
                CatDetect = "All Story Levels";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("Workmen Ward");
                Levels.Add("Melting Pot");
                Levels.Add("Sundown Path");
                Levels.Add("Hanging Gardens");
                Levels.Add("Cinderbrick Fort");
                Levels.Add("Pyth Orchard");
                Levels.Add("Langston River");
                Levels.Add("Prosper Bluff");
                Levels.Add("Wild Outskirts");
                Levels.Add("Jawson Bog");
                Levels.Add("Roathus Lagoon");
                Levels.Add("Point Lemaign");
                Levels.Add("Colford Cauldron");
                Levels.Add("Mount Zand");
                Levels.Add("Burstone Quarry");
                Levels.Add("Urzendra Gate");
                Levels.Add("Zulten's Hollow");
                if (Tazal)
                {
                    Levels.Add("Tazal Terminals 1");
                    Levels.Add("Tazal Terminals 2");
                }
                else if (Ram)
                {
                    Levels.Add("Battering Ram");
                    Levels.Add("Tazal Terminals 2");
                }
                else
                {
                    Levels.Add("Tazal Terminals");
                }
                Levels.Add("End");
            }
            //All Weapons
            else if (category.Contains("WEAPONS") || category.Contains("AW"))
            {
                CatDetect = "All Weapons";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("Workmen Ward");
                Levels.Add("Hanging Gardens");
                Levels.Add("Cinderbrick Fort");
                Levels.Add("Roathus Lagoon");
                Levels.Add("Prosper Bluff");
                Levels.Add("Wild Outskirts");
                Levels.Add("Point Lemaign");
                Levels.Add("Mount Zand");
                Levels.Add("Urzendra Gate");
                Levels.Add("Zulten's Hollow");
                Levels.Add("Colford Cauldron");
                Levels.Add("End");
            }
            //Any%
            else if (category.Contains("ANY%"))
            {
                CatDetect = "Any%";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("End");
            }
            //All Interactive Collectibles
            else if (category.Contains("COLLECTIBLES") || category.Contains("AIC"))
            {
                CatDetect = "All Interactive Collectibles";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("Melting Pot");
                Levels.Add("Workmen Ward");
                Levels.Add("Sundown Path");
                Levels.Add("Cinderbrick Fort");
                Levels.Add("Pyth Orchard");
                Levels.Add("Scrap Yard");
                Levels.Add("Zulwood Grove");
                Levels.Add("Windbag Ranch");
                Levels.Add("Breaker Barracks");
                Levels.Add("Trapper Shingle");
                Levels.Add("Hanging Gardens");
                Levels.Add("Roathus Lagoon");
                Levels.Add("Prosper Bluff");
                Levels.Add("Wild Outskirts");
                Levels.Add("Point Lemaign");
                Levels.Add("Colford Cauldron");
                Levels.Add("Mount Zand");
                Levels.Add("Urzendra Gate");
                Levels.Add("Zulten's Hollow");
                Levels.Add("Trigger Hill");
                Levels.Add("Boundless Bay");
                Levels.Add("Slinger Range");
                Levels.Add("Camp Dauncy");
                Levels.Add("Grady Incinerator");
                Levels.Add("Mancer Observatory");
                Levels.Add("Burstone Quarry");
                Levels.Add("End");
            }
            //Dreams and Challenges
            else if (category.Contains("DREAMS") || category.Contains("DC"))
            {
                CatDetect = "Dreams and Challenges";
                if (SoleRegret)
                {
                    Levels.Add("Sole Regret");
                }
                Levels.Add("Wharf District");
                Levels.Add("Cinderbrick Fort");
                Levels.Add("Workmen Ward");
                Levels.Add("Hanging Gardens");
                Levels.Add("Colford Cauldron");
                Levels.Add("Mount Zand");
                Levels.Add("Urzendra Gate");
                Levels.Add("Zulten's Hollow");
                Levels.Add("Trigger Hill");
                Levels.Add("Boundless Bay");
                Levels.Add("Scrap Yard");
                Levels.Add("Zulwood Grove");
                Levels.Add("Windbag Ranch");
                Levels.Add("Breaker Barracks");
                Levels.Add("Trapper Shingle");
                Levels.Add("Bullhead Court");
                Levels.Add("Camp Dauncy");
                Levels.Add("Slinger Range");
                Levels.Add("Grady Incinerator");
                Levels.Add("Mancer Observatory");
                Levels.Add("Point Lemaign");
                Levels.Add("Kid's Dream");
                Levels.Add("Singer's Dream");
                Levels.Add("Survivor's Dream");
                Levels.Add("End");
            }
            //Fail Case
            else
            {
                CatDetect = "Error";
                return null;
            }

            if (Levels.Count != File.GetElementsByTagName("Name").Count)
            {
                return null;
            }

            return Levels;
        }

        private Dictionary<string, TimeSpan> SetSplits(Comparison comparison)
        {
            if (Levels == null)
            {
                return null;
            }

            Dictionary<string, TimeSpan> Splits = new Dictionary<string, TimeSpan>();

            XmlNodeList splitNames = File.GetElementsByTagName("Name");
            XmlNodeList splitTimes;

            if (comparison == Comparison.SumOfBest)
            {
                splitTimes = File.DocumentElement.SelectNodes("./Segments/Segment/BestSegmentTime/RealTime");
            }
            else if (comparison == Comparison.PersonalBest)
            {
                splitTimes = File.DocumentElement.SelectNodes("./Segments/Segment/SplitTimes/SplitTime[@name='Personal Best']");
            }
            else
            {
                return null;
            }

            if (splitTimes == null || splitTimes[0].InnerText == "")
            {
                return null;
            }

            string name;
            string timeStr;
            TimeSpan time;

            for (int k = 0; k < splitNames.Count; k++)
            {
                name = splitNames.Item(k).InnerText;
                if (!name.Equals(Levels[k]))
                {
                    name = Levels[k];
                }

                timeStr = splitTimes.Item(k).InnerText.Trim();
                if (!timeStr.Contains(".")) timeStr += ".00";
                time = TimeSpan.Parse(timeStr.Substring(0, 11));

                Splits.Add(name, time);
            }

            return Splits;
        }
        public enum Timing
        {
            Skyway,
            Load,
            Error
        }

        public enum Comparison
        {
            PersonalBest,
            SumOfBest,
            Error
        }
    }
}
