using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BastionTimeConverter
{

    class Exporter
    {
        public string FileName { get; private set; }
        public SplitsFile File { get; private set; }
        public Dictionary<string, TimeSpan> SkywayPB { get; private set; }
        public Dictionary<string, TimeSpan> SkywaySOB { get; private set; }
        public Dictionary<string, TimeSpan> LoadPB { get; private set; }
        public Dictionary<string, TimeSpan> LoadSOB { get; private set; }
        public OffsetData Offsets { get; private set; }

        public Exporter(string fileName,
            SplitsFile file,
            Dictionary<string, TimeSpan> skywayPB,
            Dictionary<string, TimeSpan> skywaySOB,
            Dictionary<string, TimeSpan> loadPB,
            Dictionary<string, TimeSpan> loadSOB)
        {
            FileName = fileName;
            File = file;
            SkywayPB = skywayPB;
            SkywaySOB = skywaySOB;
            LoadPB = loadPB;
            LoadSOB = loadSOB;
            Offsets = new OffsetData();
        }

        public void Export(Target target)
        {

            Dictionary<string, TimeSpan> personalBest, sumOfBest;

            if (target.Equals(Target.Skyway))
            {
                personalBest = SkywayPB;
                sumOfBest = SkywaySOB;
            }
            else
            {
                personalBest = LoadPB;
                sumOfBest = LoadSOB;
            }

            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

            XmlWriter writer = XmlWriter.Create($"{FileName}", settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("Run");
            writer.WriteAttributeString("version", null, "1.7.0");

            writer.WriteElementString("GameIcon", null);
            writer.WriteElementString("GameName", "Bastion");
            writer.WriteElementString("CategoryName", File.CatName);

            writer.WriteStartElement("Metadata");

            writer.WriteStartElement("Run");
            writer.WriteAttributeString("id", null, "");
            writer.WriteEndElement(); //Run

            writer.WriteStartElement("Platform");
            writer.WriteAttributeString("usesEmulator", null, "False");
            writer.WriteEndElement(); //Platform

            writer.WriteElementString("Region", null);

            writer.WriteElementString("Variables", null);

            writer.WriteEndElement(); //Metadata

            writer.WriteElementString("Offset", "00:00:00");
            writer.WriteElementString("AttemptCount", File.AttemptCount);
            writer.WriteStartElement("AttemptHistory");

            XmlNodeList nodes = File.File.DocumentElement["AttemptHistory"].ChildNodes;

            foreach (XmlNode attempt in nodes)
            {
                writer.WriteStartElement("Attempt");
                
                for (int k = 0; k < attempt.Attributes.Count; k++)
                {
                    writer.WriteAttributeString(attempt.Attributes[k].Name, attempt.Attributes[k].Value);
                }

                if (attempt.HasChildNodes && attempt.FirstChild.Name == "RealTime")
                {
                    writer.WriteElementString("RealTime", attempt.FirstChild.InnerText);
                }

                writer.WriteEndElement(); //Attempt
            }

            writer.WriteEndElement(); //AttemptHistory

            writer.WriteStartElement("Segments");
            
            nodes = File.File.GetElementsByTagName("Segment");
            XmlNodeList times = null;

            foreach (KeyValuePair<string, TimeSpan> entry in personalBest)
            {
                writer.WriteStartElement("Segment");

                writer.WriteElementString("Name", entry.Key);
                writer.WriteElementString("Icon", null);
                writer.WriteStartElement("SplitTimes");

                writer.WriteStartElement("SplitTime");
                writer.WriteAttributeString("name", null, "Personal Best");

                if (!entry.Value.Equals(TimeSpan.Zero))
                {
                    writer.WriteElementString("RealTime", entry.Value.ToString());
                }

                writer.WriteEndElement(); //SplitTime
                writer.WriteEndElement(); //SplitTimes

                writer.WriteStartElement("BestSegmentTime");
                writer.WriteElementString("RealTime", sumOfBest[entry.Key].ToString());
                writer.WriteEndElement(); //BestSegmentTime

                writer.WriteStartElement("SegmentHistory");

                foreach (XmlNode seg in nodes)
                {
                    if (seg["Name"].InnerText == entry.Key)
                    {
                        times = seg["SegmentHistory"].ChildNodes;
                        continue;
                    }
                }
                
                foreach (XmlNode time in times)
                {
                    writer.WriteStartElement("Time");
                    writer.WriteAttributeString(time.Attributes[0].Name, time.Attributes[0].Value);

                    if (time.HasChildNodes && time.FirstChild.Name == "RealTime")
                    {
                        writer.WriteElementString("RealTime", Convert(time["RealTime"].InnerText, entry.Key, target));
                    }

                    writer.WriteEndElement(); //Time
                }

                writer.WriteEndElement(); //SegmentHistory
                writer.WriteEndElement(); //Segment
            }

            writer.WriteEndElement(); //Segments

            writer.WriteStartElement("AutoSplitterSettings");

            writer.WriteElementString("IL_Mode", "False");
            writer.WriteElementString("Skyway_Mode", target.Equals(Target.Skyway) ? "True" : "False");
            writer.WriteElementString("Load_Mode", target.Equals(Target.Load) ? "True" : "False");
            writer.WriteElementString("Reset", "True");
            writer.WriteElementString("Start", "True");
            writer.WriteElementString("Split", "True");
            writer.WriteElementString("End", "True");
            writer.WriteElementString("Tazal", File.Tazal.ToString());
            writer.WriteElementString("Ram", File.Ram.ToString());
            writer.WriteElementString("SoleRegret", File.SoleRegret.ToString());

            writer.WriteEndElement(); //AutoSplitterSettings

            writer.WriteEndElement(); //Run
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        private string Convert(string time, string level, Target target)
        {
            TimeSpan split = TimeSpan.Parse(time);
            TimeSpan diff = Offsets.Delays[level];
            TimeSpan prevDiff = File.Levels.IndexOf(level) - 1 > -1 ? Offsets.Delays[File.Levels[File.Levels.IndexOf(level) - 1]] : TimeSpan.Zero;

            if (target == Target.Skyway)
            {
                split = split.Add(prevDiff);
                split = split.Subtract(diff);
            }
            else
            {
                split = split.Subtract(prevDiff);
                split = split.Add(diff);
            }
            
            return split.ToString();
        }

        public enum Target
        {
            Skyway,
            Load
        }
    }
}
