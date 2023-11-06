using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{
    //public CharacterController controller;
    public Transform cam;
    [SerializeField] private GameObject rotator;
    public Vector2 turnMinMax = new Vector2(-40, 85);

    //How fast we run. Set to serialized for easy access in the inspector
    [Header("Movement speeds")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sneakSpeed;

    //Moving doesn't happen at perfect instant snaps
    [Header("Smoothing settings")]
    private float speedSmoothVelocity;
    [SerializeField] private float speedSmoothTime;
    private float modalSpeed;
    private float currentSpeed;
    private float targetSpeed;

    //Affects our jump variables
    [Header("Jump feel")]
    [SerializeField] private float jumpAmount;
    public bool groundedPlayer = true;

    //Affects how fast the player turns, how they can turn
    [Header("Turning feel")]
    private Vector2 turn = new Vector2(-180, 0);
    [SerializeField] private float turnSpeed;
    private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime;

    private float gravityValue = -9.81f;

    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
    }

    void Move()
    {
        //Accesses the basic movement controls. This works with WASD and also controller joysticks
        float translationZ = Input.GetAxisRaw("Vertical");// * speed;
        float translationX = Input.GetAxisRaw("Horizontal");// * speed;

        //Move according to current movement axis
        Vector3 direction = new Vector3(translationX, 0f, translationZ).normalized;

        //Ensures the player stops moving up or down when on the ground
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
        }
        //Factor velocity into up and down motion while jumping
        playerVelocity.y += gravityValue * Time.deltaTime;

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

        //controller.Move(playerVelocity * Time.deltaTime);
    }

    void Turn()
    {
        //Rotate the central rotation piece based on mouse position
        turn.x += Input.GetAxis("Mouse X") * turnSpeed;
        turn.y += Input.GetAxis("Mouse Y") * turnSpeed;
        turn.y = Mathf.Clamp(turn.y, turnMinMax.x, turnMinMax.y);
        rotator.transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
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
