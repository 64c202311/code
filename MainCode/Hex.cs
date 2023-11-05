using System.Collections.Generic;
using UnityEngine;

namespace HexType
{
    public class Hex
    {
        public enum HexType
        {
            Grass,
            Forest,
            Mountain,
            // ... další typy hexů
        }

        public HexType Type { get; private set; }
        public Sprite VisualRepresentation { get; private set; }
        public List<Event> PossibleEvents { get; private set; }

        public Hex(HexType type, Sprite visualRepresentation, List<Event> possibleEvents)
        {
            Type = type;
            VisualRepresentation = visualRepresentation;
            PossibleEvents = possibleEvents ?? new List<Event>(); // Přidáno pro bezpečnost
        }
    }

    // Přidána jednoduchá třída Event pro demonstraci
    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}