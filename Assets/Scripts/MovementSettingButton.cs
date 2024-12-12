using System;
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

		playerSettings.MovementSetting = playerSettings.MovementSetting.Next();
	}
}

public static class Extensions
{
	public static T Next<T>(this T src) where T : struct
	{
		if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

		T[] Arr = (T[])Enum.GetValues(src.GetType());
		int j = Array.IndexOf<T>(Arr, src) + 1;
		return (Arr.Length == j) ? Arr[0] : Arr[j];
	}
}
