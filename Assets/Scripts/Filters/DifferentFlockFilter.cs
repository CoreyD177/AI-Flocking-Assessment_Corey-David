using System.Collections.Generic; //Required for lists
using UnityEngine; //Required for Unity connection
//Create entry in Unity asset creation menu for Differen Flock Filter
[CreateAssetMenu(menuName = "Flock/Filter/Different Flock")]
public class DifferentFlockFilter : ContextFilter
{
    //Edit ContextFilters Filter list using entries we are currently filtering for
    public override List<Transform> Filter(DuckAgent agent, List<Transform> original)
    {
        //Create new filtered list
        List<Transform> filtered = new List<Transform>();
        //For each item in the original neighbour list
        foreach (Transform item in original)
        {
            //Get DuckAgent class from item
            DuckAgent itemAgent = item.GetComponent<DuckAgent>();
            //If we have a DuckAgent class from current item
            if (itemAgent != null)
            {
                //If neighbour item is not in the same flock as this duck add it to filtered list
                if (itemAgent.DuckFlock != agent.DuckFlock)
                {
                    filtered.Add(item);
                }
            }
        }
        //Return filtered list
        return filtered;
    }
}