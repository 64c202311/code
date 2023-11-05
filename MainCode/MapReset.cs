using UnityEngine;
using UnityEngine.SceneManagement;
using HexType; 
using HexManagerType;
using AreaType;
using MapGeneratorMain;

public class GameStarter : MonoBehaviour
{
    public void StartGame()
    {
        Area startingArea = GenerateRandomAreaForTier(1); // Generujeme náhodnou oblast pro první tier
        GenerateMap(startingArea);
        SceneManager.LoadScene("GameScene");
    }
}
