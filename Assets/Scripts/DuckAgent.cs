using UnityEngine; //Required for unity connection
//Require a collider to be attached to the game object this script is attached to
[RequireComponent(typeof(Collider))]
public class DuckAgent : MonoBehaviour
{
    #region Variables
    //Reference the DuckFlock class so we can use its functions
    private DuckFlock _duckFlock;
    //Create a public property so other classes can access data from DuckFlock but not modify it
    public DuckFlock DuckFlock { get => _duckFlock; }
    //Variable to store the Collider component of this game object
    private SphereCollider _duckCollider;
    //Create a public property so other classes can access this Collider variable but not modify it
    public SphereCollider DuckCollider { get => _duckCollider; }
    #endregion
    #region Setup & Destroy
    void Start()
    {
        //Retrieve the Collider from the game object and store it
        _duckCollider = GetComponent<SphereCollider>();
    }
    //When activated by the DuckFlock class add this duck to that flock
    public void Initialise(DuckFlock flock)
    {
        _duckFlock = flock;
    }
    public void OnDestroy()
    {
        //When this duck is destroyed search the agents list for this duck, delete it from list then break search operation
        foreach (DuckAgent agent in _duckFlock.agents)
        {
            if (agent == this)
            {
                _duckFlock.agents.Remove(agent);
                break;
            }
        }
    }
    #endregion
    #region Movement
    private void Update()
    {
        //If our Y position or x rotation have moved from where we want them return them to the desired position
        if (transform.position.y != 494.3f || transform.rotation.x != 0f)
        {
            transform.position = new Vector3(transform.position.x, 494.3f, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
    //Apply movement from our CompositeBehaviour
    public void Move(Vector3 velocity)
    {
        //Turn duck to desired rotation based off calculated direction
        transform.forward = velocity.normalized;
        //Use calculated direction to move duck forward
        transform.position += velocity * Time.deltaTime;
    }
    #endregion
}