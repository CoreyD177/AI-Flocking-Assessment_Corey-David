using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Steered Cohesion Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion Behaviour")]
public class SteeredCohesionBehaviour : CohesionBehaviour
{
    //Variable for our current movement velocity
    private Vector3 currentVelocity;
    //Variable for the amount of smoothing to apply
    public float agentSmoothTime = 0.5f;

    //Update CohesionBehaviours CalculateMove function to apply smoothing to its movement
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //Take the calculation we have made in CohesionBehaviour class
        Vector3 cohesionMove = base.CalculateMove(agent, context, flock);
        //Apply smoothing to calculation based off our smoothin amount
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        //Return new smoothed calculation for use in CompositeBehaviour
        return cohesionMove;
    }
}
