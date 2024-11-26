using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ExhibitDetails", menuName = "Muve/Exhibit Details", order = 1)]
public class ExhibitDetails : ScriptableObject
{
    public string Title;
	[TextArea(3, 10)]
	public string Description;
	public Sprite Image;
	public SceneAsset Scene;
}
