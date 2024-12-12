using UnityEngine;

public class ResetAllArtefacts : MonoBehaviour
{
    public void ResetEverything()
    {
		MuseumLabel[] museumLabels = Object.FindObjectsByType<MuseumLabel>(FindObjectsSortMode.None);
		foreach (MuseumLabel museumLabel in museumLabels)
		{
			if (museumLabel.artefactInteractable.TryGetComponent(out FallingNet fallingNet))
				fallingNet.Respawn();
		}
		Debug.Log($"Reset all {museumLabels.Length} artefacts to their original transforms.");
	}
}
