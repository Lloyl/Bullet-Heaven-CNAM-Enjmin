using UnityEngine;

public  static class MeshGenerator {
 
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier){
        int width   = heightMap.GetLength(0);
        int height  = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){

                meshData.verticies[vertexIndex] = new Vector3(topLeftX + x, heightMap[x,y] * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x/(float)width, y /(float)height);

                if(x < width -1 && y < height - 1){
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width); // each triangle is stored in a 1D array, for each vertexIndex the other vertexIndex of the triangle are the argument of the function
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1); //the other triangle of the square 
                }

                vertexIndex++;

            }
        }

        return meshData;
    }
}

public class MeshData{
    public Vector3[]    verticies;
    public int[]        triangles;
    public Vector2[]    uvs;
    int trianglesIndex;

    public MeshData(int meshWidth, int meshHeight){
        verticies = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth-1) * (meshHeight - 1) * 2 * 3]; // each square contains 2 triangles wich have 3 verticies 
        uvs = new Vector2[meshWidth * meshHeight]; // use to locate the triangle on the map using percentil of x and y coordinate -> 0%, 0% top left corner | 100%, 100% bottom right corner

    }

    public void AddTriangle(int a, int b, int c){
        triangles[trianglesIndex] = a;
        triangles[trianglesIndex + 1] = b;
        triangles[trianglesIndex + 2 ] = c;
        trianglesIndex += 3;
    }

    public Mesh CreateMesh(){
        Mesh mesh = new Mesh();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }

}