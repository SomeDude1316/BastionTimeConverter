using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastionTimeConverter
{
    class OffsetData
    {
        public Dictionary<string, int> Delays { get; private set; }
        public OffsetData()
        {
            Delays = new Dictionary<string, int>();

            //Story Levels
            Delays.Add("Sole Regret", 0);
            Delays.Add("Wharf District", 175);
            Delays.Add("Workmen Ward", 50);
            Delays.Add("Melting Pot", 500);
            Delays.Add("Sundown Path", 1710);
            Delays.Add("Hanging Gardens", 500);
            Delays.Add("Cinderbrick Fort", 450);
            Delays.Add("Pyth Orchard", 50);
            Delays.Add("Langston River", 250);
            Delays.Add("Prosper Bluff", 485);
            Delays.Add("Wild Outskirts", 1100);
            Delays.Add("Jawson Bog", 750);
            Delays.Add("Roathus Lagoon", 350);
            Delays.Add("Point Lemaign", 450);
            Delays.Add("Colford Cauldron", 500);
            Delays.Add("Mount Zand", 550);
            Delays.Add("Burstone Quarry", 450);
            Delays.Add("Urzendra Gate", 750);
            Delays.Add("Zulten's Hollow", 950);
            Delays.Add("Tazal Terminals 1", 0);
            Delays.Add("Battering Ram", 0);
            Delays.Add("Tazal Terminals 2", 0);
            Delays.Add("Tazal Terminals", 0);
            Delays.Add("End", 0);

            //Challenges
            Delays.Add("Scrap Yard", 50);
            Delays.Add("Trapper Shingle", 50);
            Delays.Add("Breaker Barracks", 50);
            Delays.Add("Windbag Ranch", 50);
            Delays.Add("Zulwood Grove", 50);
            Delays.Add("Slinger Range", 50);
            Delays.Add("Camp Dauncy", 50);
            Delays.Add("Trigger Hill", 50);
            Delays.Add("Grady Incinerator", 50);
            Delays.Add("Boundless Bay", 50);
            Delays.Add("Mancer Observatory", 50);
            Delays.Add("Bullhead Court", 50);

            //Dreams
            Delays.Add("Kid's Dream", 0);
            Delays.Add("Singer's Dream", 0);
            Delays.Add("Survivor's Dream", 0);
            Delays.Add("Stranger's Dream", 0);
        }
    }
}
