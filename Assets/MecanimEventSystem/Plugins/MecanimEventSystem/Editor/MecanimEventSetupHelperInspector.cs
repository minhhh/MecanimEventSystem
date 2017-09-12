using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;
using System.Linq;

[CustomEditor (typeof(MecanimEventSetupHelper))]
public class MecanimEventSetupHelperInspector : Editor
{
    SerializedProperty dataSource;
    SerializedProperty dataSources;

    void OnEnable ()
    {
        dataSource = serializedObject.FindProperty ("dataSource");
        dataSources = serializedObject.FindProperty ("dataSources");
    }

    public override void OnInspectorGUI ()
    {
        MecanimEventSetupHelper component = (MecanimEventSetupHelper)target;
        serializedObject.UpdateIfDirtyOrScript ();

        DrawDefaultInspector ();

        if (GUILayout.Button ("Fill Data Sources")) {
            component.dataSources = 
                ListUpAllObjectsByType ("Assets", "Prefab", ".prefab")
                    .Select (x => x.GetComponent <MecanimEventData> ())
                    .Where (x => x != null)
                    .ToArray ();
        }

        serializedObject.ApplyModifiedProperties ();
    }

    private static List<GameObject> ListUpAllObjectsByType (string directory, string assetType, string assetFileExtension)
    {
        string assetSearchCond = "t:" + assetType + " ";
        string[] assetPaths = new string[1] {
            directory,
        };

        string[] guids = AssetDatabase.FindAssets (assetSearchCond, assetPaths);

        List<GameObject> objs = new List<GameObject> ();

        foreach (string guid in guids) {
            string assetPath = AssetDatabase.GUIDToAssetPath (guid);
            if (!assetPath.EndsWith (assetFileExtension)) {
                continue;
            }
            objs.Add (AssetDatabase.LoadAssetAtPath <GameObject> (assetPath));
        }

        return objs;
    }

}