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
        public Dictionary<string, TimeSpan> SkywayPB { get; private set; }
        public Dictionary<string, TimeSpan> SkywaySOB { get; private set; }
        public Dictionary<string, TimeSpan> LoadPB { get; private set; }
        public Dictionary<string, TimeSpan> LoadSOB { get; private set; }

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

            foreach (KeyValuePair<string, TimeSpan> entry in personalBest)
            {
                writer.WriteLine("    <Segment>");
                writer.WriteLine($"      <Name>{entry.Key}</Name>");
                writer.WriteLine("      <Icon />");
                writer.WriteLine("      <SplitTimes>");
                writer.WriteLine("        <SplitTime name=\"Personal Best\">");
                if (!entry.Value.Equals(TimeSpan.Zero))
                {
                    writer.WriteLine($"          <RealTime>{entry.Value}</RealTime>");
                }
                writer.WriteLine("          <GameTime>00:00:00</GameTime>");
                writer.WriteLine("        </SplitTime>");
                writer.WriteLine("      </SplitTimes>");
                writer.WriteLine("      <BestSegmentTime>");
                writer.WriteLine($"        <RealTime>{sumOfBest[entry.Key]}</RealTime>");
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
            writer.WriteLine($"    <Tazal>{File.Tazal}</Tazal>");
            writer.WriteLine($"    <Ram>{File.Ram}</Ram>");
            writer.WriteLine($"    <SoleRegret>{File.SoleRegret}</SoleRegret>");
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
