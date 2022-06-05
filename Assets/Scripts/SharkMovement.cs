using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    #region Variables
    //Speed variables for shark movement
    [Header("Speed")]
    private float _sharkSpeed = 12f;
    private float _sharkTurnSpeed = 3f;
    //Audio source for shark to make sound when he catches ducks
    [Header("Audio")]
    [Tooltip("Add the SharkAudio audio source from the SharkPrefab")]
    [SerializeField] private AudioSource _audioSource;
    //Animator controller reference
    private Animator _sharkAnimator;
    //Rigidbody reference
    private Rigidbody _sharkRigidbody;
    #endregion
    private void Start()
    {
        //Retrieve the animator component from the child of the GameObject this script is attached to
        _sharkAnimator = GetComponentInChildren<Animator>();
        //Retrieve Rigidbody component from the GameObject this script is attached to
        _sharkRigidbody = GetComponent<Rigidbody>();
    }
    #region Movement
    void Update()
    {
        //Move forward when Keypad8 is pressed
        if (Input.GetKey(KeyCode.Keypad8))
        {
            _sharkRigidbody.MovePosition(transform.position + transform.forward * _sharkSpeed * Time.deltaTime);
        }
        //Turn left when Keypad4 is pressed
        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Rotate(0, -_sharkTurnSpeed, 0);
        }
        //Turn right when Keypad8 is pressed
        if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + _sharkTurnSpeed, 0f);
        }
    }
    #endregion
    #region Duck Collisions
    private void OnTriggerEnter(Collider other)
    {
        //If we collide with a duck play sound and kill the duck
        if (other.tag == "Duck")
        {
            _audioSource.Play();
            Destroy(other.gameObject);
        }
        //If no more ducks remain trigger animation and freeze position of shark
        if (GameObject.Find("DuckFlock").transform.childCount <= 1 && GameObject.Find("GoldDuckFlock").transform.childCount <= 1)
        {
            _sharkRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _sharkAnimator.SetBool("isFull", true);
        }
    }
    #endregion
    #region Restart
    public void Restart()
    {
        //On restart reset shark to original position, reverse the animation and reset rigidbody constraints so shark can move
        _sharkAnimator.SetBool("isFull", false);
        _sharkRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
        transform.position = new Vector3(0f, 494.6f, -60f);
    }
    #endregion
}
