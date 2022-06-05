using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Obstacle Avoidance Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Obstacle Avoidance")]
public class ObstacleAvoidBehaviour : DuckBehaviour
{
    #region Movement
    //Update DuckBehaviours CalculateMove function based off our new calculations
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //Retrieve IslandAvoid game object from Unity heirarchy
        GameObject _avoidIsland = GameObject.Find("IslandAvoid");
        //Retrieve SharkPrefab game object from Unity heirarchy
        GameObject _shark = GameObject.Find("SharkPrefab");
        //Set current move direction to nowhere
        Vector3 avoidanceMove = Vector3.zero;
        //If we are too close to island set direction to move away from island
        if (Vector3.Distance(agent.transform.position, _avoidIsland.transform.position) < 95f)
        {
            avoidanceMove = (Vector3)(agent.transform.position - _avoidIsland.transform.position);
        }
        //If we are too close to shark set direction to move away from shark
        else if (Vector3.Distance(agent.transform.position, _shark.transform.position) < 50f)
        {
            avoidanceMove = (Vector3)(agent.transform.position - _shark.transform.position);
        }
        //Otherwise don't adjust direction and exit function
        else
        {
            return Vector3.zero;
        }
        //Return new direction calculation for use in CompositeBehaviour
        return avoidanceMove;
    }
    #endregion
}
