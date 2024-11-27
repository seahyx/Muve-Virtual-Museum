using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPageManager : MonoBehaviour
{
    [SerializeField, Tooltip("Tutorial pages to display in order.")]
    private List<GameObject> tutorialPages = new List<GameObject>();

	[SerializeField, Tooltip("Event invoked when the tutorial is complete.")]
	private UnityEvent OnTutorialComplete = new UnityEvent();

	private int _currentPage = 0;

	public int CurrentPage
    {
        get { return _currentPage; }
        set
		{
			if (_currentPage == value || value < 0 || value >= tutorialPages.Count)
				return;
			_currentPage = value;
			foreach (GameObject page in tutorialPages)
			{
				page.SetActive(false);
			}
			tutorialPages[_currentPage].SetActive(true);
		}
    }

	public void NextPage()
	{
		CurrentPage++;
		if (CurrentPage == tutorialPages.Count - 1)
			EndTutorial();
	}

	public void PreviousPage() {
		CurrentPage--;
	}

	public void EndTutorial()
	{
		OnTutorialComplete.Invoke();
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		_currentPage = 0;
		foreach (GameObject page in tutorialPages)
		{
			page.SetActive(false);
		}
		tutorialPages[_currentPage].SetActive(true);
	}
}
