using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class GenerateModelLabel
{
	struct ArtefactDescription
	{
		public string title;
		public string year;
		public string origin;
		public string material;
		public string description;
		public string miniDescription;
		public string imported;
		public string prefabName;
	}

	const string CSV_NAME = "ArtefactDescription.csv";
	const string DESCRIPTION_ASSET_PATH = "Assets/Prefabs/Artefacts/Descriptions/";
	const string CSV_PATH = DESCRIPTION_ASSET_PATH + CSV_NAME;

	[MenuItem("Museum Utilities/Update and Generate Model Labels")]
	static void GenerateModelLabels()
	{
		TextAsset artefactDesc = AssetDatabase.LoadAssetAtPath<TextAsset>(CSV_PATH);
		string fullText = artefactDesc.text.Trim();

		string pattern = @"(?:,|\n|^)(""(?:(?:"""")*[^""]*)*""|[^"",\n]*|(?:\n|$))";
		Regex regex = new Regex(pattern);

		// Match the rest of the rows (other than the first header row)
		MatchCollection matches = regex.Matches(fullText[(fullText.IndexOf("\n") + 1)..]);

		List<ArtefactDescription> artefacts = new();
		ArtefactDescription currentDesc = new();
		for (int i = 0; i < matches.Count; i++)
		{
			string value = matches[i].Groups[1].Value.Trim();
			if (value.StartsWith("\"") && value.EndsWith("\""))
			{
				value = value[1..^1];
			}
			switch (i % 8)
			{
				case 0:
					currentDesc.title = value;
					break;
				case 1:
					currentDesc.year = value;
					break;
				case 2:
					currentDesc.origin = value;
					break;
				case 3:
					currentDesc.material = value;
					break;
				case 4:
					currentDesc.description = value;
					break;
				case 5:
					currentDesc.miniDescription = value;
					break;
				case 6:
					currentDesc.imported = value;
					break;
				case 7:
					currentDesc.prefabName = value;
					artefacts.Add(currentDesc);
					currentDesc = new();
					break;
			}
		}

		// Debug log the titles of all the artefacts
		StringBuilder sb = new();
		sb.AppendLine("Total number of artefacts: " + artefacts.Count);
		foreach (ArtefactDescription desc in artefacts)
		{
			sb.AppendLine(desc.title);
		}
		Debug.Log(sb.ToString());

		// Create the artefact description assets
		foreach (ArtefactDescription desc in artefacts)
		{
			bool hasExisting = true;

			Artefact asset = AssetDatabase.LoadAssetAtPath<Artefact>(DESCRIPTION_ASSET_PATH + desc.prefabName + ".asset");
			if (asset == null)
			{
				hasExisting = false;
				asset = ScriptableObject.CreateInstance<Artefact>();
			}

			asset.Title = desc.title;
			asset.Year = desc.year;
			asset.Origin = desc.origin;
			asset.Material = desc.material;
			asset.Description = desc.description;
			asset.MiniGameDescription = desc.miniDescription;

			if (!hasExisting)
				AssetDatabase.CreateAsset(asset, DESCRIPTION_ASSET_PATH + desc.prefabName + ".asset");
		}
	}
}
