using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeSpawner))]
public class TreeSpawnerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TreeSpawner treeSpawner = (TreeSpawner)target;

        if (GUILayout.Button("Place Trees"))
        {
            treeSpawner.PlaceTrees();
        }

        if (GUILayout.Button("Destroy Trees"))
        {
            treeSpawner.DestroyTrees();
        }
    }
}
