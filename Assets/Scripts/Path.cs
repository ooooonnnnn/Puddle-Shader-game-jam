using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<PathEndPoint> endPoints;
    public LineRenderer path;
    public bool isActive = true;
    public float totalLength;

    private void OnValidate()
    {
        path = GetComponent<LineRenderer>();
        List<Vector3> pathPos = Enumerable.Range(0, path.positionCount)
                .Select(j => path.GetPosition(j))
                .ToList();
        Vector3 curr = pathPos[0];
        for (int i = 1; i < path.positionCount; i++)
        {
            Vector3 next = pathPos[i];
            totalLength += Vector3.Distance(curr, next);
            curr = next;
        }
        for (int i = 0; i < path.positionCount; i++)
        {
            Vector3 position = path.GetPosition(i);
            position.z = 0f; // Ensure z position is 0 for 2D
            path.SetPosition(i, position);
        }
    }
}
