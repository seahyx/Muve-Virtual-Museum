using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingSlider : MonoBehaviour
{
	[SerializeField, Tooltip("The slider that will be updated with the current volume level.")]
	private Slider slider;

	private bool currentlyUpdatingSlider = false;

	private PlayerSettings playerSettings;

	void Start()
	{
		playerSettings = PlayerSettings.Instance;

		if (playerSettings == null)
		{
			Debug.LogError("PlayerSettings is null.");
			return;
		}

		UpdateSlider();
		playerSettings.OnSettingsChanged.AddListener(UpdateSlider);
		slider.onValueChanged.AddListener((x) => SetSliderSetting());
	}

	private void UpdateSlider()
	{
		if (currentlyUpdatingSlider)
			return;

		slider.normalizedValue = playerSettings.VoiceVolumeSetting;
	}

	public void SetSliderSetting()
	{
		if (playerSettings == null)
		{
			Debug.LogError("PlayerSettings is null.");
			return;
		}

		currentlyUpdatingSlider = true;
		playerSettings.VoiceVolumeSetting = slider.normalizedValue;
	}

	private void LateUpdate()
	{
		if (playerSettings == null)
			return;

		currentlyUpdatingSlider = false;
	}
}
