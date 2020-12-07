﻿using System;
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
        public Dictionary<string, string> PBSkyway { get; private set; }
        public Dictionary<string, string> SOBSkyway { get; private set; }
        public Dictionary<string, string> PBLoad { get; private set; }
        public Dictionary<string, string> SOBLoad { get; private set; }

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
            if (doc.GetElementsByTagName("GameName").Item(0).InnerText.ToUpper().Equals("BASTION"))
            {
                File = new SplitsFile(doc);
            }
            else
            {
                File = new SplitsFile();
            }
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

            Dictionary<string, string> splits;

            if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
            {
                splits = File.PBSplits;
            }
            else
            {
                splits = File.SOBSplits;
            }

            string format = "{0, 18} {1, 15} {2, 15}";
            Console.WriteLine(String.Format(format, "Split", "Skyway", "Load"));
            Console.WriteLine();

            Dictionary<string, string> output = new Dictionary<string, string>();

            int timeInMs, diff, prevDiff = 0, totalDiff = 0;
            int totalSkyway = 0, totalLoad = 0;
            string currentLevel;

            for (int k = 0; k < File.Levels.Count; k++)
            {
                currentLevel = File.Levels[k];
                timeInMs = StringToInt(splits[currentLevel]);
                diff = Offsets.Delays[currentLevel];

                if (timeInMs == 0)
                {
                    Console.WriteLine(String.Format(format, currentLevel, "--------", "--------"));
                    output.Add(currentLevel, "00:00.00");
                    continue;
                }

                if (File.Target.Equals(SplitsFile.Timing.Load))
                {
                    totalSkyway += timeInMs;

                    timeInMs -= prevDiff;
                    timeInMs += diff;

                    if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
                    {

                        timeInMs += totalDiff;
                        totalDiff += diff;
                        totalDiff -= prevDiff;

                        output.Add(currentLevel, IntToString(timeInMs));
                    }
                    else
                    {
                        output.Add(currentLevel, IntToString(timeInMs));
                    }

                    totalLoad += timeInMs;

                    Console.WriteLine(String.Format(format, currentLevel, splits[currentLevel], IntToString(timeInMs)));
                }
                else
                {
                    totalLoad += timeInMs;


                    timeInMs += prevDiff;
                    timeInMs -= diff;


                    if (comparison.Equals(SplitsFile.Comparison.PersonalBest))
                    {

                        timeInMs += totalDiff;
                        totalDiff -= diff;
                        totalDiff += prevDiff;

                        output.Add(currentLevel, IntToString(timeInMs));
                    }
                    else
                    {
                        output.Add(currentLevel, IntToString(timeInMs));
                    }

                    totalSkyway += timeInMs;

                    Console.WriteLine(String.Format(format, currentLevel, IntToString(timeInMs), splits[currentLevel]));
                }
                prevDiff = diff;
            }

            if (comparison.Equals(SplitsFile.Comparison.SumOfBest))
            {
                Console.WriteLine();
                Console.WriteLine(String.Format(format, "Total", IntToString(totalSkyway), IntToString(totalLoad)));
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
        }

        private int StringToInt(string time)
        {
            int min = int.Parse(time.Substring(0, 2)) * 60 * 100;
            int sec = int.Parse(time.Substring(3, 2)) * 100;
            int ms = int.Parse(time.Substring(6, 2));

            int total = min + sec + ms;
            return total;
        }

        private String IntToString(int num)
        {
            string min = String.Format("{0:D2}", (num / 6000));
            string sec = String.Format("{0:D2}", (num / 100 % 60));
            string ms = String.Format("{0:D2}", (num % 100));

            return min + ":" + sec + "." + ms;
        }
    }
}
