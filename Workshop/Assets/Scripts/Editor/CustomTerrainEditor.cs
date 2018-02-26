using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CustomTerrain))]
public class CustomTerrainEditor : Editor {
    bool autoUpdate = false;
    public override void OnInspectorGUI()
    {
        bool changed = DrawDefaultInspector();
        autoUpdate = EditorGUILayout.Toggle("Auto Update", autoUpdate);
        if ((changed && autoUpdate) || GUILayout.Button("Generate"))
        {
            CustomTerrain terrain = (CustomTerrain)target;
            terrain.GenerateAndApplyMesh();
        }
    }
}
