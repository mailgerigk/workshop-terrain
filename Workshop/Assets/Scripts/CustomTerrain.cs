using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTerrain : MonoBehaviour
{

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public TerrainGenerator terrainGenerator;

    public void GenerateAndApplyMesh()
    {
        Mesh mesh = terrainGenerator.GenerateMesh();
        meshFilter.mesh = mesh;

        float[,] heightmap = terrainGenerator.heightmapGenerator.GenerateNoisemap();
        Texture2D heightmapTexture = Texture2DUtility.FromNoisemap(heightmap);
        Texture2D normalmapTexture = Normalmap.getNormalMap(heightmapTexture, 2);

        meshRenderer.sharedMaterial.EnableKeyword("_NORMALMAP");
        meshRenderer.sharedMaterial.EnableKeyword("_PARALLAXMAP");

        meshRenderer.sharedMaterial.SetTexture("_BumpMap", normalmapTexture);
        meshRenderer.sharedMaterial.SetTexture("_ParallaxMap", heightmapTexture);
    }
}
