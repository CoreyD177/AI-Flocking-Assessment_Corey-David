using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Stay In Radius Behaviour
[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius Behaviour")]
public class StayInAreaBehaviour : DuckBehaviour
{
    #region Variables
    //Radius variables for the circle we want ducks to stay within
    [Header("Radius Variables")]
    [Tooltip("Set the centre location for the circle you want ducks to remain in")]
    [SerializeField] private Vector3 _center = new Vector3(-35f, 494.3f, -60);
    [Tooltip("Set the radius for the circle you want the ducks to remain in")]
    [SerializeField] private float _radius = 375f;
    #endregion
    #region Movement
    //Adjust DuckBehaviours CalculateMove function using our new calculations
    public override Vector3 CalculateMove(DuckAgent agent, List<Transform> context, DuckFlock flock)
    {
        //Direction to centre of circle
        Vector3 centerOffset = _center - (Vector3)(agent.transform.position);
        //How far are we away from the centre of the circle
        float t = centerOffset.magnitude / _radius;
        //If we are within 90% of the radius don't adjust direction and exit function
        if (t < 0.9f)
        {
            return Vector3.zero;
        }
        //Adjust our movement direction so we don't leave area and return the value for use in CompositeBehaviour
        return centerOffset * t * t;
    }
    #endregion
}
