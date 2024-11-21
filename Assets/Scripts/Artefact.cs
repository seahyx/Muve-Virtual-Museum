using UnityEngine;

[CreateAssetMenu(fileName = "artefact", menuName = "Muve/Artefact Description")]
public class Artefact : ScriptableObject
{
    [Multiline(2)]
    public string Title;
    public string Year;
    public string Origin;
    public string Material;
    [Multiline(5)]
    public string Description;
    [Multiline(3)]
    public string MiniGameDescription;
}
