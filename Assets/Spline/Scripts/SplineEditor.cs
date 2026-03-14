using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spline))]
public class SplineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();   // Draw normal inspector

        Spline spline = (Spline)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Spline Tools", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Node"))
        {
            Undo.RecordObject(spline, "Add Spline Node");
            spline.InsertNodeBetweenEnds();
            EditorUtility.SetDirty(spline);
        }

        if (GUILayout.Button("Remove Node"))
        {
            Undo.RecordObject(spline, "Remove Spline Node");
            spline.RemoveLastNode();
            EditorUtility.SetDirty(spline);
        }
    }
}