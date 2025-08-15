using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Connections))]
public class Connector : MonoBehaviour, IActivatable
{
    [SerializeField] private Connections connectionObj;
    [SerializeField] private ConnectorVisualizer visualizer;
    private LinkedList<Connection> connections;
    private LinkedListNode<Connection> currentState;

    private void Awake()
    {
        connections = new LinkedList<Connection>(connectionObj.connections);
        currentState = connections.First;
        currentState.Value.first.myExit = currentState.Value.second;
        currentState.Value.second.myExit = currentState.Value.first;
        visualizer.ChangeAngle(currentState.Value.angle);
    }

    public void NextState()
    {
        currentState.Value.first.myExit = null;
        currentState.Value.second.myExit = null;

        currentState = currentState.Next ?? connections.First;

        currentState.Value.first.myExit = currentState.Value.second;
        currentState.Value.second.myExit = currentState.Value.first;
        visualizer.ChangeAngle(currentState.Value.angle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextState();
            print($"Connecting {currentState.Value.first.name} to {currentState.Value.second.name}");
        }
    }

    public void Activate()
    {
        NextState();
    }
}
