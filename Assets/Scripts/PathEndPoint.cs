using UnityEngine;
using System.Collections.Generic;

public class PathEndPoint : MonoBehaviour
{
    [SerializeField] public List<Path> paths;
    public bool isEntrance => paths.Count == 1; 

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
}
