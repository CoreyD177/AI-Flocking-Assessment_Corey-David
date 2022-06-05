using System.Collections.Generic; //Required for Lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Aviodance Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance Behaviour")]
public class AvoidanceBehaviour : DuckBehaviour
{
    #region Movement
    //Update DuckBehaviours CalculateMove function using our new calculations
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //If we don't have any neighbours don't add anything to direction and exit function
        if (context.Count == 0)
        {
            return Vector3.zero;
        }
        //Set current move direction to nowhere
        Vector3 avoidanceMove = Vector3.zero;
        //Create new filtered list of neighbours using our applied filter or use original list if filter doesn't exist
        List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
        //Start a counter at 0
        int count = 0;
        //For each neighbour we have if they are within our avoidance radius take their position away from ours and increment the counter
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) <= flock.SquareAvoidanceRadius)
            {
                avoidanceMove += (Vector3)(agent.transform.position - item.position);
                count++;
            }

        }
        //Divide our current direction by the amount of neighbours we have to average out direction
        if (count != 0)
        {
            avoidanceMove /= count;
        }
        //Return calculated movement for use in CompositeBehaviour
        return avoidanceMove;
    }
    #endregion
}
