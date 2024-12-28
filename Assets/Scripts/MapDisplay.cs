using UnityEngine;
// tranform a noiseMap into a texture to apply on a plane in a scene
public class MapDisplay : MonoBehaviour {
    
    public Renderer textureRender;

    public void DrawTexture(Texture2D texture) { 
        textureRender.sharedMaterial.mainTexture = texture; // apply the perlin noise to the texture object applied to the plane
        textureRender.transform.localScale = new Vector3 (texture.width, 1 , texture.height);
    }

}
