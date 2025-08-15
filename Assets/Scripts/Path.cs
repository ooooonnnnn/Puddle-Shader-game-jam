using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public List<PathEndPoint> endPoints;
    public LineRenderer path;
    public bool isActive = true;

    private void Awake()
    {
        for (int i = 0; i < path.positionCount; i++)
        {
            Vector3 position = path.GetPosition(i);
            position.z = 0f; // Ensure z position is 0 for 2D
            path.SetPosition(i, position);
        }
    }

    private void OnValidate()
    {
        path = GetComponent<LineRenderer>();
    }
}
