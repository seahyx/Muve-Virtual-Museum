using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MuseumLabel : MonoBehaviour
{
    [SerializeField, Tooltip("Artefact description asset.")]
    public Artefact artefactDescription;

	[SerializeField, Tooltip("Artefact Grab Interactable.")]
	public XRGrabInteractable artefactInteractable;

    [SerializeField, Tooltip("Label text component.")]
    private TextMeshProUGUI labelTMP;

    private void Start()
    {
        UpdateDescription();
	}

	public void UpdateDescription()
    {
		if (artefactDescription == null)
		{
			Debug.LogWarning("artefactDescription is empty");
			return;
		}

		if (labelTMP == null)
		{
			Debug.LogWarning("labelTMP is empty");
			return;
		}

		labelTMP.text = GetFormattedText();
	}

	public string GetFormattedText()
	{
		return $"<b>{artefactDescription.Origin}</b>\r\n" +
			$"<size=5> </size>\r\n" +
			$"<b><i>{artefactDescription.Title}</i></b>\r\n" +
			$"<size=5> </size>\r\n" +
			$"{artefactDescription.Year}\r\n" +
			$"<size=5> </size>\r\n" +
			$"{artefactDescription.Material}\r\n" +
			$"<size=11>\r\n" +
			$"{artefactDescription.Description}</size>";
	}

	public void PlayNarration()
	{
		if (artefactDescription == null)
		{
			Debug.LogWarning("artefactDescription is empty");
			return;
		}
		if (artefactDescription.Narration == null)
		{
			Debug.LogWarning("Narration is empty");
			return;
		}
		NarrationManager.Instance.PlayNarration(artefactDescription.Narration, artefactDescription.Title);
	}
}
