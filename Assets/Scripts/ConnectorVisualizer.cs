using UnityEngine;

public class ConnectorVisualizer : MonoBehaviour
{
    public void ChangeAngle(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
