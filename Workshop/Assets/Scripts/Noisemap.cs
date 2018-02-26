using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noisemap {
    public static float[,] Generate(int width, int height, int seed, int octaves, float zoom, float persistance, float lacunarity, Vector2 offset)
    {

        float[,] heightmap = new float[width, height];

        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;

        float amplitude = 1.0f;
        float frequency = 1.0f;

        float minAmplitude = float.MaxValue;
        float maxAmplitude = float.MinValue;

        float[] offsetsX = new float[octaves];
        float[] offsetsY = new float[octaves];

        System.Random random = new System.Random(seed);

        // setup values
        for (int i = 0; i < octaves; i++)
        {
            offsetsX[i] = random.Next(short.MinValue, short.MaxValue) + offset.x;
            offsetsY[i] = random.Next(short.MinValue, short.MaxValue) + offset.y;
        }

        // sample noise values
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                amplitude = 1.0f;
                frequency = 1.0f;

                float heightValue = 0.0f;

                for (int i = 0; i < octaves; i++)
                {
                    float posX = (x - halfWidth) / zoom * frequency + offsetsX[i];
                    float posY = (y - halfHeight) / zoom * frequency + offsetsY[i];

                    float perlinValue = Mathf.PerlinNoise(posX, posY) * 2.0f - 1.0f;
                    heightValue += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                heightmap[x, y] = heightValue;

                minAmplitude = Mathf.Min(minAmplitude, heightValue);
                maxAmplitude = Mathf.Max(maxAmplitude, heightValue);
            }
        }

        // normalize values
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                heightmap[x, y] = Mathf.InverseLerp(minAmplitude, maxAmplitude, heightmap[x, y]);
            }
        }


        return heightmap;
    }
}
