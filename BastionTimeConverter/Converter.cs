using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace BastionTimeConverter
{
    class Converter
    {
        public OffsetData Offsets { get; private set; }
        public SplitsFile File { get; private set; }
        public Dictionary<string, TimeSpan> PBSkyway { get; private set; }
        public Dictionary<string, TimeSpan> SOBSkyway { get; private set; }
        public Dictionary<string, TimeSpan> PBLoad { get; private set; }
        public Dictionary<string, TimeSpan> SOBLoad { get; private set; }

        public Converter()
        {
            Offsets = new OffsetData();
            File = null;
            PBSkyway = null;
            SOBSkyway = null;
            PBLoad = null;
            SOBLoad = null;
        }

        public void SetFile(string openFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(openFile);

            File = doc.GetElementsByTagName("GameName").Item(0).InnerText.ToUpper().Equals("BASTION") ? new SplitsFile(doc) : new SplitsFile();
        }

        public void Convert(SplitsFile.Comparison comparison)
        {
            if (File.Time.Equals(SplitsFile.Timing.Skyway))
            {
                PBSkyway = File.PBSplits;
                SOBSkyway = File.SOBSplits;
            }
            else
            {
                PBLoad = File.PBSplits;
                SOBLoad = File.SOBSplits;
            }

            Dictionary<string, TimeSpan> splits;

            splits = comparison.Equals(SplitsFile.Comparison.PersonalBest) ? File.PBSplits : File.SOBSplits;


            Dictionary<string, TimeSpan> output = new Dictionary<string, TimeSpan>();

            TimeSpan currTime, diff, prevDiff = TimeSpan.Zero, totalDiff = TimeSpan.Zero;
            TimeSpan totalSkyway = TimeSpan.Zero, totalLoad = TimeSpan.Zero;
            string currLevel;

            for (int k = 0; k < File.Levels.Count; k++)
            {
                currLevel = File.Levels[k];
                currTime = splits[currLevel];
                diff = Offsets.Delays[currLevel];

                if (currTime == TimeSpan.Zero)
                {
                    output.Add(currLevel, TimeSpan.Zero);
                    continue;
                }

                if (File.Target.Equals(SplitsFile.Timing.Load))
                {
                    totalSkyway = totalSkyway.Add(currTime);

                    currTime = currTime.Subtract(prevDiff);
                    currTime = currTime.Add(diff);

                    if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
                    {

                        currTime = currTime.Add(totalDiff);
                        totalDiff = totalDiff.Add(diff);
                        totalDiff = totalDiff.Subtract(prevDiff);

                        output.Add(currLevel, currTime);
                    }
                    else
                    {
                        output.Add(currLevel, currTime);
                    }

                    totalLoad = totalLoad.Add(currTime);
                }
                else
                {
                    totalLoad = totalLoad.Add(currTime);


                    currTime = currTime.Add(prevDiff);
                    currTime = currTime.Subtract(diff);


                    if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
                    {

                        currTime = currTime.Add(totalDiff);
                        totalDiff = totalDiff.Subtract(diff);
                        totalDiff = totalDiff.Add(prevDiff);

                        output.Add(currLevel, currTime);
                    }
                    else
                    {
                        output.Add(currLevel, currTime);
                    }

                    totalSkyway = totalSkyway.Add(currTime);
                }
                prevDiff = diff;
            }

            if (File.Target.Equals(SplitsFile.Timing.Skyway) && comparison.Equals(SplitsFile.Comparison.PersonalBest))
            {
                PBSkyway = output;
            }
            else if (File.Target.Equals(SplitsFile.Timing.Skyway) && comparison.Equals(SplitsFile.Comparison.SumOfBest))
            {
                SOBSkyway = output;
            }
            else if (File.Target.Equals(SplitsFile.Timing.Load) && comparison.Equals(SplitsFile.Comparison.PersonalBest))
            {
                PBLoad = output;
            }
            else
            {
                SOBLoad = output;
            }
            WriteToBox(comparison, totalSkyway, totalLoad);
        }

        private void WriteToBox(SplitsFile.Comparison comparison, TimeSpan totalSkyway, TimeSpan totalLoad)
        {
            Dictionary<string, TimeSpan> skyway, load;
            if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
            {
                skyway = PBSkyway;
                load = PBLoad;
            }
            else
            {
                skyway = SOBSkyway;
                load = SOBLoad;
            }

            string timeFormat, format = "{0, 18} {1, 15} {2, 15}";

            Console.WriteLine(format, "Split", "Skyway", "Load");
            Console.WriteLine();

            foreach (KeyValuePair<string, TimeSpan> entry in skyway)
            {
                if (entry.Value.Equals(TimeSpan.Zero))
                {
                    if (entry.Value.Hours > 0) Console.WriteLine(format, entry.Key, "-----------", "-----------");
                    else Console.WriteLine(format, entry.Key, "--------", "--------");
                }
                else
                {
                    timeFormat = entry.Value.Hours > 0 ? @"%h\:mm\:ss\.ff" : @"%m\:ss\.ff";

                    Console.WriteLine(format, entry.Key, entry.Value.ToString(timeFormat), load[entry.Key].ToString(timeFormat));
                }
            }

            if (comparison.Equals(SplitsFile.Comparison.SumOfBest))
            {
                timeFormat = totalSkyway.Hours > 0 ? @"%h\:mm\:ss\.ff" : @"%m\:ss\.ff";

                Console.WriteLine();
                Console.WriteLine(format, "Total", totalSkyway.ToString(timeFormat), totalLoad.ToString(timeFormat));
            }
        }
    }
}
