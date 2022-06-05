using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Cohesion Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion Behaviour")]
public class CohesionBehaviour : DuckBehaviour
{
    #region Movement
    //Update DuckBehaviours CalculateMove function using our new calculations
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //If we have no neighbours don't add anything to direction and exit function
        if (context.Count == 0)
        {
            return Vector3.zero;            
        }
        //Set current movement direction to nowhere
        Vector3 cohesionMove = Vector3.zero;
        //Create a filtered list of neighbours using our applied filter or just use original list if no filter exists
        List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
        //Start a counter at 0
        int count = 0;
        //For each neighbour we have add their current position to our movement direction so we can drift towards them and stick together and increment counter
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector3)item.position;
            count++;
        }
        //Divide our movement by the amount of neighbours we have to average out direction
        if (count != 0)
        {
            cohesionMove /= count;
        }
        //Take away our own position from the calculation
        cohesionMove -= (Vector3)agent.transform.position;
        //Return the calculated movement direction for use in SteeredCohesionBehaviour
        return cohesionMove;
    }
    #endregion
}
