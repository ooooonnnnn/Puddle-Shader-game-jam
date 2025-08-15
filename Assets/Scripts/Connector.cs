using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private Connection[] connectionsArray;
    private LinkedList<Connection> connections;
    private LinkedListNode<Connection> currentState;

    private void Awake()
    {
        connections = new LinkedList<Connection>(connectionsArray);
        currentState = connections.First;
        currentState.Value.first.myExit = currentState.Value.second;
        currentState.Value.second.myExit = currentState.Value.first;
    }

    public void NextState()
    {
        currentState.Value.first.myExit = null;
        currentState.Value.second.myExit = null;

        currentState = currentState.Next ?? connections.First;

        currentState.Value.first.myExit = currentState.Value.second;
        currentState.Value.second.myExit = currentState.Value.first;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextState();
            print($"Connecting {currentState.Value.first.name} to {currentState.Value.second.name}");
        }
    }
}

[System.Serializable]
public class Connection
{
    public PipeBehavior first;
    public PipeBehavior second;
}
