using System.Collections.Generic; //Required for lists
using UnityEngine; //required for Unity connection
//Create entry in Unity asset creation menu for Wander Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Wander Behaviour")]
public class WanderBehaviour : DuckBehaviour
{
    #region Variables
    //Reference Path class so we can use the waypoint list
    private Path _path;
    //Set current waypoint index to 0 by default
    private int _currentWaypoint = 0;
    #endregion
    #region Movement
    //CalculateMove function from DuckBehaviour class
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //If we don't have a path find one
        if (_path == null)
        {
            FindPath(agent, context);
        }
        //Return our path for use in FollowPath function
        return FollowPath(agent);
    }
    private Vector3 FollowPath(DuckAgent agent)
    {
        //if we don't have a path don't add anything to our movement calculation
        if (_path == null) return Vector3.zero;
        //Create a Vector3 to store direction to next waypoint
        Vector3 waypointDirection;
        //Use WaypointInRadius function to check if we have reached a waypoint
        if (WaypointInRadius(agent, _currentWaypoint, out waypointDirection))
        {
            //Iterate to next waypoint
            _currentWaypoint++;
            //If we have gone past our last waypoint go back to the first
            if (_currentWaypoint >= _path.waypoints.Count)
            {
                _currentWaypoint = 0;
            }
            //Don't add anything to our movement calculation as we are at waypoint
            return Vector3.zero;
        }
        //Return direction to next waypoint for use in movement calculation
        return waypointDirection.normalized;
    }
    public bool WaypointInRadius(DuckAgent agent, int currentWaypoint, out Vector3 waypointDirection)
    {
        //Set direction to next waypoint
        waypointDirection = (Vector3)(_path.waypoints[currentWaypoint].position - agent.transform.position);
        //If we are close enough to waypoint return true
        if (waypointDirection.magnitude < _path.radius)
        {
            return true;
        }
        //Else we are not at waypoint yet
        else
        {
            return false;
        }
    }
    private void FindPath(DuckAgent agent, List<Transform> context)
    {
        //Create a list of transforms filled with the list from the filter attached to this behaviour or the full context list if filtered list is not found
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //If filtered list is empty still return nothing
        if (filteredContext.Count == 0)
        {
            return;
        }
        //Create random integer for index of filtered list
        int randomPath = Random.Range(0, filteredContext.Count);
        //Retrieve the Path class from the parent of the randomized entry in filtered list
        _path = filteredContext[randomPath].GetComponentInParent<Path>();
    }
    #endregion
}
