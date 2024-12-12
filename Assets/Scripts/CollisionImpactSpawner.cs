using UnityEngine;

public class CollisionImpactSpawner : MonoBehaviour
{
    [SerializeField]
    private CollisionImpactParticle impactParticlePrefab;

    [SerializeField]
    private AudioClip impactSound;

	[SerializeField]
	private float maxImpactSoundVolume = 1;

	[SerializeField]
	private float minImpactRelativeVelocity = 1;

	[SerializeField]
	private float maxImpactRelativeVelocity = 6f;

	[SerializeField]
	private float audioPitch = 1;

	[SerializeField]
	private float cooldown = 0.1f;

	private float lastImpactTime;

	private void OnCollisionEnter(Collision collision)
	{
		if (Time.time - lastImpactTime < cooldown || collision.relativeVelocity.magnitude <= minImpactRelativeVelocity)
			return;
		lastImpactTime = Time.time;

		CollisionImpactParticle impactParticle = Instantiate(impactParticlePrefab, collision.contacts[0].point, Quaternion.identity);
		impactParticle.audioSource.volume = Mathf.Clamp01((collision.relativeVelocity.magnitude - minImpactRelativeVelocity) / (maxImpactRelativeVelocity - minImpactRelativeVelocity)) * maxImpactSoundVolume;
		impactParticle.audioSource.pitch = audioPitch + Random.Range(-.1f, 1f);
		impactParticle.audioSource.PlayOneShot(impactSound);
	}
}
