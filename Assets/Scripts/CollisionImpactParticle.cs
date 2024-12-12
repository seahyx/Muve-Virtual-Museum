using UnityEngine;

public class CollisionImpactParticle : MonoBehaviour
{
	public AudioSource audioSource;

	private void Update()
	{
		if (!audioSource.isPlaying)
		{
			Destroy(gameObject);
		}
	}
}
