using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Alignment Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment Behaviour")]
public class AlignmentBehaviour : DuckBehaviour
{
    #region Movement
    //Update DuckBehaviours CalculateMove function with new movement calculations
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //If we don't have any neighbours just move forward and exit function
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }
        //Set current movement to nowhere
        Vector3 alignmentMove = Vector3.zero;
        //Create new list of neighbours using applied filter or just use full list if filter not present
        List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
        //Start a count at 0
        int count = 0;
        //For each neighbour in our list add their forward direction to our current direction and increment our counter
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector3)item.transform.forward;
            count++;
        }
        //Divide our total movement calculation by the amount of neighbours we have to average out direction
        if (count != 0)
        {
            alignmentMove /= count;
        }
        //Return our calculated direction for use in CompositeBehaviour
        return alignmentMove;
    }
    #endregion
}
