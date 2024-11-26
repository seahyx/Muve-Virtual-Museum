using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuDetailsPanel : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> noneSelectedUI = new List<GameObject>();
	[SerializeField]
	private List<GameObject> detailsUI = new List<GameObject>();
	[SerializeField]
	private TextMeshProUGUI exhibitTitle;
	[SerializeField]
	private TextMeshProUGUI exhibitDescription;
	[SerializeField]
	private Image exhibitImage;
	[SerializeField]
	private Button startButton;

	private ExhibitDetails currentlySelectedExhibit;

	private void Start()
	{
		foreach (var ui in detailsUI)
		{
			ui.SetActive(false);
		}

		foreach (var ui in noneSelectedUI)
		{
			ui.SetActive(true);
		}

		startButton.onClick.AddListener(() =>
		{
			if (currentlySelectedExhibit != null)
			{
				SceneManager.LoadSceneAsync(currentlySelectedExhibit.Scene.name);
			}
		});
	}

	private void UpdateDetails(ExhibitDetails exhibitDetails)
	{
		exhibitTitle.text = exhibitDetails.Title;
		exhibitDescription.text = exhibitDetails.Description;
		exhibitImage.sprite = exhibitDetails.Image;
	}

	public void OnExhibitSelected(ExhibitDetails exhibitDetails)
	{
		currentlySelectedExhibit = exhibitDetails;
		UpdateDetails(exhibitDetails);

		foreach (var ui in noneSelectedUI)
		{
			ui.SetActive(false);
		}

		foreach (var ui in detailsUI)
		{
			ui.SetActive(true);
		}
	}
}
