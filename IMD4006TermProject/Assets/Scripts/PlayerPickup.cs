using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note make sure you turn of collision for the players sphere collider and the pick up layer
public class PlayerPickup : MonoBehaviour
{ //layer your player can pick up and move

    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    private Player player;
    [SerializeField] private Transform PlayerCameraTransform;
    [SerializeField] private Transform PickupTarget;
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObj;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        Renderer rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CurrentObj)
            {
                CurrentObj.useGravity = true;
                CurrentObj.drag = 0;
                CurrentObj = null;
                return;
            }

            // Vector3 mousePos  = Input.mousePosition;
            //raycaster to ceck what the player has selected
            // Ray CamRay = PlayerCamera.ScreenPointToRay(new Vector3(0.5F, 0.5F, 0F));

            //Rays from the camera 0 represents the bottom or left and 1 represents the top or right of the view).
            // Ray CamRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.65F, 0F));
            Ray CamRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
            //if so assign the rigid body we create to the target object + diable it's gravity
            //if (Physics.Raycast(CamRay, out RaycastHit HitInfo, PickupRange, PickupMask))

            {
                //if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit HitInfo, PickupRange, PickupMask))
                if (Physics.Raycast(CamRay, out RaycastHit HitInfo, PickupRange, PickupMask))
                {
                    
                    CurrentObj = HitInfo.rigidbody;
                    CurrentObj.useGravity = false;
                    CurrentObj.isKinematic = false;
                    CurrentObj.drag = 5;
                    
                    if (CurrentObj.gameObject.tag == "Treasure")
                    {

                        //Figure out what kind of treasure we just found, act accordingly
                        player.coinCount += CurrentObj.GetComponent<Treasure>().treasureStats.coinValue;
                        for(int i = 0; i < player.enemySet.Items.Count; i++)
                        {
                            player.enemySet.Items[i].GetComponent<Enemy>().treasureLeft -= CurrentObj.GetComponent<Treasure>().treasureStats.coinValue;
                        }
                        if (CurrentObj.GetComponent<Treasure>().treasureStats.finalChest)
                        {
                            StartCoroutine(player.OnPlayerWon());
                        }
                        //Remove the coin from the scene
                        CurrentObj.GetComponent<Interactable>().Die();
                        player.coinCountTxt.text = "Coin Count: " + player.coinCount;

                        /*
                        //Figure out what kind of treasure we just found, act accordingly
                        //  this.coinCount += CurrentObj.GetComponent<Treasure>().treasureStats.coinValue;
                        if (CurrentObj.GetComponent<Treasure>().treasureStats.finalChest)
                        {
                            // StartCoroutine(OnPlayerWon());
                        }
                        //Remove the coin from the scene
                        CurrentObj.GetComponent<Interactable>().Die();
                        //  coinCountTxt.text = "Coin Count: " + coinCount;
                        */
                    }


                }

            }
        }




    }

    private void FixedUpdate()
    {

        if (CurrentObj)
        {

            Vector3 DirectionToPoint = PickupTarget.position - CurrentObj.position;
            float DistanceToPoint = DirectionToPoint.magnitude;
            CurrentObj.velocity = DirectionToPoint * 12f * DistanceToPoint;

            //float lerpSpeed = 10f;
            //Vector3 newPos = Vector3.Lerp(transform.position, PickupTarget.position, Time.deltaTime * lerpSpeed);
            // CurrentObj.MovePosition(newPos);

        }

    }



}
