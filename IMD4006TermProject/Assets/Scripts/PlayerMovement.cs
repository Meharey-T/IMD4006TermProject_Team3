using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    //Rigidbody rb;
    //How fast we run. Set to serialized for easy access in the inspector
    [SerializeField] private float speed;
    [SerializeField] private float jumpAmount;

    private float gravityValue = -9.81f;

    public bool groundedPlayer = true;
    private Vector3 playerVelocity;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Stuff for rotation speed
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        Move();
        //Commented this out because it doesn't quite work how I wanted but it's close. Uncomment to see how it works
        //Turn();
        //Jump();
    }

    void Move()
    {
        //Accesses the basic movement controls. This works with WASD and also controller joysticks
        float translationZ = Input.GetAxisRaw("Vertical");// * speed;
        float translationX = Input.GetAxisRaw("Horizontal");// * speed;

        //Move according to current movement axis
        //transform.Translate(translationX, 0, translationZ);
        Vector3 direction = new Vector3(translationX, 0f, translationZ).normalized;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if(Input.GetButtonDown("Jump") /*&& groundedPlayer */)
        {
            playerVelocity.y += Mathf.Sqrt(jumpAmount * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /*
    void Turn()
    {
        //If we assigned this to the camera instead of the player I think it might work better, but I'm not sure if forward movement would update properly
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);
        
    }
    */

    /*
    void Jump()
    {
        if (Input.GetButtonDown("Jump")){
            Debug.Log("Jump!");
            
        }
    }
    */
}
