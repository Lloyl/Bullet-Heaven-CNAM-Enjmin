using UnityEngine;

public static class PerlinNoise {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed , float scale, int octaves, float persistance, float lacunarity, Vector2 offset){
        float[,] noiseMap = new float[mapWidth, mapHeight];

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        System.Random prng      = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float halfWidth = mapWidth / 2f ;
        float halfHeight = mapHeight / 2f;
        
        for (int i = 0; i < octaves; i++){
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y ;// magic values found by trials and errors

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if ( scale <= 0){
            scale = 0.0001f;
        }

        for(int y = 0; y < mapHeight; y++){
            for(int x = 0; x < mapWidth; x++){

                float amplitude     = 1 ;
                float frequency     = 1 ;
                float noiseHeight   = 0 ;


                for(int i = 0; i< octaves; i++)
                {
                    float sampleX = (x - halfWidth )/ scale * frequency + octaveOffsets[i].x; // the higher the frequency, the further apart sample point will be thus increasing the speed at wich value lerp
                    float sampleY = (y - halfHeight)/ scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // PerlinNoise return a value between 0 and 1, we want a value between -1 and 1
                    noiseHeight += perlinValue * amplitude;
                    

                    amplitude *= persistance ;
                    frequency *= lacunarity;
                }


                if(noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                } else if(noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight; 
                }


                noiseMap[y, x] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++){
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); // normalized noiseMap values between 0 and 1

            }
        }

        return noiseMap;
    }
}
