using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Save and update player settings.
/// </summary>
public class PlayerSettings : MonoBehaviour
{
	#region Fake Singleton Pattern

	public static PlayerSettings Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	#endregion

	public enum MovementType
	{
		Teleport,
		Smooth,
		Both,
	}

	public static string GetMovementName(MovementType movementType)
	{
		switch (movementType)
		{
			case MovementType.Teleport:
				return "Mode: Teleport";
			case MovementType.Smooth:
				return "Mode: Smooth";
			case MovementType.Both:
				return "Mode: Both";
			default:
				return "Unknown";
		}
	}

	private const string MovementSettingKey = "MovementSetting";
	public MovementType MovementSetting
	{
		get
		{ 
			return (MovementType)PlayerPrefs.GetInt(MovementSettingKey, (int)MovementType.Teleport);
		}
		set
		{
			PlayerPrefs.SetInt(MovementSettingKey, (int)value);
			OnSettingsChanged.Invoke();
		}
	}

	private const string VoiceVolumeSettingKey = "VoiceVolumeSetting";
	public float VoiceVolumeSetting
	{
		get
		{
			return PlayerPrefs.GetFloat(VoiceVolumeSettingKey, 1.0f);
		}
		set
		{
			PlayerPrefs.SetFloat(VoiceVolumeSettingKey, value);
			OnSettingsChanged.Invoke();
		}
	}

	private const string FirstTimeSettingKey = "FirstTimeSetting";
	public bool FirstTimeSetting
	{
		get
		{
			return PlayerPrefs.GetInt(FirstTimeSettingKey, 1) == 1;
		}
		set
		{
			PlayerPrefs.SetInt(FirstTimeSettingKey, value ? 1 : 0);
			OnSettingsChanged.Invoke();
		}
	}

	public UnityEvent OnSettingsChanged = new UnityEvent();
}
