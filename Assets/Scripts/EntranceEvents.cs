using System;
using Unity.Cinemachine;
using UnityEngine;

public class EntranceEvents : MonoBehaviour
{
    public event Action OnEnter;
    public event Action OnExit;
    [SerializeField] private bool enableCameraOnExit;
    [SerializeField] private bool disableCameraOnEnter;
    [SerializeField] private bool disableEntranceOnExit;
    [SerializeField, HideInInspector] private PathEndPoint endPoint;
    [SerializeField] private CinemachineFollow cinemachineFollow;
    [SerializeField] private CameraBehavior cameraBehavior;

    private void OnValidate()
    {
        endPoint = GetComponent<PathEndPoint>();
    }

    private void Awake()
    {
        if (enableCameraOnExit)
        {
            OnExit += () => cinemachineFollow.enabled = true;
            OnExit += cameraBehavior.ForceMinFov;
        }
        if (disableCameraOnEnter)
            OnEnter += () => cinemachineFollow.enabled = false;
        if (disableEntranceOnExit)
        {
            OnExit += () => endPoint.paths[0].isActive = false;
            OnExit += endPoint.UpdateStateColor;
        }

        MyDebugLogManager.LogMessage("EntranceEvents events successfuly set");
    }

    public void InvokeEnter()
    {
        OnEnter?.Invoke();
    }

    public void InvokeExit()
    {
        MyDebugLogManager.LogMessage("InvokeExit called");
        OnExit?.Invoke();
    }
}
