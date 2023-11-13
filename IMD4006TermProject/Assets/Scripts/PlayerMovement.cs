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
    Vector3 jumpVector;
    public bool groundedPlayer = true;

    //Affects how fast the player turns, how they can turn
    [Header("Turning feel")]
    private Vector2 turn = new Vector2(-180, 0);
    [SerializeField] private float turnSpeed;
    private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime;

    private float gravityValue = -9.81f;

    private Vector3 playerVelocity;

    //Component references
    private CapsuleCollider groundDetector;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        groundDetector = GetComponentInChildren<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        jumpVector = new Vector3(0, jumpAmount, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Reset();
        Jump();
    }

    private void LateUpdate()
    {
        //Adds a higher downward force on the player, helps jump feel more snappy
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
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
        float translationZ = Input.GetAxisRaw("Vertical");// * speed;
        float translationX = Input.GetAxisRaw("Horizontal");// * speed;

        //Move according to current movement axis
        Vector3 direction = new Vector3(translationX, 0f, translationZ).normalized;

        //Handles rotating if player is moving
        if (direction != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotator.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        //Start calculating the walk speed
        targetSpeed = modalSpeed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        
        //Handles moving in multiple directions
        if(translationZ > 0f && translationX == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        } else if(translationZ > 0f && translationX != 0f)
        {
            transform.Translate(transform.forward * currentSpeed/2 * Time.deltaTime, Space.World);
        }
        if(translationZ < 0 && translationX == 0f)
        {
            transform.Translate(-transform.forward * currentSpeed * Time.deltaTime, Space.World);
        } else if (translationZ < 0f && translationX != 0f)
        {
            transform.Translate(-transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }

        if (translationX > 0f && translationZ == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }else if (translationX > 0f && translationZ != 0f)
        {
            transform.Translate(transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }
        if (translationX < 0f && translationZ == 0f)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }else if (translationX < 0f && translationZ != 0f)
        {
            transform.Translate(transform.forward * currentSpeed / 2 * Time.deltaTime, Space.World);
        }

        //transform.Translate(playerVelocity * Time.deltaTime);
    }
    
    void Turn()
    {
        //Rotate the central rotation piece based on mouse position
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
