using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Make a scriptable object to be referenced by created behaviours
public abstract class DuckBehaviour : ScriptableObject
{
    //CalculateMove function to be updated by the CompositeBehaviour behaviour
    public abstract Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock);
    //Filter to be applied for the behaviour being created
    [Header("Filter")]
    [Tooltip("Add the filter you want to be applied to this behaviour. Can be left blank.")]
    public ContextFilter filter;
}
