using System.IO;
using System.Linq;
using UnityEngine;

public class MoveLineToPoint : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    private LineRenderer path;
    private int posCount;

    public void MoveLine()
    {
        path = GetComponent<LineRenderer>();
        posCount = path.positionCount;
        for (int i = 0; i < posCount; i++)
        {
            Vector3 position = path.GetPosition(i);
            position.z = 0f; // Ensure z position is 0 for 2D
            path.SetPosition(i, position);
        }

        if (followTarget == null)
            return;

        //Find the closer end to the followTarget
        Vector3[] offsets = new Vector3[2] {path.GetPosition(0) - followTarget.position,
                                       path.GetPosition(posCount-1) - followTarget.position};
        int minInd = offsets.Select((offsets, index) => new { magnitude = offsets.magnitude, index = index })
            .OrderBy(x => x.magnitude)
            .First().index;
        Vector3 offset = offsets[minInd];
        for (int i = 0; i < posCount; i++)
        {
            path.SetPosition(i,
                path.GetPosition(i) - offset);
        }
    }
}
