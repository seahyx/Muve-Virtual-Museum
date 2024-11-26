using TMPro;
using UnityEngine;

public class MovementSettingButton : MonoBehaviour
{
	[SerializeField, Tooltip("The TMP that will be updated with the current movement type.")]
	private TextMeshProUGUI valueText;

	private PlayerSettings playerSettings;

	void Start()
	{
		playerSettings = PlayerSettings.Instance;

		if (playerSettings == null)
		{
			Debug.LogError("PlayerSettings is null.");
			return;
		}

		UpdateValueText();
		playerSettings.OnSettingsChanged.AddListener(UpdateValueText);
	}

	private void UpdateValueText()
	{
		valueText.text = PlayerSettings.GetMovementName(playerSettings.MovementSetting);
	}

	public void ToggleMovementSetting()
	{
		if (playerSettings == null)
		{
			Debug.LogError("PlayerSettings is null.");
			return;
		}

		playerSettings.MovementSetting = playerSettings.MovementSetting == PlayerSettings.MovementType.Smooth
			? PlayerSettings.MovementType.Teleport
			: PlayerSettings.MovementType.Smooth;
	}
}
