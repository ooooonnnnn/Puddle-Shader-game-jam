using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PipeTravesalSound : MonoBehaviour
{
    [SerializeField, HideInInspector] private AudioSource audioSource;

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    public void StartSound()
    {
        MyDebugLogManager.LogMessage("StartSound called");
        audioSource?.Play();
    }

    public void StopSound()
    {
        MyDebugLogManager.LogMessage("StopSound called");
         if (audioSource == null)
             MyDebugLogManager.LogMessage("AudioSource is null.");

        // Stop the audio source
        audioSource?.Stop();
    }
}
