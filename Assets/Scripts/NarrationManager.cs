using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class NarrationManager : MonoBehaviour
{
	[SerializeField, Tooltip("The AudioSource that the narrator will play from.")]
	private AudioSource audioSource;

	[SerializeField, Tooltip("Invoked when the current narration changes.")]
	private UnityEvent onAudioChanged = new();

	[SerializeField, Tooltip("Stop playing audio if the same narration is already playing.")]
	private bool stopNarrationIfPlayAgain = true;

	public string CurrentNarrationTitle { get; private set; }
	public bool IsPlaying => audioSource.isPlaying;
	public float playTime => audioSource.time;
	public float duration => audioSource.clip.length;
	public float playTimeNormalized => playTime / duration;

	public static NarrationManager Instance { get; private set; }

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		Instance = this;

		// Get voice volume from player settings
		audioSource.volume = PlayerSettings.Instance.VoiceVolumeSetting;

		// Subscribe to the settings changed event
		PlayerSettings.Instance.OnSettingsChanged.AddListener(() =>
		{
			audioSource.volume = PlayerSettings.Instance.VoiceVolumeSetting;
		});

	}

	public void PlayNarration(AudioClip clip, string title)
	{
		if (audioSource.isPlaying && CurrentNarrationTitle == title)
		{
			audioSource.Stop();
		}
		else
		{
			CurrentNarrationTitle = title;
			audioSource.clip = clip;
			audioSource.Play();
			onAudioChanged.Invoke();
		}
	}
}
