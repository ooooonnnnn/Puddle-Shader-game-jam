using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Connections : MonoBehaviour
{
    public Connection[] connections;
}

[System.Serializable]
public class Connection
{
    public PipeBehavior first;
    public PipeBehavior second;
    public float angle;
}