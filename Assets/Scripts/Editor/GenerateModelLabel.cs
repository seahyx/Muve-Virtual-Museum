using UnityEditor;
using UnityEngine;


public class GenerateModelLabel
{
    [MenuItem("Museum Utilities/Generate Model Labels")]
    static void GenerateModelLabels()
    {
        TextAsset artefactDesc = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Models/Artifacts/ArtefactDescription.csv");
        Debug.Log(artefactDesc);
    }
}
