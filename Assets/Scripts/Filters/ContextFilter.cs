using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Make it a scriptable object so it can be accessed without being attached to a GameObject
public abstract class ContextFilter : ScriptableObject
{
    //Create abstract list of transforms to be referenced by filters that inherit from this class
    public abstract List<Transform> Filter(DuckAgent agent, List<Transform> original);
}
