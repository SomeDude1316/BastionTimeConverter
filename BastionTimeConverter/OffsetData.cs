using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastionTimeConverter
{
    class OffsetData
    {
        public Dictionary<string, TimeSpan> Delays = new Dictionary<string, TimeSpan>()
        {
            //Story Levels
            { "Sole Regret", TimeSpan.Zero },
            { "Wharf District", new TimeSpan(0, 0, 0, 1, 750) },
            { "Workmen Ward", new TimeSpan(0, 0, 0, 0, 500) },
            { "Melting Pot", new TimeSpan(0, 0, 0, 5, 0) },
            { "Sundown Path", new TimeSpan(0, 0, 0, 17, 100) },
            { "Hanging Gardens", new TimeSpan(0, 0, 0, 5, 0) },
            { "Cinderbrick Fort", new TimeSpan(0, 0, 0, 4, 500) },
            { "Pyth Orchard", new TimeSpan(0, 0, 0, 0, 500) },
            { "Langston River", new TimeSpan(0, 0, 0, 2, 500) },
            { "Prosper Bluff", new TimeSpan(0, 0, 0, 4, 850) },
            { "Wild Outskirts", new TimeSpan(0, 0, 0, 11, 0) },
            { "Jawson Bog", new TimeSpan(0, 0, 0, 7, 500) },
            { "Roathus Lagoon", new TimeSpan(0, 0, 0, 3, 500) },
            { "Point Lemaign", new TimeSpan(0, 0, 0, 4, 500) },
            { "Colford Cauldron", new TimeSpan(0, 0, 0, 5, 0) },
            { "Mount Zand", new TimeSpan(0, 0, 0, 5, 500) },
            { "Burstone Quarry", new TimeSpan(0, 0, 0, 4, 500) },
            { "Urzendra Gate", new TimeSpan(0, 0, 0, 7, 500) },
            { "Zulten's Hollow", new TimeSpan(0, 0, 0, 9, 500) },
            { "Tazal Terminals 1", TimeSpan.Zero },
            { "Battering Ram", TimeSpan.Zero },
            { "Tazal Terminals 2", TimeSpan.Zero },
            { "Tazal Terminals", TimeSpan.Zero },
            { "End", TimeSpan.Zero },

            //Challenges
            { "Scrap Yard", new TimeSpan(0, 0, 0, 0, 500) },
            { "Trapper Shingle", new TimeSpan(0, 0, 0, 0, 500) },
            { "Breaker Barracks", new TimeSpan(0, 0, 0, 0, 500) },
            { "Windbag Ranch", new TimeSpan(0, 0, 0, 0, 500) },
            { "Zulwood Grove", new TimeSpan(0, 0, 0, 0, 500) },
            { "Slinger Range", new TimeSpan(0, 0, 0, 0, 500) },
            { "Camp Dauncy", new TimeSpan(0, 0, 0, 0, 500) },
            { "Trigger Hill", new TimeSpan(0, 0, 0, 0, 500) },
            { "Grady Incinerator", new TimeSpan(0, 0, 0, 0, 500) },
            { "Boundless Bay", new TimeSpan(0, 0, 0, 0, 500) },
            { "Mancer Observatory", new TimeSpan(0, 0, 0, 0, 500) },
            { "Bullhead Court", new TimeSpan(0, 0, 0, 0, 500) },

            //Dreams
            { "Kid's Dream", TimeSpan.Zero },
            { "Singer's Dream", TimeSpan.Zero },
            { "Survivor's Dream", TimeSpan.Zero },
            { "Stranger's Dream", TimeSpan.Zero }
        };
    }
}
