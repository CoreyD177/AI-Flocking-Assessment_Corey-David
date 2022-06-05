using System.Collections.Generic; //Needed for lists
using UnityEngine; //Needed for Unity connection
//Create entry in Unity asset creation menu for Same Flock Filter
[CreateAssetMenu(menuName = "Flock/Filter/Same Flock Filter")]
public class SameFlockFilter : ContextFilter
{
    //Edit ContextFilters Filter list using values we are currently filtering for
    public override List<Transform> Filter(DuckAgent agent, List<Transform> original)
    {
        //Create new filtered list
        List<Transform> filtered = new List<Transform>();
        //For each collider in original neighbour list
        foreach (Transform item in original)
        {
            //Get DuckAgent class from item
            DuckAgent itemAgent = item.GetComponent<DuckAgent>();
            //If we have a DuckAgent class from this item
            if (itemAgent != null)
            {
                //If neighbour item is in the same flock as this duck add it to the filtered list
                if (itemAgent.DuckFlock == agent.DuckFlock)
                {
                    filtered.Add(item);
                }
            }
        }
        //Return the filtered list for use
        return filtered;
    }
}
