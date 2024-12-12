using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MovementSettingManager : MonoBehaviour
{
    [SerializeField, Tooltip("Left controller reference.")]
    private ActionBasedControllerManager leftController;
	[SerializeField, Tooltip("Right controller reference.")]
	private ActionBasedControllerManager rightController;

	private void Start()
	{
		PlayerSettings.Instance.OnSettingsChanged.AddListener(OnSettingsChanged);
		OnSettingsChanged();
	}

	private void OnSettingsChanged()
	{
		PlayerSettings.MovementType movementType = PlayerSettings.Instance.MovementSetting;
		switch (movementType)
		{
			case PlayerSettings.MovementType.Teleport:
				leftController.teleportEnabled = true;
				rightController.teleportEnabled = true;
				leftController.smoothMotionEnabled = false;
				break;
			case PlayerSettings.MovementType.Smooth:
				leftController.teleportEnabled = false;
				rightController.teleportEnabled = false;
				leftController.smoothMotionEnabled = true;
				break;
			case PlayerSettings.MovementType.Both:
				leftController.teleportEnabled = false;
				rightController.teleportEnabled = true;
				leftController.smoothMotionEnabled = true;
				break;
		}
	}
}
