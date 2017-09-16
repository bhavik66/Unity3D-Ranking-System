using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointsManager))]
public class WaypointContainerEditor : Editor
{
    public enum Mode
    {
        EnableEdit,
        DisableEdit
    }

    public enum DrawType
    {
        Icon,
        LabelIcon
    }

    private static Mode operationMode;

    private static string namePrefix;

    private static DrawType drawType;
    private static IconManager.Icon selectedIcon;
    private static IconManager.LabelIcon selectedLabelIcon;

    private static Vector3 positionAdder = Vector3.zero;

    private static int m_count = 0;

    RaycastHit hitInfo;

    void OnSceneGUI()
    {

        if (operationMode == Mode.EnableEdit || operationMode == Mode.EnableEdit)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseUp && e.button == 1 )
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    if (operationMode == Mode.EnableEdit)
                        AddWaypoint();
                }
            }
        }

    }

    public override void OnInspectorGUI()
    {

        if (operationMode == Mode.EnableEdit)
        {
            if (GUILayout.Button("Disable Edit"))
            {
                operationMode = Mode.DisableEdit;
            }


            namePrefix = EditorGUILayout.TextField("Name prefix", "WP");

            positionAdder = EditorGUILayout.Vector3Field("Change Position", positionAdder);

            drawType = (DrawType)EditorGUILayout.EnumPopup("Icon Type", drawType);

            if (drawType == DrawType.Icon)
                selectedIcon = (IconManager.Icon)EditorGUILayout.EnumPopup("Icons", selectedIcon);
            else if (drawType == DrawType.LabelIcon)
                selectedLabelIcon = (IconManager.LabelIcon)EditorGUILayout.EnumPopup("Label Icons", selectedLabelIcon);

            if (GUILayout.Button("Reset Count"))
            {
                m_count = 0;
            }

            EditorGUILayout.LabelField(">>>> Right click for add waypoint <<<<<");
        }
        else if (operationMode == Mode.DisableEdit)
        {
            if (GUILayout.Button("Enable Edit"))
            {
                operationMode = Mode.EnableEdit;
            }
        }
    }

    private void AddWaypoint()
    {
        GameObject waypointInstance = new GameObject();
        waypointInstance.transform.position = hitInfo.point + positionAdder;
        waypointInstance.transform.parent = Selection.activeGameObject.transform;
        waypointInstance.name = namePrefix + "_" + m_count;

        if (drawType == DrawType.Icon)
            IconManager.SetIcon(waypointInstance, selectedIcon);
        else if (drawType == DrawType.LabelIcon)
            IconManager.SetIcon(waypointInstance, selectedLabelIcon);

        m_count++;
    }
}
