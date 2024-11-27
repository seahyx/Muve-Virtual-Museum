using TMPro;
using UnityEngine;

public class MuseumLabel : MonoBehaviour
{
    [SerializeField, Tooltip("Artefact description asset.")]
    private Artefact artefactDescription;

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
}
