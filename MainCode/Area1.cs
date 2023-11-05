using System.Collections.Generic;
using HexType;

namespace AreaType
{
    public class Area
    {
        public string Name { get; private set; }
        public List<Hex> Hexes { get; private set; }
        public List<Hex> EntryHexes { get; private set; }

        public Area(string name, List<Hex> hexes, List<Hex> entryHexes)
        {
            Hexes = new List<Hex>();
            Name = name;
            Hexes = hexes;
            EntryHexes = entryHexes;
        }
        public int Tier { get; set; }
    }

}