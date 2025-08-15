using UnityEngine;

public class Connections : MonoBehaviour
{
    public Connection[] connections;
}

[System.Serializable]
public class Connection
{
    public Path first;
    public Path second;
    public float angle;
}