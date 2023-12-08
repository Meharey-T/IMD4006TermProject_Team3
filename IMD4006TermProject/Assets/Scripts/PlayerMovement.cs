using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Functionality for player movement, rotation, orientation, etc
public class PlayerMovement : MonoBehaviour
{
    //Camera related values
    public Transform cam;
    [SerializeField] private GameObject rotator;
    public Vector2 turnMinMax = new Vector2(-40, 85);

    //How fast we run. Set to serialized for easy access in the inspector
    [Header("Movement speeds")]
    //am setting to public to help keep things consistent in terrain state
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sneakSpeed;

    //stats for sprinting - or not sprinting
    private int maxStamina = 150;
    public int currentStamina = 150;
    public bool consumingStamina = false;
    public bool winded = false;
    /*
    public enum Speed
    {
        idle,
        walk,
        run//,
           //sneak,
           // still

    }
    public Speed currSpeed;*/

    //Values for modifying movement speeds
    [Header("Smoothing settings")]
    private float speedSmoothVelocity;
    [SerializeField] private float speedSmoothTime;
   
    private float modalSpeed;
    private float currentSpeed;
    private float targetSpeed;

    //Affects our jump variables
    [Header("Jump feel")]
    [SerializeField] public float jumpAmount;
    [SerializeField] private float gravityScale;
    //Vector3 jumpVector;
    public bool groundedPlayer = true;
    public bool isRolling = false;
    public bool isJumping = false;

    //Affects how fast the player turns, how they can turn
    [Header("Turning feel")]
    [SerializeField] private float turnSpeed;
    private Vector2 turn = new Vector2(-180, 0);
    private float turnSmoothVelocity;
    [SerializeField] public float turnSmoothTime;
    public float turnSmoothTimeSnappy = 0.05f;
    public float turnSmoothTimeSlow = 0.5f;

    private float tiltSmoothVelocityX;
    private float tiltSmoothVelocityZ;
    [SerializeField] private float tiltSmoothTime;

    [Header("Sound Settings")]
    //Sound settings
    public float sneakSoundRadius = 1;
    public float walkSoundRadius = 5;
    public float sprintSoundRadius = 9;
    public float currentSoundRadius = 5;

    [Header("Terrain Mulipier")]
    

    //Component references
    public Rigidbody rb;
    public SphereCollider soundRadius;
    [SerializeField] private GameObject playerGeo;
    [SerializeField] private Image staminaBar;
    [SerializeField] public PlayerAnimator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        soundRadius = this.GetComponentInChildren<SphereCollider>();
        staminaBar.fillMethod = Image.FillMethod.Horizontal;
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Reset();
        ResolveSoundRadius();
        UpdateStaminaDisplay();
    }

    //Fixed update should be roughly the same no matter what your framerate is
    private void FixedUpdate()
    {
        //Adds a higher downward force on the player, helps jump feel more snappy
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        if(consumingStamina && (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && currentStamina > 0){
            currentStamina--;
        }
        else if(currentStamina <= 0 && !winded)
        {
            winded = true;
        }
        else if(currentStamina < maxStamina)
        {
            currentStamina++;
        }
    }

    private void LateUpdate()
    {
        //Failsafe to keep the player on the level and not falling through the floor
        if (this.gameObject.transform.position.y < 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }
    
    //Failsafe to put the player back at the start position
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
            //figures out the required y axis angle to turn towards the direction we're going to move in
            float targetRotationY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + rotator.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotationY, ref turnSmoothVelocity, turnSmoothTime);
        }
        //Start calculating the walk speed
        targetSpeed = modalSpeed * direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //Rotate the player geometry based on the direction the character is moving
        float targetRotationX = -2.5f * currentSpeed;
        float targetRotationZ = Mathf.Atan2(direction.x, 0) * Mathf.Rad2Deg * 0.01f * currentSpeed;
        playerGeo.transform.localEulerAngles = new Vector3(Mathf.SmoothDampAngle(playerGeo.transform.eulerAngles.x, targetRotationX, ref tiltSmoothVelocityX, tiltSmoothTime),
            0, Mathf.SmoothDampAngle(playerGeo.transform.eulerAngles.z, targetRotationZ, ref tiltSmoothVelocityZ, tiltSmoothTime));

        //Handles moving in multiple directions
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        TerrainState terraine = GetComponent<TerrainState>();

        //terraine.PlayWalkingSound();
    }
    
    void Turn()
    {
        //Rotate the central camera rotation piece based on mouse position
        turn.x += Input.GetAxis("Mouse X") * turnSpeed;
        turn.y += Input.GetAxis("Mouse Y") * turnSpeed;
        turn.y = Mathf.Clamp(turn.y, turnMinMax.x, turnMinMax.y);
        rotator.transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }

    public IEnumerator ResetRoll()
    {
        yield return new WaitForSeconds(1f);
        isRolling = false;
    }

    public IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = false;

    }

    private void ResolveSoundRadius()
    {
        //When we add more sound functionality we're going to put a multiplier on this base on the surface you're traveling over
        soundRadius.radius = currentSoundRadius;
    }

    private void UpdateStaminaDisplay()
    {
        
        float staminaPercent = (float)currentStamina / maxStamina;
        //Debug.Log(staminaPercent);
        staminaBar.fillAmount = staminaPercent;
    }

    public IEnumerator CatchBreath()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;
        winded = false;
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
/*
    public Speed GetWalk()
    {

        return Speed.walk;
    }
    public Speed GetRun()
    {

        return Speed.run;
    }*/
}
