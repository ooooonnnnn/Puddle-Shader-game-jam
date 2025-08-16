using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(MoveLineToPoint))]
public class MoveLineToPoint_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector first
        DrawDefaultInspector();

        // Reference to the target script
        MoveLineToPoint myComponent = (MoveLineToPoint)target;

        // Add a button
        if (GUILayout.Button("Move line to point"))
        {
            myComponent.MoveLine();
        }
    }
}
#endif