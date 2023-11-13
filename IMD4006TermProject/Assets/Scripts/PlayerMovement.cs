using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{
    //Camera related values
    public Transform cam;
    [SerializeField] private GameObject rotator;
    public Vector2 turnMinMax = new Vector2(-40, 85);

    //How fast we run. Set to serialized for easy access in the inspector
    [Header("Movement speeds")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sneakSpeed;

    //Values for modifying movement speeds
    [Header("Smoothing settings")]
    private float speedSmoothVelocity;
    [SerializeField] private float speedSmoothTime;
    private float modalSpeed;
    private float currentSpeed;
    private float targetSpeed;

    //Affects our jump variables
    [Header("Jump feel")]
    [SerializeField] private float jumpAmount;
    [SerializeField] private float gravityScale;
    //Vector3 jumpVector;
    public bool groundedPlayer = true;

    //Affects how fast the player turns, how they can turn
    [Header("Turning feel")]
    private Vector2 turn = new Vector2(-180, 0);
    [SerializeField] private float turnSpeed;
    private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime;

    //Component references
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        //jumpVector = new Vector3(0, jumpAmount, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Reset();
        Jump();
    }

    //Fixed update should be roughly the same no matter what your framerate is
    private void FixedUpdate()
    {
        //Adds a higher downward force on the player, helps jump feel more snappy
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    private void LateUpdate()
    {
        //Failsafe to keep the player on the level and not falling through the floor
        if (this.gameObject.transform.position.y < 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }

    private void Reset()
    {
        if (Input.GetKeyDown("r"))
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }

    void Move()
    {
        //Accesses the basic movement controls. This works with WASD and also controller joysticks
        //translationZ will be 1 when pressing W and -1 on S. X will be 1 with D and -1 with A
        float translationZ = Input.GetAxisRaw("Vertical");
        float translationX = Input.GetAxisRaw("Horizontal");

        //Debug.Log(translationZ + ", " + translationX);

        //Set player direction vector based on the axis they are moving in
        Vector3 direction = new Vector3(translationX, 0f, translationZ).normalized;

        //Handles rotating if player is moving into the direction they're traveling to
        if (direction != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotator.transform.eulerAngles.y;
            //Debug.Log(targetRotation);
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        //Start calculating the walk speed
        targetSpeed = modalSpeed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //Handles moving in multiple directions
        ResolveDirectionalMovement(translationX, translationZ);
    }

    //Finishes movement based on which axes are active
    void ResolveDirectionalMovement(float translationX, float translationZ)
    {
        //This block is forward motion, Z axis
        //If Z is positive and no movement on X axis
        if (translationZ > 0f && translationX == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        //If Z is positive and moving sideways
        else if (translationZ > 0f && translationX != 0f)
        {
            transform.Translate(transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }
        //If Z is negative and no sideways movement
        if (translationZ < 0 && translationX == 0f)
        {
            transform.Translate(-transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        //If Z is negative and moving sideways
        else if (translationZ < 0f && translationX != 0f)
        {
            transform.Translate(-transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }

        //Block for right-side motion, X axis
        //If X is positive and no movement front or back
        if (translationX > 0f && translationZ == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        //If X is positive and yes movement front or back
        else if (translationX > 0f && translationZ != 0f)
        {
            transform.Translate(transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }
        //If X is negative and no movement front or back
        if (translationX < 0f && translationZ == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        //If X is negative and yes movement front or back
        else if (translationX < 0f && translationZ != 0f)
        {
            transform.Translate(transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }
    }
    
    void Turn()
    {
        //Rotate the central camera rotation piece based on mouse position
        turn.x += Input.GetAxis("Mouse X") * turnSpeed;
        turn.y += Input.GetAxis("Mouse Y") * turnSpeed;
        turn.y = Mathf.Clamp(turn.y, turnMinMax.x, turnMinMax.y);
        rotator.transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }

    void Jump()
    {
        //If player presses jump, jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
    }

    public float getBaseSpeed()
    {
        return baseSpeed;
    }

    public float getSprintSpeed()
    {
        return sprintSpeed;
    }

    public float getSneakSpeed()
    {
        return sneakSpeed;
    }

    public void setModalSpeed(float speed)
    {
        modalSpeed = speed;
    }
}
