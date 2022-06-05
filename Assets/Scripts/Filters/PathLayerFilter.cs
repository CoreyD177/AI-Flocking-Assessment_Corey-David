using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Path Layer Filter
[CreateAssetMenu(menuName = "Flock/Filter/Path Layer")]
public class LayerFilter : ContextFilter
{
    //Create a variable so you can set the Layer mask we are looking for in Unity
    public LayerMask mask;
    //Edit the ContextFilters Filter list to contain the entries we want for this filter
    public override List<Transform> Filter(DuckAgent agent, List<Transform> original)
    {
        //Create a new filtered list
        List<Transform> filtered = new List<Transform>();
        //For each item in the original neighbour collider list
        foreach (Transform item in original)
        {
            //If item has the mask we are looking for add it to the filtered list
            if (0 != (mask & (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }
        //Return filtered list
        return filtered;
    }
}
