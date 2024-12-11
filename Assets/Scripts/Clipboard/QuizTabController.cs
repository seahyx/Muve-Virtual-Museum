using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTabController : MonoBehaviour
{
    [SerializeField]
    private GameObject instructionPage;
	[SerializeField]
	private GameObject quizPage;
	[SerializeField]
	private GameObject resultPage;

	public void SwitchToIntructionPage()
	{
		quizPage.SetActive(false);
		resultPage.SetActive(false);
		instructionPage.SetActive(true);
	}

	public void SwitchToQuizPage()
	{
		instructionPage.SetActive(false);
		resultPage.SetActive(false);
		quizPage.SetActive(true);
	}

	public void SwitchToResultPage()
	{
		instructionPage.SetActive(false);
		quizPage.SetActive(false);
		resultPage.SetActive(true);
	}
}
