using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MiniGameManager;

public class ClipboardQuizController : MonoBehaviour
{
	[SerializeField, Tooltip("Minigame manager.")]
	private MiniGameManager manager;

	[SerializeField, Tooltip("Quiz list content.")]
	private GameObject quizListContent;
	[SerializeField, Tooltip("Score text.")]
	private TextMeshProUGUI scoreTMP;
	[SerializeField, Tooltip("Result text.")]
	private TextMeshProUGUI resultTMP;
	[SerializeField, Tooltip("Quiz tab controller.")]
	private QuizTabController quizTabController;

	[SerializeField, Tooltip("Quiz row prefab.")]
	private QuizRow quizRowPrefab;
	[SerializeField, Tooltip("Quiz row image prefab.")]
	private QuizRow quizRowImagePrefab;
	[SerializeField, Tooltip("Quiz row completed prefab.")]
	private QuizRow quizRowCompletedPrefab;

	[SerializeField]
	private AudioClip correctSound;
	[SerializeField]
	private AudioClip completeSound;

	private List<QuizRow> completedQuizRows = new();
	private QuizRow currentQuizRow;
	private AudioSource audioSource;

	private void Start()
	{
		manager.OnNextQuestion.AddListener(OnNextQuestion);
		manager.OnQuizCompleted.AddListener(OnQuizCompleted);
		audioSource = GetComponent<AudioSource>();
	}

	private void ClearList()
	{
		foreach (QuizRow row in completedQuizRows)
		{
			Destroy(row.gameObject);
		}
		completedQuizRows.Clear();
		if (currentQuizRow != null)
			Destroy(currentQuizRow.gameObject);
		currentQuizRow = null;
	}

	public void OnNextQuestion()
	{
		// Update the score
		scoreTMP.text = $"Score: {manager.totalScore}";

		// Get the current question
		Question question = manager.currentQuestion;
		if (question == null)
			return;

		// Previous question is complete, remove it and add to the completed rows
		if (currentQuizRow != null)
		{
			QuizRow completedRow = Instantiate(quizRowCompletedPrefab, quizListContent.transform);
			completedRow.Question = currentQuizRow.Question;
			completedRow.transform.SetSiblingIndex(1);
			completedQuizRows.Add(completedRow);
			Destroy(currentQuizRow.gameObject);

			// Play audio
			audioSource.PlayOneShot(correctSound);
		}

		// Create a new quiz row
		QuizRow quizRow;
		if (question.ClueImage == null)
		{
			quizRow = Instantiate(quizRowPrefab, quizListContent.transform);
			quizRow.Question = question;
		}
		else
		{
			quizRow = Instantiate(quizRowImagePrefab, quizListContent.transform);
			quizRow.Question = question;
		}
		quizRow.transform.SetAsFirstSibling();
		currentQuizRow = quizRow;
	}

	public void StartQuiz()
	{
		ClearList();
		manager.StartQuiz();
	}

	public void StopQuiz()
	{
		ClearList();
		manager.StopQuiz();
	}

	private void OnQuizCompleted()
	{
		// Play audio
		audioSource.PlayOneShot(completeSound);

		quizTabController.SwitchToResultPage();

		string result =
			$"Artefacts: {manager.currentQuestionIndex} / {manager.questionList.Count}\n" +
			$"Score: {manager.totalScore}\n" +
			$"Remarks: {manager.GetScoreRemarks()}";
		resultTMP.text = result;
	}
}