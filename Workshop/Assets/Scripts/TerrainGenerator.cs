using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public enum TerrainMode {
        Point,
        Cube,
    }

    public NoisemapGenerator heightmapGenerator;
    public NoisemapGenerator moisturemapGenerator;
    public Texture2D biomemap;
    public TerrainMode worldMode;
    public Vector3 scale = Vector3.one;

    public Mesh GenerateMesh() {
        float[,] heightmap = heightmapGenerator.GenerateNoisemap();
        float[,] moisturemap = moisturemapGenerator.GenerateNoisemap();
        return PointWorld.Generate(heightmap, moisturemap, biomemap, scale);
    }
}
