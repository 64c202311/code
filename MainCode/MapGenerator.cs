using UnityEngine;
using System.Collections.Generic;
using HexType; 
using HexManagerType;
using AreaType;

namespace MapGeneratorMain
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private HexManager hexManager; // Reference na HexManager
        [SerializeField] private GameObject hexPrefab;  // Prefab hexu, který bude obsahovat SpriteRenderer pro vizuální reprezentaci
        [SerializeField] private int mapWidth = 10;     // Šířka mapy v hexech
        [SerializeField] private int mapHeight = 10;    // Výška mapy v hexech
        [SerializeField] private float hexSize = 1f;    // Velikost jednoho hexu

        private Area currentArea;

        void Start()
        {
            public Area startingArea = GenerateRandomAreaForTier(1); // Generujeme náhodnou oblast pro první tier
            GenerateMap(startingArea);
        }

        public void GenerateMap(Area area)
        {
            currentArea = area;

            // Procházíme všechny hexy v oblasti a vytváříme je v Unity
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    // Zde by byla logika pro výběr náhodného hexu z currentArea
                    Hex hex = GetRandomHexFromArea(currentArea);

                    // Vytvoření instance hexu v Unity
                    GameObject hexGO = Instantiate(hexPrefab, CalculateHexPosition(x, y), Quaternion.identity);
                    hexGO.GetComponent<SpriteRenderer>().sprite = hexManager.GetVisualRepresentation(hex.Type);
                }
            }
        }

        private Hex GetRandomHexFromArea(Area area)
        {
            // Zde je logika pro náhodný výběr hexu z dostupných typů
            Hex.HexType randomType = (Hex.HexType)Random.Range(0, System.Enum.GetValues(typeof(Hex.HexType)).Length);
            Sprite randomSprite = hexManager.GetVisualRepresentation(randomType);
            List<HexType.Event> eventsForHex = new List<HexType.Event>(); // Zde můžete přidat logiku pro generování náhodných událostí pro hex
            return new Hex(randomType, randomSprite, eventsForHex);
        }

        private Vector3 CalculateHexPosition(int x, int y)
        {
            // Výpočet skutečné pozice hexu na základě jeho souřadnic v mřížce
            float xPos = x * hexSize * 1.5f;
            float yPos = y * hexSize * Mathf.Sqrt(3) + (x % 2 == 0 ? 0 : hexSize * Mathf.Sqrt(3) / 2);

            // Posunutí pozice hexu tak, aby střed mapy byl v (0,0)
            xPos -= (mapWidth * hexSize * 0.75f) / 2;  // 0.75 je proto, že hexy se překrývají o 1/4 své šířky
            yPos -= (mapHeight * hexSize * Mathf.Sqrt(3)) / 2;

            return new Vector3(xPos, yPos, 0);
        }
        private Area GenerateRandomAreaForTier(int tier)
        {
            // Zde by byla logika pro výběr náhodné oblasti z prvního tieru.
            // Pro jednoduchost nyní vrátíme novou oblast s několika náhodnými hexy.
            List<Hex> randomHexes = new List<Hex>();
            List<Hex> mainStoryHexes = new List<Hex>(); // Můžete sem přidat hexy hlavního příběhu, pokud je máte

            // Přidání několika náhodných hexů do oblasti
            for (int i = 0; i < 10; i++) // Přidáme 10 náhodných hexů pro demonstraci
            {
                Hex.HexType randomType = (Hex.HexType)Random.Range(0, System.Enum.GetValues(typeof(Hex.HexType)).Length);
                Sprite randomSprite = hexManager.GetVisualRepresentation(randomType); // Získání náhodného sprite pro daný typ hexu
                List<HexType.Event> eventsForHex = new List<HexType.Event>(); // Zde byste mohl přidat logiku pro generování náhodných událostí pro hex
                Hex randomHex = new Hex(randomType, randomSprite, eventsForHex);
                randomHexes.Add(randomHex);
            }

            return new Area("Random Area Name", randomHexes, mainStoryHexes); // Můžete změnit "Random Area Name" na něco smysluplnějšího
        }
    }
}