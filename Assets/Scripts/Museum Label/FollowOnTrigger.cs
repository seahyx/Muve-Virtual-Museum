using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class FollowOnTrigger : MonoBehaviour
{
	[SerializeField]
	private LazyFollow lazyFollowObject;

	[SerializeField, Tooltip("Offset transform reference.")]
	private Transform offset;

	[SerializeField, Tooltip("Position applied to the offset transform when following.")]
	private Vector3 followPositionOffset;

	[SerializeField, Tooltip("Rotation applied to the offset transform when following.")]
	private Vector3 followRotationOffset;

	[SerializeField, Tooltip("Time taken to return to the intial label position in seconds.")]
	private float returnDuration = 2.0f;

	[SerializeField, Tooltip("Animation curve.")]
	private AnimationCurve returnCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	private Vector3 initialFollowPosition = Vector3.zero;
	private Quaternion initialFollowRotation = Quaternion.identity;
	private Vector3 initialOffsetPosition = Vector3.zero;
	private Quaternion initialOffsetRotation = Quaternion.identity;

	private void Awake()
	{
		initialFollowPosition = lazyFollowObject.transform.localPosition;
		initialFollowRotation = lazyFollowObject.transform.localRotation;
		initialOffsetPosition = offset.localPosition;
		initialOffsetRotation = offset.localRotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			EnableFollow();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			DisableFollow();
		}
	}

	private void EnableFollow()
	{
		StopAllCoroutines();
		StartCoroutine(FollowAnim());
		lazyFollowObject.enabled = true;
	}

	private void DisableFollow()
	{
		lazyFollowObject.enabled = false;
		StopAllCoroutines();
		StartCoroutine(ResetAnim());
	}

	private IEnumerator FollowAnim()
	{
		offset.GetLocalPositionAndRotation(out Vector3 offsetPosition, out Quaternion offsetRotation);
		float currentTime = 0.0f;
		while (currentTime < returnDuration)
		{
			currentTime += Time.deltaTime;
			float lerpValue = currentTime / returnDuration;
			lerpValue = returnCurve.Evaluate(lerpValue);

			offset.localPosition = Vector3.LerpUnclamped(offsetPosition, followPositionOffset, lerpValue);
			offset.localRotation = Quaternion.SlerpUnclamped(offsetRotation, Quaternion.Euler(followRotationOffset), lerpValue);
			yield return null;
		}

		offset.localPosition = followPositionOffset;
		offset.localRotation = Quaternion.Euler(followRotationOffset);
	}

	private IEnumerator ResetAnim()
	{
		lazyFollowObject.transform.GetLocalPositionAndRotation(out Vector3 followPosition, out Quaternion followRotation);
		offset.GetLocalPositionAndRotation(out Vector3 offsetPosition, out Quaternion offsetRotation);
		float currentTime = 0.0f;
		while (currentTime < returnDuration)
		{
			currentTime += Time.deltaTime;
			float lerpValue = currentTime / returnDuration;
			lerpValue = returnCurve.Evaluate(lerpValue);

			lazyFollowObject.transform.localPosition = Vector3.LerpUnclamped(followPosition, initialFollowPosition, lerpValue);
			lazyFollowObject.transform.localRotation = Quaternion.SlerpUnclamped(followRotation, initialFollowRotation, lerpValue);
			offset.localPosition = Vector3.LerpUnclamped(offsetPosition, initialOffsetPosition, lerpValue);
			offset.localRotation = Quaternion.SlerpUnclamped(offsetRotation, initialOffsetRotation, lerpValue);
			yield return null;
		}

		lazyFollowObject.transform.localPosition = initialFollowPosition;
		lazyFollowObject.transform.localRotation = initialFollowRotation;
		offset.localPosition = initialOffsetPosition;
		offset.localRotation = initialOffsetRotation;
	}
}
