using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Connections))]
public class Connector : MonoBehaviour, IActivatable
{
    [SerializeField] private Connections connectionObj;
    [SerializeField] private ConnectorVisualizer visualizer;
    [SerializeField] private PathEndPoint thisPathEndPoint;
    private LinkedList<Connection> connections;
    private LinkedListNode<Connection> _currentConnection;
    public Connection currentConnection => _currentConnection.Value;

    private void Awake()
    {
        connections = new LinkedList<Connection>(connectionObj.connections);
        _currentConnection = connections.First;

        UpdatePathsActivity();
        visualizer.ChangeAngle(_currentConnection.Value.angle);
    }

    public void NextState()
    {
        _currentConnection = _currentConnection.Next ?? connections.First;

        UpdatePathsActivity();
        visualizer.ChangeAngle(_currentConnection.Value.angle);
    }

    private void UpdatePathsActivity()
    {
        //Turn on all paths from the currentConnection
        //Turn off all others
        foreach (Path path in thisPathEndPoint.paths)
        {
            if (currentConnection.first == path || currentConnection.second == path)
            {
                    path.isActive = true;
            }
            else
            {
                path.isActive = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextState();
            print($"Connecting {_currentConnection.Value.first.name} to {_currentConnection.Value.second.name}");
        }
    }

    public void Activate()
    {
        NextState();
    }
}
