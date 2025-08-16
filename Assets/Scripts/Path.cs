using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<PathEndPoint> endPoints;
    public LineRenderer path;
    public bool isActive = true;

    private void OnValidate()
    {
        path = GetComponent<LineRenderer>();
    }
}
