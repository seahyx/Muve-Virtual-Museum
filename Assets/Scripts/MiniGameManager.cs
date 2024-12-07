using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MiniGameManager : MonoBehaviour
{
    class Question
    {
        public string CorrectAnswer;
        public string PlayerAnswer;
        public string QuestionClue;

        public Question(string CorrectAnswer, string QuestionClue)
        {
            this.CorrectAnswer = CorrectAnswer;
            this.QuestionClue = QuestionClue;
        }
    }

    [SerializeField]
    int maxQuestions = 5;
    [SerializeField, Tooltip("Duration per question in seconds.")]
    float quizDurationPerQuestion = 100;

    MuseumLabel[] museumLabels;
    List<Question> questionList = new();
    int currentQuestion = 0;
    bool quizRunning = false;

    private void Start()
    {
        museumLabels = Object.FindObjectsByType<MuseumLabel>(FindObjectsSortMode.None);
        foreach (MuseumLabel museumLabel in museumLabels)
        {
            museumLabel.artefactInteractable.firstSelectEntered.AddListener((eventArgs) =>
                OnArtefactSelected(museumLabel.artefactInteractable, museumLabel.artefactDescription.Title));
        }
    }

    public void StartQuiz()
    {
        quizRunning = true;
        int[] quizIndices = GenerateQuestionIndices();
        questionList.Clear();
        foreach(int quizIndex in quizIndices)
        {
            var arteDesc = museumLabels[quizIndex].artefactDescription;
            questionList.Add(new Question(arteDesc.Title, arteDesc.MiniGameDescription));
        }
        currentQuestion = 0;
        DisplayQuestion(currentQuestion);
    }

    public void NextQuestion()
    {
        DisplayQuestion(currentQuestion++);
    }

    private void DisplayQuestion(int currentQuestion)
    {
        if (currentQuestion >= questionList.Count())
        {
            DisplayResults();
            return;
        }

        // Do some shit
    }

    private void AnswerQuestion(string answer)
    {
        questionList[currentQuestion].PlayerAnswer = answer;
        DisplayQuestion(currentQuestion);
    }

    private void DisplayResults()
    {

    }

    private int[] GenerateQuestionIndices()
    {
        // Randomly Selected maxQuestions number of items from museumLabels
        int[] arr = Enumerable.Range(0, museumLabels.Count()).ToArray();

        // Shuffle the array
        System.Random rng = new System.Random();
        rng.Shuffle(arr);

        int totalQuestions = arr.Count() < maxQuestions ? arr.Count() : maxQuestions;
        int[] quizIndices = arr[0..totalQuestions];

        return quizIndices;
    }

    private void OnArtefactSelected(XRGrabInteractable interactable, string title)
    {
        if (!quizRunning)
            return;

        AnswerQuestion(title);
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
