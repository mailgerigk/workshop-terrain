using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Normalmap {

    // https://forum.unity.com/threads/normal-map-from-height-map-on-runtime.259273/
    public static Texture2D getNormalMap(Texture2D texture, float str = 2.0f)
    {
        Texture2D normal = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        for (int x = 1; x < texture.width - 1; x++)
            for (int y = 1; y < texture.height - 1; y++)
            {
                //using Sobel operator
                float tl, t, tr, l, right, bl, bot, br;
                tl = intensity(texture.GetPixel(x - 1, y - 1).r, texture.GetPixel(x - 1, y - 1).g, texture.GetPixel(x - 1, y - 1).b);
                t = intensity(texture.GetPixel(x - 1, y).r, texture.GetPixel(x - 1, y).g, texture.GetPixel(x - 1, y).b);
                tr = intensity(texture.GetPixel(x - 1, y + 1).r, texture.GetPixel(x - 1, y + 1).g, texture.GetPixel(x - 1, y + 1).b);
                right = intensity(texture.GetPixel(x, y + 1).r, texture.GetPixel(x, y + 1).g, texture.GetPixel(x, y + 1).b);
                br = intensity(texture.GetPixel(x + 1, y + 1).r, texture.GetPixel(x + 1, y + 1).g, texture.GetPixel(x + 1, y + 1).b);
                bot = intensity(texture.GetPixel(x + 1, y).r, texture.GetPixel(x + 1, y).g, texture.GetPixel(x + 1, y).b);
                bl = intensity(texture.GetPixel(x + 1, y - 1).r, texture.GetPixel(x + 1, y - 1).g, texture.GetPixel(x + 1, y - 1).b);
                l = intensity(texture.GetPixel(x, y - 1).r, texture.GetPixel(x, y - 1).g, texture.GetPixel(x, y - 1).b);

                //Sobel filter
                float dX = (tr + 2.0f * right + br) - (tl + 2.0f * l + bl);
                float dY = (bl + 2.0f * bot + br) - (tl + 2.0f * t + tr);
                float dZ = 1.0f / str;

                Vector3 vc = new Vector3(dX, dY, dZ);
                vc.Normalize();
                //Debug.Log(vc.x + " " + vc.y + " " + vc.z);
                normal.SetPixel(x, y, new Color((vc.y + 1f) / 2f, (vc.y + 1f) / 2f, (vc.y + 1f) / 2f, (vc.x + 1f) / 2f));
            }
        normal.Apply();
        return normal;
    }

    public static float intensity(float r, float g, float b)
    {
        return (r + g + b) / 3.0f;
    }
}
