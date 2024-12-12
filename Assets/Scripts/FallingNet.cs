using UnityEngine;

public class FallingNet : MonoBehaviour
{
	[SerializeField, Tooltip("Minimum Y height before this object gets respawned back in its original position.")]
	private float minimumY = -10.0f;

	[SerializeField, Tooltip("Whether to reset the rotation after respawning.")]
	private bool resetRotation = false;

	[SerializeField, Tooltip("Whether to reset the scale after respawning.")]
	private bool resetScale = true;

	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private Vector3 originalScale;

	private void Awake()
	{
		originalPosition = transform.localPosition;
		originalRotation = transform.localRotation;
		originalScale = transform.localScale;
	}
	private void Update()
	{
		if (transform.position.y < minimumY)
		{
			Respawn();
		}
	}
	public void Respawn()
	{
		transform.localPosition = originalPosition;
		if (resetRotation)
			transform.localRotation = originalRotation;
		if (resetScale)
			transform.localScale = originalScale;
		if (TryGetComponent(out Rigidbody rigidbody))
			rigidbody.velocity = Vector3.zero;
		if (TryGetComponent(out CharacterController characterController))
			characterController.SimpleMove(Vector3.zero);
	}
}
