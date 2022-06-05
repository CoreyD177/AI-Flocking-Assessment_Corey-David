using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Composite Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/CompositeBehaviour")]
public class CompositeBehaviour : DuckBehaviour
{
    #region Variables
    //Struct allowing for Behaviours to be added along with the weight of their importance
    [System.Serializable]
    public struct BehaviourGroup
    {
        public DuckBehaviour behaviour;
        public float weights;
    }
    //Create an array from the struct so we can add entries in Unity
    [Header("Flock Behaviours")]
    [Tooltip("Add each behaviour you want this flock to have and assign it an importance weight")]
    public BehaviourGroup[] behaviours;
    #endregion
    #region Movement
    //Update DuckBehaviours CalculateMove function so it can give the movement to the DuckAgent based off this calculation
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //Set move direction to nowhere
        Vector3 move = Vector3.zero;
        //For each behaviour we have added
        foreach (BehaviourGroup behave in behaviours)
        {
            //Create a partial movement value taking into account the movement calculation of the actual behaviour and its importance
            Vector3 partialMove = behave.behaviour.CalculateMove(agent, context, flock) * behave.weights;
            //If we have a direction to move now make sure we are not moving there too fast
            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > behave.weights * behave.weights)
                {
                    partialMove.Normalize();
                    partialMove *= behave.weights;
                }
            }
            //Add this behaviours movement calculation to the total movement
            move += partialMove;
        }
        //Once we have added all movement calculations return the total so DuckAgent can use it to move
        return move;
    }
    #endregion
}
