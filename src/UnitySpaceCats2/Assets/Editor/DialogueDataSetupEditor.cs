using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueDataSetup))]
public class DialogueDataSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        DialogueDataSetup setup = (DialogueDataSetup)target;
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("Setup All Dialogue", GUILayout.Height(30)))
        {
            setup.SetupAllDialogue();
            EditorUtility.SetDirty(setup.path1Data);
            EditorUtility.SetDirty(setup.path2Data);
            EditorUtility.SetDirty(setup.path3Data);
            EditorUtility.SetDirty(setup.path4Data);
            AssetDatabase.SaveAssets();
        }
        
        GUILayout.Space(5);
        
        if (GUILayout.Button("Setup Path 1"))
        {
            setup.SetupPath1Dialogue();
            EditorUtility.SetDirty(setup.path1Data);
            AssetDatabase.SaveAssets();
        }
        
        if (GUILayout.Button("Setup Path 2"))
        {
            setup.SetupPath2Dialogue();
            EditorUtility.SetDirty(setup.path2Data);
            AssetDatabase.SaveAssets();
        }
        
        if (GUILayout.Button("Setup Path 3"))
        {
            setup.SetupPath3Dialogue();
            EditorUtility.SetDirty(setup.path3Data);
            AssetDatabase.SaveAssets();
        }
        
        if (GUILayout.Button("Setup Path 4"))
        {
            setup.SetupPath4Dialogue();
            EditorUtility.SetDirty(setup.path4Data);
            AssetDatabase.SaveAssets();
        }
    }
}