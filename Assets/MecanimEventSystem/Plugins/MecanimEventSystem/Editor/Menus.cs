using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class Menus
{
    [MenuItem ("Assets/Create/MecanimEventData")]
    public static void CreateMEDPrefab ()
    {
        var assetPath = Path.Combine (Path.GetDirectoryName (AssetDatabase.GetAssetPath (Selection.activeObject)), Selection.activeObject.name + "_MED.prefab");
        Object prefab = EditorUtility.CreateEmptyPrefab (assetPath);
        var go = new GameObject ();
        go.AddComponent <MecanimEventData> ();
        EditorUtility.ReplacePrefab (go, prefab, ReplacePrefabOptions.ConnectToPrefab);
        AssetDatabase.SaveAssets ();
        GameObject.DestroyImmediate (go);

        Object asset = AssetDatabase.LoadAssetAtPath <Object> (assetPath);
        Selection.activeObject = asset;
    }

    [MenuItem ("Assets/Create/MecanimEventData", true)]
    public static bool ValidateCreateMEDPrefab ()
    {
        return Selection.activeObject is RuntimeAnimatorController;
    }
}
