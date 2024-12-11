using UnityEngine;

public class FallingNet : MonoBehaviour
{
    [SerializeField, Tooltip("Minimum Y height before this object gets respawned back in its original position.")]
    private float minimumY = -10.0f;

	[SerializeField, Tooltip("Whether to reset the rotation after respawning.")]
	private bool resetRotation = false;

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private void Awake()
	{
		originalPosition = transform.position;
		originalRotation = transform.rotation;
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
		transform.position = originalPosition;
		if (resetRotation)
		{
			transform.rotation = originalRotation;
		}
		if (TryGetComponent(out Rigidbody rigidbody))
			rigidbody.velocity = Vector3.zero;
		if (TryGetComponent(out CharacterController characterController))
			characterController.SimpleMove(Vector3.zero);
	}
}
