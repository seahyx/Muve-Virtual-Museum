using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
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
    private float minimumVelocity = 0.1f;

    [SerializeField]
    private FootstepSet[] footstepSets;
    private AudioSource audioSource;
    private CharacterController controller;
    [SerializeField]
    private TeleportationProvider teleportationProvider;
    private bool isTeleporting = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        teleportationProvider.endLocomotion += onTeleport;
    }

    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isTeleporting)
        {
            if (controller.velocity.magnitude < minimumVelocity)
                return;

            if (audioSource.isPlaying)
                return;
        }
        else
        {
            isTeleporting = false;
        }
        
        if (hit.normal.y < 0.4f)
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
        FootstepSet defaultStepSet = footstepSets[0];
        AudioClip defaultClip = defaultStepSet.footstepSounds[Random.Range(0, defaultStepSet.footstepSounds.Length)];
        audioSource.PlayOneShot(defaultClip);
    }

    void onTeleport(LocomotionSystem loco)
    {
        isTeleporting = true;
    }
}
