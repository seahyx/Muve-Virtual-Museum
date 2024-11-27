using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MuseumLabel)), CanEditMultipleObjects]
public class MuseumLabelInspector : Editor
{
	public override VisualElement CreateInspectorGUI()
	{
		VisualElement root = new();

		InspectorElement.FillDefaultInspector(root, serializedObject, this);

		// Get the properties
		SerializedProperty artefactDescription = serializedObject.FindProperty("artefactDescription");
		SerializedProperty labelTMP = serializedObject.FindProperty("labelTMP");

		// Add a button to update the description
		Button updateButton = new Button(() =>
		{
			foreach (MuseumLabel target in targets)
			{
				if (artefactDescription.objectReferenceValue == null || labelTMP.objectReferenceValue == null)
					continue;

				TextMeshProUGUI label = (TextMeshProUGUI)labelTMP.objectReferenceValue;
				Undo.RecordObject(label, "Update Description");
				label.text = target.GetFormattedText();
			}
		})
		{
			text = "Update Description"
		};
		updateButton.SetEnabled(artefactDescription.objectReferenceValue != null && labelTMP.objectReferenceValue != null);
		root.RegisterCallback<ChangeEvent<Object>>(evt =>
		{
			updateButton.SetEnabled(artefactDescription.objectReferenceValue != null && labelTMP.objectReferenceValue != null);
		});

		root.Add(updateButton);

		return root;
	}
}
