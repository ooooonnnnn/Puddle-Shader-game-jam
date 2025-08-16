using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    private float oneOverSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody2D>();
        oneOverSpeed = 1f / speed;
    }

    public void StartFollow(PathEndPoint origin)
    {
        print("Called StartFollow");
        StartCoroutine(FollowPathsToTarget(origin));
    }

    private IEnumerator FollowPathsToTarget(PathEndPoint origin)
    {
        PathEndPoint currentPoint = origin;
        PathEndPoint prevPoint = null;
        List<PathEndPoint> endPoints = new List<PathEndPoint>();
        List<Path> paths = new List<Path>();

        //Get all paths and end points from origin to target
        do
        {
            endPoints.Add(currentPoint);
            print(
                $"{currentPoint.paths.Where(p => p.isActive).Count()} active paths from {currentPoint.gameObject.name}");

            //Find next path segment
            Path nextPath;
            if (prevPoint == null)
            {
                nextPath = currentPoint.paths.Find(path =>
                {
                    return path.isActive && path.endPoints.Contains(currentPoint);
                });
            }
            else
            {
                nextPath = currentPoint.paths.Find(path =>
                {
                    return path.isActive && !path.endPoints.Contains(prevPoint);
                });
            }

            if (nextPath == null)
            {
                yield break;
            }

            paths.Add(nextPath);

            //Find next point
            prevPoint = currentPoint;
            currentPoint = nextPath.endPoints.Find(ep => ep != currentPoint);
            print($"next end point {currentPoint.gameObject.name}");
        } while (!currentPoint.isEntrance);

        endPoints.Add(currentPoint);

        print(
            $"Found {endPoints.Count} end points and {paths.Count} paths from {origin.gameObject.name} to {currentPoint.gameObject.name}");
        print($"Paths: {string.Join(", ", paths.Select(path => path.gameObject.name))}");
        print($"End Points: {string.Join(", ", endPoints.Select(ep => ep.gameObject.name))}");

        // Move through the path
        float initialVelocity = rb.linearVelocity.magnitude;
        speed = Mathf.Clamp(speed, 10f, initialVelocity * 5f);
        oneOverSpeed = 1f / speed;
        rb.bodyType = RigidbodyType2D.Static;
        yield return TweenPosition(transform.position, endPoints[0].transform.position);
        for (int i = 0; i < paths.Count; i++)
        {
            transform.position = endPoints[i].transform.position;
            yield return null;

            List<Vector3> pathPos = Enumerable.Range(0, paths[i].path.positionCount)
                .Select(j => paths[i].path.GetPosition(j))
                .ToList();
            //follow the path in the correct direction
            bool flip = (pathPos[pathPos.Count - 1] - transform.position).magnitude <
                        (pathPos[0] - transform.position).magnitude;

            float totalLength = paths[i].totalLength;
            for (int j = 0; j < paths[i].path.positionCount; j++)
            {
                int k = !flip ? j : paths[i].path.positionCount - 1 - j;
                //transform.position = paths[i].path.GetPosition(k);
                yield return TweenPosition(transform.position, paths[i].path.GetPosition(k));
            }
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        endPoints.Last().DisableTrigger();
        // Final position at the last end point
        transform.position =
            endPoints.Last().transform.position; // Adjust for exit position
        rb.linearVelocity = endPoints.Last().transform.up *
                      Mathf.Clamp(initialVelocity + endPoints.Last().exitSpeedBoost,
                          0, endPoints.Last().maxExitSpeed);
    }

    private IEnumerator TweenPosition(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);
        float duration = distance * oneOverSpeed;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.position = end; // Ensure final position is set
    }
}