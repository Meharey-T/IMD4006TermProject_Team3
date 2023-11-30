using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainState : MonoBehaviour
{
   
    [SerializeField] private GameObject player;
    public PlayerMovement pMovement;
    public CapsuleCollider terrainCheck;
    public enum Speed
    {
        idle,
        walk,
        run,
        sneak
           // still

    }
    public Speed currSpeed = Speed.walk;

    public enum FloorType {
        Hardwood,
        Carpet
    }
    FloorType TerrainType;
    private bool isMoving;
   
    [Header("FootSteps")]
    public List<AudioClip> carpetSFX;
    public List<AudioClip> hardwoodSFX;

   private AudioSource footStepsSource;

    AudioClip clip = null;
    public string terrainTag;
    // Start is called before the first frame update


    void Start()
    {
        //pMovement.GetComponent<PlayerMovement>();
        terrainCheck = this.GetComponentInChildren<CapsuleCollider>();
        footStepsSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0 ||
Input.GetAxis("Horizontal") < 0)
        {
            isMoving = true;

        }
        else {isMoving = false; }


        GetTerrain();

        ////////////////////////////////////////////////currSpeed = GetSpeed();

    }
        //if (pMovement.groundedPlayer && terrainCheck.collision.gameObject.layer == 8)
        /*   if (pMovement.groundedPlayer)
           {

           footStepsSound.enabled = true;
       }*/
       /* if (Player.speed == walking)
         {

            switch (TerrainType)
            {
                case FloorType.Carpet:
                   carpetWalk.enabled = true;
                    Debug.Log("carpetfloor");
                    break;
                case FloorType.Hardwood:
                    hardWoodWalk.enabled = true;
                    Debug.Log("hardwoodfloor");
                    break;



            }
    







         }
         else
         {
             footStepsSound.enabled == false;
         }
       */

        
/*
       


    }*/
/*
    private void OnTriggerEnter(Collider other) {
        //if (other.gameObject.layer == 7 && pMovement.groundedPlayer)

       


        if (other.gameObject.layer == 7)
        {
           
            if (other.tag == "Carpet")
            {
                TerrainType = FloorType.Carpet;
                Debug.Log("This is carpet");
            }
            else if (other.tag == "HardWood")
            {
                TerrainType = FloorType.Hardwood;
                Debug.Log("This is hardwood");
            }
        }
        
        //we are walking
       if (currSpeed== Speed.walk ) { 
            //PlayWalkingSound();
        }
       //we are running
       else if (currSpeed == Speed.run)
        {
            //PlayRunningSound();
        }
       //we are sneakkng
     /*  else if (currSpeed == Speed.sneak)
            {
                PlaySneakingSound();
            }
     

    }

  public  float currSpeed() {
        return pMovement.currentSoundRadius;


    }

 
*/


    private FloorType GetTerrain()
    {
        if (terrainTag == "Carpet")
        {
            TerrainType = FloorType.Carpet;
            Debug.Log("This is carpet");
        }
        else if (terrainTag == "HardWood")
        {
            TerrainType = FloorType.Hardwood;
            Debug.Log("This is hardwood");
        }

        //Debug.Log(TerrainType);
        return TerrainType;
    }
    public void PlayWalkingSound()
    {

       
        if (isMoving)
        {
            clip = null;
            TerrainType = GetTerrain();

            switch (TerrainType)
            {
                
                case FloorType.Hardwood:
                    clip = hardwoodSFX[(int)Speed.walk];
                    Debug.Log("hardwoodfloor");
                    break;
                case FloorType.Carpet:
                    clip = carpetSFX[(int)Speed.walk];
                    Debug.Log("carpetfloor");
                    break;


            }
            footStepsSource.enabled = true;
            footStepsSource.clip = clip;
            footStepsSource.volume = 1;
            if (!footStepsSource.isPlaying)
            {
                footStepsSource.Play();
            }
        }
        else
        {
            PlayIdleSound();
        }
    }
   
  

    public void PlayRunningSound()
    {

        if (isMoving)
        {
            clip = null;

            TerrainType = GetTerrain();

            switch (TerrainType)
            {
                case FloorType.Carpet:
                    clip = carpetSFX[(int)Speed.run];
                    //Debug.Log("carpetfloor");
                    break;
                case FloorType.Hardwood:
                    clip = hardwoodSFX[(int)Speed.run];
                    //Debug.Log("hardwoodfloor");
                    break;



            }
            footStepsSource.enabled = true;
            footStepsSource.clip = clip;
            footStepsSource.volume = 1;
            if (!footStepsSource.isPlaying)
            {
                footStepsSource.Play();
            }
        }
        else
        {
            PlayIdleSound();
        }
    }




    /*
    public Speed GetSpeed ()
    {
        Debug.Log("my curr speed isss" + (Speed)pMovement.currSpeed);
        return (Speed)pMovement.currSpeed;
    }

    */

    public void PlaySneakingSound()
    {
        
        if(isMoving)
        {




            clip = null;

            TerrainType = GetTerrain();

            switch (TerrainType)
            {
                case FloorType.Carpet:
                    clip = carpetSFX[(int)Speed.sneak];
                    Debug.Log("carpetfloor");
                    break;
                case FloorType.Hardwood:
                    clip = hardwoodSFX[(int)Speed.sneak];
                    Debug.Log("hardwoodfloor");
                    break;



            }
            footStepsSource.enabled = true;
            footStepsSource.clip = clip;
            footStepsSource.volume = 1;

            if (!footStepsSource.isPlaying)
            {
                footStepsSource.Play();
            }
            
        }
        else
        {
            PlayIdleSound();
        }
    }

    public void PlayIdleSound()
    {


           
            footStepsSource.enabled=false;
            
        
    }

    public Speed GetWalk()
    {

        return Speed.walk;
    }
    public Speed GetRun()
    {

        return Speed.run;
    }
    public Speed GetSneak()
    {

        return Speed.sneak;
    }


}
