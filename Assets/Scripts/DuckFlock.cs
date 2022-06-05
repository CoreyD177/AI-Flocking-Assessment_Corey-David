using System.Collections.Generic;
using UnityEngine;

public class DuckFlock : MonoBehaviour
{
    #region Variables
    //Script variables to allow us to access functions and variables from other classes
    [Header("Scripts")]
    [Tooltip("Add the duck prefab with the FlockAgent script attached here")]
    public DuckAgent duckPrefab;
    [Tooltip("Add the Composite Behaviour scriptable object for the appropriate flock here")]
    public DuckBehaviour duckBehaviour;
    //List of flock agents in the scene
    [Header("Flock Agents")]
    [Tooltip("List will populate itself on game load")]
    public List<DuckAgent> agents;
    //Speed and area variables
    [Header("Flock Population")]
    [Range(10, 500)]
    [Tooltip("How many ducks in flock to start with")]
    public int startingCount = 25;
    [Header("Flock Speed")]
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Header("Positioning")]
    [Range(1f, 20f)]
    [Tooltip("How far away do you want the duck to be looking for its neighbour ducks")]
    public float neighourRadius = 20f;
    [Range(0f, 1f)]
    [Tooltip("How close do you want ducks to be able to get")]
    public float avoidanceRadiusMultiplier = 0.5f;
    //Y position for duck so it can be constrained to the right level
    private float _duckYPosition = 494.3f;
    //Private variables to control flock speed and avoidance based off previous base values
    float _squareMaxSpeed;
    float _squareNeighbourRadius;
    float _squareAvoidanceRadius;
    //Property allowing us to retrieve the value of the _squareAvoidanceRadious from another script but not change it
    public float SquareAvoidanceRadius { get { return _squareAvoidanceRadius; } }
    #endregion
    private void Start()
    {
        //Set up the squared speed and radius variables
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighbourRadius = neighourRadius * neighourRadius;
        _squareAvoidanceRadius = _squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        //Instantiate the duck prefabs based on the starting count using the prefab that corresponds to the game object this script is attached to
        for (int i = 0; i < startingCount; i++)
        {
            DuckAgent newDuck = Instantiate(duckPrefab, new Vector3(Random.Range(-15,16), _duckYPosition, Random.Range(0,31)), Quaternion.identity, transform);
            newDuck.name = "Duck " + i;
            newDuck.Initialise(this);
            agents.Add(newDuck);
        }
    }
    #region Movement
    private void Update()
    {
        //For each duck in the scene
        foreach (DuckAgent agent in agents)
        {
            //Create a list of nearby ducks using the GetNearbyObjects function
            List<Transform> context = GetNearbyObjects(agent);
            //Retrieve movement value from DuckBehaviours CalculateMove function based of this duck and its list of neighbours
            Vector3 move = duckBehaviour.CalculateMove(agent, context, this);
            //Times movement by drivefactor to calculate speed to move
            move *= driveFactor;
            //If we are going to move quicker than the max speed bring speed back down
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            //Apply movement
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(DuckAgent agent)
    {
        //Create the list to be used in update
        List<Transform> context = new List<Transform>();
        //Create an array of ducks that reside within neighbour radius
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighourRadius);
        //For each duck in array add their transform to the context list
        foreach (Collider c in contextColliders)
        {
            if (c != agent.DuckCollider)
            {
                context.Add(c.transform);
            }
        }
        //Return the context list for use in update
        return context;
    }
    #endregion
    #region Restart Game
    public void Restart()
    {
        //If script is attached to the DuckFlock object kill all its children
        if (transform.name == "DuckFlock")
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        //Else kill all of GoldDuckFlocks children
        else
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        //Instantiate a new flock based on starting count using prefab that corresponds to game object this script is attached to
        for (int i = 0; i < startingCount; i++)
        {
            DuckAgent newDuck = Instantiate(duckPrefab, new Vector3(Random.Range(-15, 16), _duckYPosition, Random.Range(0, 31)), Quaternion.identity, transform);
            newDuck.name = "Duck " + i;
            newDuck.Initialise(this);
            agents.Add(newDuck);
        }
    }
    #endregion
}
