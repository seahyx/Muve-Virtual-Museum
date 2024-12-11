using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MiniGameManager;

public class QuizRow : MonoBehaviour
{
    public enum QuizRowType
    {
        Text,
        Image,
		Completed
    }

    [SerializeField, Tooltip("Row type.")]
	public QuizRowType rowType = QuizRowType.Text;

	[SerializeField, Tooltip("Question number TMP.")]
    public TextMeshProUGUI questionNumTMP;

    [SerializeField, Tooltip("Question clue TMP.")]
    public TextMeshProUGUI questionClueTMP;

	[SerializeField, Tooltip("Question clue image.")]
	public Image questionClueImage;

	[SerializeField, Tooltip("Toggle UGUI.")]
    public Toggle checkbox;

    private Question _question;
    public Question Question
    {
		get => _question;
		set
		{
			_question = value;
			questionNumTMP.text = $"{_question.Index + 1}.";
			switch (rowType)
			{
				case QuizRowType.Text:
					questionClueTMP.text = _question.ClueText;
					break;
				case QuizRowType.Image:
					questionClueImage.sprite = _question.ClueImage;
					break;
				case QuizRowType.Completed:
					questionClueTMP.text = _question.CorrectAnswer;
					break;
			}
		}
	}
}
