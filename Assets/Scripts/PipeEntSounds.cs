using System;
using UnityEngine;

[RequireComponent(typeof(EntranceEvents))]
[RequireComponent(typeof(AudioSource))]
public class PipeEntSounds : MonoBehaviour
{
    [SerializeField] private AudioClip exitSound;
    [SerializeField] private AudioClip enterSound;
    private AudioSource _audioSource;
    private EntranceEvents _entranceEvents;
    
    void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
        _entranceEvents = GetComponent<EntranceEvents>();
    }

    private void Awake()
    {
        if (_entranceEvents != null && enterSound != null) _entranceEvents.OnEnter += () => PlaySound(enterSound);
        if (_entranceEvents != null && exitSound != null) _entranceEvents.OnExit += () => PlaySound(exitSound);
    }

    private void PlaySound(AudioClip sound)
    {
        if (_audioSource) _audioSource.PlayOneShot(sound);
    }
}