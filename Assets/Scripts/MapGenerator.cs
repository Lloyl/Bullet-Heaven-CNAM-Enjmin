using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions ;
    public void GenerateMap(){
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindAnyObjectByType<MapDisplay>(); // find the plane wich has the MapDisplay type to apply the texture
        display.DrawNoiseMap(noiseMap);
    }

    private void OnValidate(){ // called automatically when values are updated
        if (mapWidth < 1){
            mapWidth = 1; 
        }
        if (mapHeight < 1){
            mapHeight = 1;
        }
        if (lacunarity < 1){
            lacunarity = 1;
        }
        if (octaves < 0){
            octaves = 0;
        }
    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
