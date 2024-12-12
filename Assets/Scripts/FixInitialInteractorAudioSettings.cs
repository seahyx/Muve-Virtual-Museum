using UnityEngine;

public class FixInitialInteractorAudioSettings : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    private float volume = 1;

	void Update()
    {
        AudioSource src = GetComponent<AudioSource>();
        if (src == null) return;

        src.spatialBlend = 1;
		src.volume = volume;
        enabled = false;
	}
}
