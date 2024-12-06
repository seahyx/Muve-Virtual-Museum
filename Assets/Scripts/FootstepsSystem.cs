using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsSystem : MonoBehaviour
{
    [System.Serializable]
    class FootstepSet
    {
        public string groundTag;
        public AudioClip[] footstepSounds;
    }

    [SerializeField]
    private FootstepSet[] footstepSets;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (audioSource.isPlaying)
            return;

        foreach (FootstepSet stepSet in footstepSets)
        {
            if (hit.gameObject.tag == stepSet.groundTag)
            {
                AudioClip clip = stepSet.footstepSounds[Random.Range(0, stepSet.footstepSounds.Length)];
                audioSource.PlayOneShot(clip);
                return;
            }
        }
        FootstepSet stepSet = footstepSets[0];
        AudioClip clip = stepSet.footstepSounds[Random.Range(0, stepSet.footstepSounds.Length)];
        audioSource.PlayOneShot(clip);
    }
}
