using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartFollow(PathEndPoint origin)
    {
        StartCoroutine(FollowPathsToTarget(origin));
    }

    private IEnumerator FollowPathsToTarget(PathEndPoint origin)
    { 
        PathEndPoint currentPoint = origin;
        List<PathEndPoint> endPoints = new List<PathEndPoint>();
        List<Path> paths = new List<Path>();

        //Get all paths and end points from origin to target
        do
        {
            endPoints.Add(currentPoint);
            print($"{currentPoint.paths.Where(p => p.isActive).Count()} active paths from {currentPoint.gameObject.name}");
            Path nextPath = currentPoint.paths.Find(path => 
            {return path.isActive && path.endPoints.Contains(currentPoint);});
            paths.Add(nextPath);
            currentPoint = nextPath.endPoints.Find(ep => ep != currentPoint);
            print($"next end point {currentPoint.gameObject.name}");

        } while (!currentPoint.isEntrance);

        endPoints.Add(currentPoint);

        print($"Found {endPoints.Count} end points and {paths.Count} paths from {origin.gameObject.name} to {currentPoint.gameObject.name}");

        // Move through the path
        rb.bodyType = RigidbodyType2D.Static;
        for (int i = 0; i < paths.Count; i++)
        {
            transform.position = endPoints[i].transform.position;
            yield return new WaitForSeconds(0.2f);
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
