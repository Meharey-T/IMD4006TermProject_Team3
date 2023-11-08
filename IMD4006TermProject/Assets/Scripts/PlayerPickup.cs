using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{ //layer your player can pick up and move

    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] private Transform PickupTarget;
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {



            if (CurrentObj)
            {


                CurrentObj.useGravity = true;
                CurrentObj.drag = 0;
                CurrentObj = null;
                return;


            }









            {
                // Vector3 mousePos  = Input.mousePosition;
                //raycaster to ceck what the player has selected
                Ray CamRay = PlayerCamera.ScreenPointToRay(new Vector3(0.5F, 0.5F, 0F));


                //if so assign the rigid body we create to the target object + diable it's gravity
                // if (Physics.Raycast(CamRay, out RaycastHit HitInfo, PickupRange, PickupMask))

                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, PickupRange, PickupMask))

                {
                    CurrentObj = HitInfo.rigidbody;
                    CurrentObj.useGravity = false;
                    CurrentObj.drag = 5;
                    Debug.Log(HitInfo);

                    if (CurrentObj.tag == "Treasure")
                    {
                        Debug.Log("Found treasure");
                        //Figure out what kind of treasure we just found, act accordingly
                        //  this.coinCount += CurrentObj.GetComponent<Treasure>().treasureStats.coinValue;
                        if (CurrentObj.GetComponent<Treasure>().treasureStats.finalChest)
                        {
                            Debug.Log("Player has won");
                            // StartCoroutine(OnPlayerWon());
                        }
                        //Remove the coin from the scene
                        CurrentObj.GetComponent<Interactable>().Die();
                        //  coinCountTxt.text = "Coin Count: " + coinCount;

                        Debug.Log("this is treasure");
                    }






                }



            }
        }


    }

    private void FixedUpdate()
    {

        if (CurrentObj)
        {

            // Vector3 DirectionToPoint = PickupTarget.position - CurrentObj.position;
           //float DistanceToPoint = DirectionToPoint.magnitude;
            //CurrentObj.velocity = DirectionToPoint * 12f * DistanceToPoint;

           float lerpSpeed = 10f;
           Vector3 newPos = Vector3.Lerp(transform.position, PickupTarget.position, Time.deltaTime * lerpSpeed);
           CurrentObj.MovePosition(newPos);

        }

    }
}
