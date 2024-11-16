using System.IO;
using UnityEditor;
using UnityEngine;

public class ReplaceModelsWithAssets
{
	[MenuItem("Museum Utilities/Replace Models with Prefabs")]
	static void ReplaceModels()
	{
		string prefabPath = "Assets/BrokenVector/LowPolyDungeon/Prefabs";
		if (!Directory.Exists(prefabPath))
		{
			Debug.LogError("LowPolyDungeon prefabs folder not found. Make sure the LowPolyDungeon package is installed.");
			return;
		}

		GameObject[] sceneObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject selectedObject in sceneObjects)
		{
			if (!PrefabUtility.IsPartOfModelPrefab(selectedObject))
				continue;

			string modelName = selectedObject.name;
			if (modelName.Contains(" ("))
			{
				modelName = modelName.Split(" (")[0];
			}
			string[] prefabGUIDs = AssetDatabase.FindAssets($"t:prefab {modelName}", new string[] { prefabPath });
			bool hasPrefab = false;

			foreach (string prefabGUID in prefabGUIDs)
			{
				GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(prefabGUID));
				if (prefabAsset.name.ToLower() == modelName.ToLower())
				{
					GameObject newObject = PrefabUtility.InstantiatePrefab(prefabAsset) as GameObject;
					newObject.transform.position = selectedObject.transform.position;
					newObject.transform.rotation = selectedObject.transform.rotation;
					newObject.transform.localScale = selectedObject.transform.localScale;
					newObject.transform.parent = selectedObject.transform.parent;
					Undo.DestroyObjectImmediate(selectedObject);
					hasPrefab = true;
					break;
				}
			}
			if (!hasPrefab)
				Debug.LogWarning($"Prefab not found for model {selectedObject.name}");
		}
	}
}
