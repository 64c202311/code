using System.Collections.Generic;
using UnityEngine;
using HexType;

namespace HexManagerType
{
    public class HexManager : MonoBehaviour
    {
        [System.Serializable]
        public struct HexVisual
        {
            public Hex.HexType Type;
            public Sprite VisualRepresentation;
        }

        public List<HexVisual> HexVisuals;

        private Dictionary<Hex.HexType, Sprite> hexVisualDictionary;

        private void Awake()
        {
            hexVisualDictionary = new Dictionary<Hex.HexType, Sprite>();
            foreach (var hexVisual in HexVisuals)
            {
                hexVisualDictionary[hexVisual.Type] = hexVisual.VisualRepresentation;
            }
        }

        public Sprite GetVisualRepresentation(Hex.HexType type)
        {
            return hexVisualDictionary[type];
        }
    }
}