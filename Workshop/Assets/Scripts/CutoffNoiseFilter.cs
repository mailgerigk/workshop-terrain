using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoffNoiseFilter : NoisemapGenerator {

    public float minValue;
    public float maxValue;

    public override float[,] GenerateNoisemap()
    {
        float[,] noisemap = base.GenerateNoisemap();

        for (int y = 0; y < noisemap.GetLength(1); y++)
        {
            for (int x = 0; x < noisemap.GetLength(0); x++)
            {
                noisemap[x, y] = Mathf.Clamp(noisemap[x, y], minValue, maxValue);
            }
        }

        return noisemap;
    }
}
