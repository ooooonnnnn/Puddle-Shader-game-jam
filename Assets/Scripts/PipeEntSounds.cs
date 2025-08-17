using System;
using UnityEngine;

[RequireComponent(typeof(EntranceEvents))]
[RequireComponent(typeof(AudioSource))]
public class PipeEntSounds : MonoBehaviour
{
    [SerializeField, HideInInspector] private AudioClip exitSound;
    [SerializeField, HideInInspector] private AudioClip enterSound;
    [SerializeField, HideInInspector] private AudioSource _audioSource;
    [SerializeField, HideInInspector] private EntranceEvents _entranceEvents;
    
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