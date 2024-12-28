using UnityEngine;
// tranform a noiseMap into a texture to apply on a plane in a scene
public class MapDisplay : MonoBehaviour {
    
    public Renderer textureRender;
    public void DrawNoiseMap(float[,] noiseMap){
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        
        Color[] colourMap = new Color[width * height];
        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]); // create grey scale value to each pixel
            }
        }

        texture.SetPixels (colourMap); // apply grey scale values to the texture rendered
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture; // apply the perlin noise to the texture object applied to the plane
        textureRender.transform.localScale = new Vector3 (width, 1 , height);
    }

}
