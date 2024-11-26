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
	}

	public static string GetMovementName(MovementType movementType)
	{
		switch (movementType)
		{
			case MovementType.Teleport:
				return "Teleportation";
			case MovementType.Smooth:
				return "Smooth";
			default:
				return "Unknown";
		}
	}

	private const string MovementSettingKey = "MovementSetting";
	public MovementType MovementSetting
	{
		get
		{ 
			return (MovementType)PlayerPrefs.GetInt(MovementSettingKey, (int)MovementType.Smooth);
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

	public UnityEvent OnSettingsChanged = new UnityEvent();
}
