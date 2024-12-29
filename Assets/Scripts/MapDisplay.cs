using UnityEngine;
// tranform a noiseMap into a texture to apply on a plane in a scene
public class MapDisplay : MonoBehaviour {
    
    public Renderer textureRender;
    public MeshFilter meshfilter;
    public MeshRenderer meshRenderer; 

    public void DrawTexture(Texture2D texture) { 
        textureRender.sharedMaterial.mainTexture = texture; // apply the perlin noise to the texture object applied to the plane
        textureRender.transform.localScale = new Vector3 (texture.width, 1 , texture.height);
    }
    public void DrawMesh(MeshData meshdata, Texture2D texture){
        meshfilter.sharedMesh = meshdata.CreateMesh(); // mesh must be shared to be able to be generated outside of game mode
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
