using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;
    public void GenerateMap(){
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        MapDisplay display = FindAnyObjectByType<MapDisplay>(); // find the plane wich has the MapDisplay type to apply the texture
        display.DrawNoiseMap(noiseMap);
    }

}
