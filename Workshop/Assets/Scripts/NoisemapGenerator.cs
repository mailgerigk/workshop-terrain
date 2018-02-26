using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisemapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int seed;
    public int octaves;
    public float zoom;
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    public virtual float[,] GenerateNoisemap()
    {
        return Noisemap.Generate(width, height, seed, octaves, zoom, persistance, lacunarity, offset);
    }
}
