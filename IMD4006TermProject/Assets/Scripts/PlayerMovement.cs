using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    //How fast we run. Set to serialized for easy access in the inspector
    [SerializeField] private float speed;
    [SerializeField] private float jumpAmount;

    //Stuff for rotation speed
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Commented this out because it doesn't quite work how I wanted but it's close. Uncomment to see how it works
        //Turn();
        Jump();
    }

    void Move()
    {
        //Accesses the basic movement controls. This works with WASD and also controller joysticks
        float translationZ = Input.GetAxis("Vertical") * speed;
        float translationX = Input.GetAxis("Horizontal") * speed;

        //Move according to current movement axis
        transform.Translate(translationX, 0, translationZ);
    }

    void Turn()
    {
        //If we assigned this to the camera instead of the player I think it might work better, but I'm not sure if forward movement would update properly
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);
        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")){
            Debug.Log("Jump!");
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
    }
}
