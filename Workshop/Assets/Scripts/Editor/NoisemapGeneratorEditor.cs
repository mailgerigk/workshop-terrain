using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(NoisemapGenerator))]
public class NoisemapGeneratorEditor : Editor {
    bool autoUpdate = true;
    Texture2D texture = null;
    public override void OnInspectorGUI()
    {
        bool changed = DrawDefaultInspector();
        autoUpdate = EditorGUILayout.Toggle("Auto Update", autoUpdate);
        if((changed && autoUpdate) || GUILayout.Button("Generate"))
        {
            NoisemapGenerator generator = (NoisemapGenerator)target;
            float[,] heightmap = generator.GenerateNoisemap();
            texture = Texture2DUtility.FromNoisemap(heightmap);
        }

        if(texture != null && GUILayout.Button("Save as png"))
        {
            File.WriteAllBytes("Assets/texture.png", texture.EncodeToPNG());
        }

        if(texture != null)
        {
            GUILayout.Box(texture);
        }
    }
}
