using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

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

        Color[] colourMap = new Color[mapWidth * mapHeight];

        for(int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++){
                float currentHeight = noiseMap[y, x];
                for (int i = 0; i < regions.Length; i++){
                    if(currentHeight <= regions[i].height){
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindAnyObjectByType<MapDisplay>(); // find the plane wich has the MapDisplay type to apply the texture
        if (drawMode == DrawMode.NoiseMap){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColourMap) {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        } else if (drawMode == DrawMode.Mesh){
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
            
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
