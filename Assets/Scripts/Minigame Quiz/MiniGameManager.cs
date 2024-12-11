using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniGameManager : MonoBehaviour
{
    public class Question
    {
        public int Index;
        public string CorrectAnswer;
        public string ClueText;
        public Sprite ClueImage;

        public Question(int idx, string ans, string clue, Sprite image)
        {
            Index = idx;
            CorrectAnswer = ans;
            ClueText = clue;
			ClueImage = image;
		}
    }

    //[SerializeField]
    //int maxQuestions = 5;
    [SerializeField, Tooltip("Time limit for the quiz in seconds.")]
    float quizDuration = 600;

    [SerializeField, Tooltip("Max point per question")]
    public int itemScore = 100;

    [SerializeField, Tooltip("Grace Period for the question in seconds.")]
    public int gracePeriod = 10;

    [SerializeField, Tooltip("Score reduction per second after grace period.")]
    public int scoreReductionPerSecond = 3;

    [SerializeField, Tooltip("Minimum score per question.")]
    public int minScore = 20;

    [Header("Events")]

    [SerializeField, Tooltip("Invoked when the quiz moves to the next question.")]
    public UnityEvent OnNextQuestion = new();

    [SerializeField, Tooltip("Invoked when the quiz is completed.")]
    public UnityEvent OnQuizCompleted = new();

    /// <summary>
    /// Currently active question.
    /// </summary>
    public int currentQuestionIndex { get; private set; } = 0;
    /// <summary>
    /// Total score of the quiz.
    /// </summary>
    public int totalScore { get; private set; } = 0;
    /// <summary>
    /// Timer for the current question.
    /// </summary>
    public float timerCurrentQuestion { get; private set; } = 0;
    /// <summary>
    /// Total time elapsed since the quiz started.
    /// </summary>
    public float timerTotal { get; private set; } = 0;
    /// <summary>
    /// List of questions.
    /// </summary>
	public List<Question> questionList { get; private set; } = new();
    /// <summary>
    /// Current question.
    /// </summary>
    public Question currentQuestion => questionList[currentQuestionIndex];

	MuseumLabel[] museumLabels;
    bool quizRunning = false;

    private void Start()
    {
        museumLabels = Object.FindObjectsByType<MuseumLabel>(FindObjectsSortMode.None);
        foreach (MuseumLabel museumLabel in museumLabels)
        {
            museumLabel.artefactInteractable.firstSelectEntered.AddListener((eventArgs) =>
                OnArtefactSelected(museumLabel.artefactInteractable, museumLabel.artefactDescription.Title));
		}
		Debug.Log($"Total Museum Labels in scene: {museumLabels.Length}");
	}

    private void Update()
    {
        if (quizRunning)
        {
            timerCurrentQuestion += Time.deltaTime;
            timerTotal += Time.deltaTime;
        }
    }

    public void StartQuiz()
    {
        quizRunning = true;
        int[] quizIndices = GenerateQuestionIndices();
        questionList.Clear();

		Debug.Log("Quiz Started, quiz order:");
		for (int i = 0; i < quizIndices.Length; i++)
        {
			var arteDesc = museumLabels[quizIndices[i]].artefactDescription;
			questionList.Add(new Question(i, arteDesc.Title, arteDesc.MiniGameDescription, arteDesc.MiniGameImage));

            Debug.Log($"{i}, {arteDesc.Title}, {arteDesc.MiniGameDescription}, {arteDesc.MiniGameImage}");
		}
        currentQuestionIndex = 0;
        totalScore = 0;
        timerCurrentQuestion = 0;
        timerTotal = 0;
        DisplayQuestion(currentQuestionIndex);
    }

    public void StopQuiz()
    {
		quizRunning = false;
		DisplayResults();

        Debug.Log($"Quiz Completed, Total Score: {totalScore}");
	}

    public void NextQuestion()
    {
        DisplayQuestion(currentQuestionIndex++);
        timerCurrentQuestion = 0;
    }

    private void DisplayQuestion(int currentQuestionIndex)
    {
        if (currentQuestionIndex >= questionList.Count())
        {
            StopQuiz();
            return;
        }

        Debug.Log($"Displaying Question: {currentQuestionIndex}, {currentQuestion.CorrectAnswer}, {currentQuestion.ClueText}, {currentQuestion.ClueImage}");
		OnNextQuestion.Invoke();
    }

    private void DisplayResults()
    {
        OnQuizCompleted.Invoke();
    }

    private void AnswerQuestion(string answer)
    {
        if(questionList[currentQuestionIndex].CorrectAnswer == answer)
        {
            totalScore += CalculateScore(timerCurrentQuestion);
            NextQuestion();
        }
    }

    private int[] GenerateQuestionIndices()
    {
        // Randomly Selected maxQuestions number of items from museumLabels
        int[] arr = Enumerable.Range(0, museumLabels.Count()).ToArray();

        // Shuffle the array
        System.Random rng = new System.Random();
        rng.Shuffle(arr);

        return arr;
    }

    private void OnArtefactSelected(XRGrabInteractable interactable, string title)
    {
        if (!quizRunning)
            return;

        AnswerQuestion(title);
    }

    private int CalculateScore(float timerCurrentQuestion)
    {
        // Question start, grace period, reduce score until min score.
        if (timerCurrentQuestion <= gracePeriod)
            return itemScore;

        int score = itemScore - (int)((timerCurrentQuestion - gracePeriod) * scoreReductionPerSecond);

        return score < minScore ? minScore : score;
    }
}

static class RandomExtensions
{
    public static void Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
