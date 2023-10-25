using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    [SerializeField] private GameObject rotator;
    public Vector2 turnMinMax = new Vector2(-40, 85);

    //How fast we run. Set to serialized for easy access in the inspector
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sneakSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float speedSmoothVelocity;
    [SerializeField] private float speedSmoothTime;
    [SerializeField] private float jumpAmount;
    float targetSpeed;

    bool isWalking = true;
    bool isSprinting;
    bool isSneaking;

    private float gravityValue = -9.81f;

    public bool groundedPlayer = true;
    private Vector3 playerVelocity;

    //Turning variables
    public float turnSpeed;
    float turnSmoothVelocity;
    float turnSmoothTime = 0.2f;
    public Vector2 turn = new Vector2(-180, 0);

    //Stuff for rotation speed
    //public float horizontalSpeed = 2.0f;
    //public float verticalSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        Move();
        Turn();
        //Jump();
    }

    void Move()
    {
        //Accesses the basic movement controls. This works with WASD and also controller joysticks
        
        float translationZ = Input.GetAxisRaw("Vertical");// * speed;
        float translationX = Input.GetAxisRaw("Horizontal");// * speed;
        //Vector2 translateDir = new Vector2(translationX, translationZ).normalized;

        //Move according to current movement axis
        //transform.Translate(translationX, 0, translationZ);
        Vector3 direction = new Vector3(translationX, 0f, translationZ).normalized;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //Handles rotating if player is moving
        if (direction != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotator.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        //If player presses jump, jump
        if(Input.GetButtonDown("Jump") /*&& groundedPlayer */)
        {
            playerVelocity.y += Mathf.Sqrt(jumpAmount * -3.0f * gravityValue);
            //controller.Move().
        }
        //Factor velocity into up and down motion while jumping
        playerVelocity.y += gravityValue * Time.deltaTime;
        //Actually start calculating movement speed
        if (isWalking)
        {
            targetSpeed = baseSpeed;
        }
        else if (isSprinting)
        {
            targetSpeed = sprintSpeed;
        }
        else if (isSneaking)
        {
            targetSpeed = sneakSpeed;
        }
        targetSpeed = targetSpeed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        //controller.Move(playerVelocity * Time.deltaTime);
    }

    void Turn()
    {
        //Rotate the central rotation piece based on mouse position
        turn.x += Input.GetAxis("Mouse X") * turnSpeed;
        turn.y += Input.GetAxis("Mouse Y") * turnSpeed;
        turn.y = Mathf.Clamp(turn.y, turnMinMax.x, turnMinMax.y);
        rotator.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        
    }

    /*
    void Jump()
    {
        if (Input.GetButtonDown("Jump")){
            Debug.Log("Jump!");
            
        }
    }
    */
}
