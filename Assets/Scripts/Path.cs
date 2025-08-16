using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<PathEndPoint> endPoints;
    public LineRenderer path;
    public bool isActive = true;

    private void OnValidate()
    {
        path = GetComponent<LineRenderer>();
        for (int i = 0; i < path.positionCount; i++)
        {
            Vector3 position = path.GetPosition(i);
            position.z = 0f; // Ensure z position is 0 for 2D
            path.SetPosition(i, position);
        }
    }
}
