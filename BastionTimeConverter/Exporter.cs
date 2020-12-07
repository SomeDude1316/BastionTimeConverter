using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastionTimeConverter
{

    class Exporter
    {
        public string FileName { get; private set; }
        public SplitsFile File { get; private set; }
        public Dictionary<string, string> SkywayPB { get; private set; }
        public Dictionary<string, string> SkywaySOB { get; private set; }
        public Dictionary<string, string> LoadPB { get; private set; }
        public Dictionary<string, string> LoadSOB { get; private set; }

        public Exporter(string fileName,
            SplitsFile file,
            Dictionary<string, string> skywayPB,
            Dictionary<string, string> skywaySOB,
            Dictionary<string, string> loadPB,
            Dictionary<string, string> loadSOB)
        {
            FileName = fileName;
            File = file;
            SkywayPB = skywayPB;
            SkywaySOB = skywaySOB;
            LoadPB = loadPB;
            LoadSOB = loadSOB;
        }

        public void Export(Target target)
        {

            Dictionary<string, string> personalBest, sumOfBest;

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

            StreamWriter writer = new StreamWriter($"{FileName}");

            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            writer.WriteLine("<Run version=\"1.7.0\">");
            writer.WriteLine("  <GameIcon />");
            writer.WriteLine("  <GameName>Bastion</GameName>");
            writer.WriteLine($"  <CategoryName>{File.CatName}</CategoryName>");
            writer.WriteLine("  <Metadata>");
            writer.WriteLine("    <Run id=\"\" />");
            writer.WriteLine("    <Platform usesEmulator=\"False\">");
            writer.WriteLine("    </Platform>");
            writer.WriteLine("    <Region>");
            writer.WriteLine("    </Region>");
            writer.WriteLine("    <Variables />");
            writer.WriteLine("  </Metadata>");
            writer.WriteLine("  <Offset>00:00:00</Offset>");
            writer.WriteLine($"  <AttemptCount>{File.AttemptCount}</AttemptCount>");
            writer.WriteLine("  <AttemptHistory />");
            writer.WriteLine("  <Segments>");

            foreach (KeyValuePair<string, string> entry in personalBest)
            {
                writer.WriteLine("    <Segment>");
                writer.WriteLine($"      <Name>{entry.Key}</Name>");
                writer.WriteLine("      <Icon />");
                writer.WriteLine("      <SplitTimes>");
                writer.WriteLine("        <SplitTime name=\"Personal Best\">");
                if (!entry.Value.Equals("00:00.00"))
                {
                    writer.WriteLine($"          <RealTime>00:{entry.Value}00000</RealTime>");
                }
                writer.WriteLine("          <GameTime>00:00:00</GameTime>");
                writer.WriteLine("        </SplitTime>");
                writer.WriteLine("      </SplitTimes>");
                writer.WriteLine("      <BestSegmentTime>");
                writer.WriteLine($"        <RealTime>00:{sumOfBest[entry.Key]}00000</RealTime>");
                writer.WriteLine("      </BestSegmentTime>");
                writer.WriteLine("      <SegmentHistory />");
                writer.WriteLine("    </Segment>");
            }

            writer.WriteLine("  </Segments>");
            writer.WriteLine("  <AutoSplitterSettings>");
            writer.WriteLine("    <IL_Mode>False</IL_Mode>");
            if (target.Equals(Target.Skyway))
            {
                writer.WriteLine($"    <Skyway_Mode>True</Skyway_Mode>");
                writer.WriteLine($"    <Load_Mode>False</Load_Mode>");
            }
            else
            {
                writer.WriteLine($"    <Skyway_Mode>False</Skyway_Mode>");
                writer.WriteLine($"    <Load_Mode>True</Load_Mode>");
            }
            writer.WriteLine("    <Reset>True</Reset>");
            writer.WriteLine("    <Start>True</Start>");
            writer.WriteLine("    <Split>True</Split>");
            writer.WriteLine("    <End>True</End>");
            writer.WriteLine($"    <Tazal>{File.Tazal.ToString()}</Tazal>");
            writer.WriteLine($"    <Ram>{File.Ram.ToString()}</Ram>");
            writer.WriteLine($"    <SoleRegret>{File.SoleRegret.ToString()}</SoleRegret>");
            writer.WriteLine("  </AutoSplitterSettings>");
            writer.WriteLine("</Run>");

            writer.Close();
        }

        public enum Target
        {
            Skyway,
            Load
        }
    }
}
