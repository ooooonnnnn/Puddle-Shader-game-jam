using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PathEndPoint : MonoBehaviour
{
    [SerializeField] public List<Path> paths;
    [SerializeField] public float exitSpeedBoost = 5f;
    [SerializeField] public float maxExitSpeed = 25f;
    [SerializeField, HideInInspector] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color inactiveTint;
    [SerializeField, HideInInspector] private Color initialCol;
    private static event Action OnChangeStateStatic;
    public bool isEntrance => paths.Count == 1;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialCol = spriteRenderer.color;
    }

    private void Awake()
    {
        if (isEntrance)
        {
            OnChangeStateStatic += UpdateStateColor;
            UpdateStateColor();
        }
    }

    public Path GetPathContainingEndPoint(PathEndPoint endPoint)
    {
        foreach (var path in paths)
        {
            foreach (var point in path.endPoints)
            {
                if (point == endPoint)
                {
                    return path;
                }
            }
        }

        return null;
    }

    public Path GetPathNotContainingEndPoint(PathEndPoint endPoint)
    {
        foreach (var path in paths)
        {
            bool notContains = true;
            foreach (var point in path.endPoints)
            {
                if (point == endPoint)
                {
                    notContains = false;
                    break;
                }
            }

            if (notContains)
            {
                return path;
            }
        }

        return null;
    }

    public void DisableTrigger()
    {
        StartCoroutine(TriggerTimeout());
    }

    private IEnumerator TriggerTimeout()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }

    public void UpdateStateColor()
    {
        spriteRenderer.color = paths[0].isActive ? initialCol : initialCol * inactiveTint;
    }

    public static void InvokeChangeState()
    {
        OnChangeStateStatic?.Invoke();
    }

    private void OnDestroy()
    {
        if (isEntrance)
            OnChangeStateStatic = null;
    }


}