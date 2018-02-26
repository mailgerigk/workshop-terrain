using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointWorld {
	public static Mesh Generate(float[,] heightmap, float[,] moisturemap, Texture2D biomemap, Vector3 scale) {

        int width = heightmap.GetLength(0);
        int height = heightmap.GetLength(1);
        Vector3[] vertices = new Vector3[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + y * width;
                vertices[index] =
                    Vector3.right * x * scale.x +
                    Vector3.up * heightmap[x, y] * scale.y +
                    Vector3.forward * y * scale.z;
            }
        }

        int i = 0;
        int[] indices = new int[6 * width * height];
        for (int y = 1; y < height; y++)
        {
            for (int x = 1; x < width; x++)
            {
                int topLeft = (x - 1) + (y - 1) * width;
                int topRight = topLeft + 1;
                int bottomLeft = topLeft + width;
                int bottomRight = topRight + width;

                indices[i++] = bottomLeft;
                indices[i++] = topRight;
                indices[i++] = topLeft;

                indices[i++] = bottomLeft;
                indices[i++] = bottomRight;
                indices[i++] = topRight;
            }
        }

        Color[] colors = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + y * width;
                float elevation = heightmap[x, y];
                float moisture = moisturemap[x, y];

                int posX = (int)(moisture * biomemap.width);
                int posY = (int)(elevation * biomemap.height);

                colors[index] = biomemap.GetPixel(posX, posY);
            }
        }

        Vector2[] uvs = new Vector2[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = x + y * width;
                float elevation = heightmap[x, y];
                float moisture = moisturemap[x, y];

                uvs[index] = new Vector2(moisture, elevation);
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        mesh.colors = colors;
        mesh.SetUVs(0, new List<Vector2>(uvs));

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}
