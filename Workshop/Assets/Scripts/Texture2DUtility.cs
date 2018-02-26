using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Texture2DUtility {
    public static Texture2D FromNoisemap(float[,] noisemap)
    {
        int width = noisemap.GetLength(0);
        int height = noisemap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] colors = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + y * width;
                float heightValue = noisemap[x, y];
                colors[index] = new Color(heightValue, heightValue, heightValue);
            }
        }

        texture.SetPixels(colors);
        texture.Apply();

        return texture;
    }
}
