using UnityEngine;

[CreateAssetMenu(fileName = "artefact", menuName = "Muve/Artefact Description")]
public class Artefact : ScriptableObject
{
    public string Title;
    public string Year;
    public string Origin;
    public string Material;
    [TextArea(3, 15)]
    public string Description;
    [TextArea(3, 5)]
    public string MiniGameDescription;
    public Sprite MiniGameImage;
    public AudioClip Narration;
}
