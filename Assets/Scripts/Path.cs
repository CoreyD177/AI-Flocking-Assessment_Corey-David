using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    #region Variables
    //List of waypoints for yellow ducks to follow once they find any of the waypoints in game world
    [Header("Waypoints")]
    [Tooltip("Will be populate automatically on running")]
    public List<Transform> waypoints;
    //Radius that determines how close duck needs to be to find waypoint
    [Tooltip("Set the detection radius for the waypoints")]
    public float radius = 20f;
    //Set the size of gizmos visible in scene view whilst game is running
    [Header("Gizmos")]
    [Tooltip("Set the gizmo size for scene view guides")]
    [SerializeField] private Vector3 _gizmoSize = Vector3.one;
    #endregion
    #region Waypoint List
    private void Start()
    {
        //Call the function to fill the list with waypoints
        FillWithChildObjects();
    }
    private void FillWithChildObjects()
    {
        //Find all child objects of this scripts game object and add them to the list
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                waypoints.Add(child);
            }
        }
    }
    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        //If we don't have any waypoints exit function
        if (waypoints == null || waypoints.Count == 0)
        {
            return;
        }
        //For each waypoint entry
        for (int i = 0; i < waypoints.Count; i++)
        {
            //Store waypoint location
            Transform waypoint = waypoints[i];
            //If waypoint entry is empty skip to next iteration
            if (waypoint == null)
            {
                continue;
            }
            //Draw a cyan coloured gizmo at location of current waypoint entry
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(waypoint.position, _gizmoSize);
            //Draw a magenta line connecting waypoint to the next waypoint entry
            Gizmos.color = Color.magenta;
            if (i + 1 < waypoints.Count && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(waypoint.position, waypoints[0].position);
            }
        }
    }
    #endregion
}
