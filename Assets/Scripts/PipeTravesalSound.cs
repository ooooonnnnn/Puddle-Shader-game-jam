using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PipeTravesalSound : MonoBehaviour
{
    private AudioSource audioSource;

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
        audioSource?.Stop();
    }
}
