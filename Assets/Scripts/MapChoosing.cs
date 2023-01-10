using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChoosing : MonoBehaviour
{
    public void ChooseMap(string mapType)
    {
        switch (mapType)
        {
            case "Forest" :
                MapSettings.mapType = MapSettings.MapType.Forest;
                break;
            case "Game Scene" :
                MapSettings.mapType = MapSettings.MapType.Forest;
                break;
            case "Water":
                MapSettings.mapType = MapSettings.MapType.Water;
                break;
            case "Sky":
                MapSettings.mapType = MapSettings.MapType.Sky;
                break;
                
        }
        SceneManager.LoadScene("GameScene");
    }
}

public static class MapSettings
{
    public enum MapType
    {
        Forest,
        Water,
        Sky
    }

    public static MapType mapType;
}
