using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

    public TerrainGenerator terrainGenerator;
    public GameObject treePrefab;
    public Texture2D treemap;
    public int treeCount;
    public int seed;

    public void PlaceTrees()
    {
        DestroyTrees();

        System.Random random = new System.Random(seed);
        float[,] heightmap = terrainGenerator.heightmapGenerator.GenerateNoisemap();
        float[,] moisturemap = terrainGenerator.moisturemapGenerator.GenerateNoisemap();

        int width = heightmap.GetLength(0);
        int height = heightmap.GetLength(1);

        List<Vector3> trees = new List<Vector3>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float elevation = heightmap[x, y];
                float moisture = moisturemap[x, y];

                int posX = (int)(moisture * treemap.width);
                int posY = (int)(elevation * treemap.height);

                float colorValue = treemap.GetPixel(posX, posY).r;
                if(colorValue < random.NextDouble())
                {
                    Vector3 position =
                        Vector3.right * x * terrainGenerator.scale.x +
                        Vector3.up * heightmap[x, y] * terrainGenerator.scale.y +
                        Vector3.forward * y * terrainGenerator.scale.z;

                    trees.Add(position);
                }
            }
        }

        for (int i = 0; i < treeCount; i++)
        {
            int index = random.Next(0, trees.Count);
            Vector3 position = trees[index];
            trees.RemoveAt(index);
            Instantiate(treePrefab, position, Quaternion.AngleAxis((float)random.NextDouble() * 360, Vector3.up), transform);
        }
    }

    public void DestroyTrees()
    {
        while(transform.childCount > 0)
        { 
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject);
        }
    }
}
