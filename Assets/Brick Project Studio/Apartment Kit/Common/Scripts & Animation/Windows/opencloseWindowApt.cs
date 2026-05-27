using System.Collections;
using UnityEngine;

public class DoorAutoTrigger : MonoBehaviour
{
    [Header("Door")]
    public Animator doorAnimator;
    public string openStateName = "Opening";
    public string closeStateName = "Closing";

    [Header("Close Settings")]
    public bool closeWhenPlayerLeaves = true;
    public float closeDelayAfterEnter = 0f; // set to 1-2 sec if you want auto close after entering

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;

    [Header("Player Tag")]
    public string playerTag = "Player";

    private bool isOpen = false;
    private Coroutine closeRoutine;

    private void Awake()
    {
        // Auto-find components if not assigned
        if (doorAnimator == null)
            doorAnimator = GetComponentInParent<Animator>();

        if (audioSource == null && doorAnimator != null)
            audioSource = doorAnimator.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // Cancel any pending close
        if (closeRoutine != null)
        {
            StopCoroutine(closeRoutine);
            closeRoutine = null;
        }

        // Open door
        if (!isOpen)
            OpenDoor();

        // Optional: close after a delay once player enters
        if (closeDelayAfterEnter > 0f)
            closeRoutine = StartCoroutine(CloseAfterDelay(closeDelayAfterEnter));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (closeWhenPlayerLeaves)
        {
            // Close when player leaves trigger area
            if (closeRoutine != null)
            {
                StopCoroutine(closeRoutine);
                closeRoutine = null;
            }

            if (isOpen)
                CloseDoor();
        }
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isOpen)
            CloseDoor();

        closeRoutine = null;
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (doorAnimator != null)
            doorAnimator.Play(openStateName, 0, 0f);

        PlaySound(openClip);
    }

    private void CloseDoor()
    {
        isOpen = false;

        if (doorAnimator != null)
            doorAnimator.Play(closeStateName, 0, 0f);

        PlaySound(closeClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
