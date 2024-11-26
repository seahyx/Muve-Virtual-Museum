using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MainMenuExhibitItem : MonoBehaviour
{
	[SerializeField]
	private ExhibitDetails exhibitDetails;
	[SerializeField]
	private MainMenuDetailsPanel detailsPanel;
	[SerializeField]
	private TextMeshProUGUI exhibitTitle;
	[SerializeField]
	private Image exhibitImage;

	private Toggle toggle;

	private void Start()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(OnToggleValueChanged);

		exhibitTitle.text = exhibitDetails.Title;
		exhibitImage.sprite = exhibitDetails.Image;
	}

	private void OnToggleValueChanged(bool isOn)
	{
		if (isOn)
		{
			detailsPanel.OnExhibitSelected(exhibitDetails);
		}
	}
}
