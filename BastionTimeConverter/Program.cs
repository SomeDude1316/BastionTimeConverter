using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace BastionTimeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument document = ReadFile();
            
            string comparison = "";
            
            do
            {
                Console.Write("Compare against Personal Best or Sum of Best? (PB/SOB): ");
                comparison = Console.ReadLine().ToUpper();
            } while (comparison != "PB" && comparison != "SOB");
            
            SplitsFile splits = new SplitsFile(document, comparison);

            Dictionary<string, int> delays = MakeDelayDict();
            List<string> levelList = splits.Levels;
            Dictionary<string,string> sumOfBest = splits.Splits;
            string timing = splits.CurrentTiming();
            string category = splits.CatName;

            string convertTo = ConfirmConversion(timing);
            switch (convertTo)
            {
                case "error":
                Console.ReadKey();
                    Environment.Exit(4);
                    break;
                case "cancel":
                Console.ReadKey();
                    Environment.Exit(5);
                    break;
                default:
                    break;
            }

            Convert(sumOfBest, delays, levelList, category, convertTo, comparison);

            Console.Write("Press any key to exit");
            Console.ReadKey();
        }

        static XmlDocument ReadFile()
        {
            XmlDocument doc = new XmlDocument();
            
            string currentDir = Environment.CurrentDirectory;
            Console.Write($"Searching {currentDir} for splits files...");
            string[] files = Directory.GetFiles(currentDir, "*.lss");

            if (files.Length == 0)
            {
                Console.WriteLine("Error: No file found (Exit code: 1)");
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.WriteLine($"Found {files.Length} splits file" + (files.Length == 1 ? "" : "s"));

            string[] file;
            string ans = "";
            bool noneChosen = false;
            
            for (int k = 0; k < files.Length; k++)
            {
                file = files[k].Split('\\');
                Console.Write($"Load {file[file.Length - 1]}? (y/n): ");
                ans = Console.ReadLine();

                if (ans.ToUpper().Equals("Y"))
                {
                    try
                    {
                        doc.Load(files[k]);
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.WriteLine("Error: No file found (Exit code: 1)");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    break;
                }
                noneChosen = k == files.Length - 1;
            }

            if (noneChosen)
            {
                Console.WriteLine("No file selected (Exit code: 2)");
                Console.ReadKey();
                Environment.Exit(2);
            }

            return doc;
        }

        static Dictionary<string, int> MakeDelayDict()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //Story Levels
            dict.Add("Sole Regret", 0);
            dict.Add("Wharf District", 175);
            dict.Add("Workmen Ward", 50);
            dict.Add("Melting Pot", 500);
            dict.Add("Sundown Path", 1710);
            dict.Add("Hanging Gardens", 500);
            dict.Add("Cinderbrick Fort", 450);
            dict.Add("Pyth Orchard", 50);
            dict.Add("Langston River", 250);
            dict.Add("Prosper Bluff", 485);
            dict.Add("Wild Outskirts", 1100);
            dict.Add("Jawson Bog", 750);
            dict.Add("Roathus Lagoon", 350);
            dict.Add("Point Lemaign", 450);
            dict.Add("Colford Cauldron", 500);
            dict.Add("Mount Zand", 550);
            dict.Add("Burstone Quarry", 450);
            dict.Add("Urzendra Gate", 750);
            dict.Add("Zulten's Hollow", 950);
            dict.Add("Tazal Terminals 1", 0);
            dict.Add("Battering Ram", 0);
            dict.Add("Tazal Terminals 2", 0);
            dict.Add("Tazal Terminals", 0);
            dict.Add("End", 0);

            //Challenges
            dict.Add("Scrap Yard", 50);
            dict.Add("Trapper Shingle", 50);
            dict.Add("Breaker Barracks", 50);
            dict.Add("Windbag Ranch", 50);
            dict.Add("Zulwood Grove", 50);
            dict.Add("Slinger Range", 50);
            dict.Add("Camp Dauncy", 50);
            dict.Add("Trigger Hill", 50);
            dict.Add("Grady Incinerator", 50);
            dict.Add("Boundless Bay", 50);
            dict.Add("Mancer Observatory", 50);
            dict.Add("Bullhead Court", 50);

            //Dreams
            dict.Add("Kid's Dream", 0);
            dict.Add("Singer's Dream", 0);
            dict.Add("Survivor's Dream", 0);
            dict.Add("Stranger's Dream", 0);

            return dict;
        }

        static string ConfirmConversion(string currentTiming)
        {
            String newTiming = "";
            if (currentTiming.Equals("Skyway"))
            {
                newTiming = "Load";
            }
            else if (currentTiming.Equals("Load"))
            {
                newTiming = "Skyway";
            }
            else
            {
                Console.WriteLine("Error: Unable to detect current timing, cannot convert. (Exit code: 4)");
                return "error";
            }

            Console.Write($"File lists current timing method as {currentTiming} timing. Convert to {newTiming} timing? (y/n): ");
            string ans = Console.ReadLine();
            if (!ans.ToUpper().Equals("Y"))
            {
                Console.WriteLine("Conversion Canceled. (Exit code: 5)");
                return "cancel";
            }

            return newTiming;
        }

        static int StringToInt(string time)
        {
            int min = Int32.Parse(time.Substring(0, 2)) * 60 * 100;
            int sec = Int32.Parse(time.Substring(3, 2)) * 100;
            int ms = Int32.Parse(time.Substring(6, 2));

            int total = min + sec + ms;
            return total;
        }

        static String IntToString(int num)
        {        
            string min = String.Format("{0:D2}", (num / 6000));
            string sec = String.Format("{0:D2}", (num / 100 % 60));
            string ms = String.Format("{0:D2}", (num % 100));

            return min + ":" + sec + "." + ms;
        }

        static void Convert(Dictionary<string, string> times, Dictionary<string, int> delay,
                            List<string> levelList, string cat, string target, string compare)
        {
            Dictionary<string, string> timeList = times;
            Dictionary<string, int> delayList = delay;
            List<string> levels = levelList;
            string catName = cat;
            string newTiming = target;
            string comparison = compare;
            string format = "{0, 18} {1, 15} {2, 15}";

            string comparisonString;
            if (compare.Equals("PB"))
            {
                comparisonString = "Personal Best";
            }
            else
            {
                comparisonString = "Sum of Best";
            }
            
            StreamWriter writer = new StreamWriter($"{catName} {comparisonString} Splits.txt");

            writer.WriteLine($"Bastion {comparisonString} Splits");
            writer.WriteLine("Category: " + catName);
            writer.WriteLine();
            writer.WriteLine(String.Format(format, "Split", "Skyway", "Load"));
            writer.WriteLine();

            int timeInMs = 0, diff = 0, prevDiff = 0, totalDiff = 0;
            int totalSkyway = 0, totalLoad = 0;
            string currentLevel = "";

            for (int k = 0; k < levels.Count; k++)
            {
                currentLevel = levels[k];
                timeInMs = StringToInt(timeList[currentLevel]);
                diff = delayList[currentLevel];

                if (newTiming.Equals("Load"))
                {
                    totalSkyway += timeInMs;
                    
                    timeInMs = timeInMs - prevDiff;
                    timeInMs = timeInMs + diff;

                    if (comparison.Equals("PB"))
                    {
                        timeInMs += totalDiff;
                        totalDiff += diff;
                        totalDiff -= prevDiff;
                    }

                    totalLoad += timeInMs;

                    writer.WriteLine(String.Format(format, currentLevel, timeList[currentLevel], IntToString(timeInMs)));
                }
                else
                {
                    totalLoad += timeInMs;

                    timeInMs = timeInMs + prevDiff;
                    timeInMs = timeInMs - diff;

                    if (comparison.Equals("PB"))
                    {
                        timeInMs += totalDiff;
                        totalDiff -= diff;
                        totalDiff += prevDiff;
                    }

                    totalSkyway += timeInMs;

                    writer.WriteLine(String.Format(format, currentLevel, IntToString(timeInMs), timeList[currentLevel]));
                }
                prevDiff = diff;
            }

            if (comparison.Equals("SOB"))
            {
                writer.WriteLine();
                writer.WriteLine(String.Format(format, "Total", IntToString(totalSkyway), IntToString(totalLoad)));
            }
                        
            writer.Close();
            
            Console.WriteLine($"Conversion complete. Converted times are in \"{catName} {comparisonString} Splits.txt\"");
        }
    }
}