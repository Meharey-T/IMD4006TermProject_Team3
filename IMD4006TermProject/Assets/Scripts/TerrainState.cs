using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainState : MonoBehaviour
{ //get player sound collider 
    //check player movement ... walk action sneak action 
   
    [SerializeField] private GameObject player;
    public PlayerMovement pMovement;
    public CapsuleCollider terrainCheck;
    public SphereCollider soundRadius;
    [SerializeField] public float hardwoodMultiplier = (float)1.1;
    [SerializeField] public float cobblestoneMultiplier = (float)1;
    [SerializeField] public float crateMultiplier = (float)1.1;
    [SerializeField] public float carpetMultiplier = (float)0.5;
    public enum Speed
    {
        idle,
        walk,
        run,
        sneak
         

    }
    public Speed currSpeed = Speed.walk;

    public enum FloorType {
        Hardwood,
        Carpet,
        CobbleStone,
        Crate
    }
    FloorType TerrainType;
    private bool isMoving;
   
    [Header("FootSteps")]
    public List<AudioClip> carpetSFX;
    public List<AudioClip> hardwoodSFX;
    public List<AudioClip> cobblestoneSFX;
    public List<AudioClip> crateSFX;

    private AudioSource footStepsSource;

    AudioClip clip = null;
    public string terrainTag;
    // Start is called before the first frame update


    void Start()
    {
        //pMovement.GetComponent<PlayerMovement>();
        terrainCheck = this.GetComponentInChildren<CapsuleCollider>();
        footStepsSource = GetComponent<AudioSource>();
        //soundRadius = this.GetComponentInChildren<SphereCollider>();
       
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
       


    private FloorType GetTerrain()
    {
        if (terrainTag == "Carpet")
        {

            TerrainType = FloorType.Carpet;
        }
        else if (terrainTag == "HardWood")
        {
            TerrainType = FloorType.Hardwood;
        }
        if (terrainTag == "CobbleStone")
        {
            TerrainType = FloorType.CobbleStone;
        }
        else if (terrainTag == "Crate")
        {
            TerrainType = FloorType.Crate;
        }
        return TerrainType;


    }// Set sound radius based on the terrain. Use a floortype mulitplier and base it on the player speed
     //play sound FX of walking
    public void PlayWalkingSound()
    {

       
        if (isMoving)
        {
            clip = null;
            TerrainType = GetTerrain();

            switch (TerrainType)
            {
                
                case FloorType.Hardwood:
                    pMovement.currentSoundRadius= pMovement.walkSoundRadius * hardwoodMultiplier;
                    clip = hardwoodSFX[(int)Speed.walk];
                    break;
                case FloorType.Carpet:
                    pMovement.currentSoundRadius = pMovement.walkSoundRadius * carpetMultiplier;
                    clip = carpetSFX[(int)Speed.walk];
                    break;
                case FloorType.CobbleStone:
                    pMovement.currentSoundRadius = pMovement.walkSoundRadius * cobblestoneMultiplier;
                    clip = cobblestoneSFX[(int)Speed.walk];
                    break;
                case FloorType.Crate:
                    pMovement.currentSoundRadius = pMovement.walkSoundRadius * crateMultiplier;
                    clip = crateSFX[(int)Speed.walk];
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

    // Set sound radius based on the terrain. Use a floortype mulitplier and base it on the player speed
    //play sound FX of running

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
                    pMovement.soundRadius.radius = pMovement.sprintSoundRadius * hardwoodMultiplier;
                    break;
                case FloorType.Hardwood:
                    pMovement.soundRadius.radius = pMovement.sprintSoundRadius * carpetMultiplier;
                    clip = hardwoodSFX[(int)Speed.run];
                    break;
                case FloorType.CobbleStone:
                    pMovement.soundRadius.radius = pMovement.sprintSoundRadius * cobblestoneMultiplier;
                    clip = cobblestoneSFX[(int)Speed.run];
                    break;
                case FloorType.Crate:
                    pMovement.currentSoundRadius = pMovement.sprintSoundRadius * crateMultiplier;
                    clip = crateSFX[(int)Speed.run];
                    break;



            }
            footStepsSource.enabled = true;
            footStepsSource.clip = clip;
            footStepsSource.volume = (float)1.5;
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


    // Set sound radius based on the terrain. Use a floortype mulitplier and base it on the player speed
    //play sound FX of sneaking
    public void PlaySneakingSound()
    {
        
        if(isMoving)
        {




            clip = null;

            TerrainType = GetTerrain();

            switch (TerrainType)
            {
                case FloorType.Carpet:
                    pMovement.currentSoundRadius = pMovement.sneakSoundRadius * carpetMultiplier;
                    clip = carpetSFX[(int)Speed.sneak];
                    break;
                case FloorType.Hardwood:
                    pMovement.currentSoundRadius = pMovement.sneakSoundRadius * hardwoodMultiplier;
                    clip = hardwoodSFX[(int)Speed.sneak];
                    
                    break;
                case FloorType.CobbleStone:
                    pMovement.currentSoundRadius = pMovement.sneakSoundRadius * cobblestoneMultiplier;
                    clip = cobblestoneSFX[(int)Speed.sneak];
                   
                    break;
                case FloorType.Crate:
                    pMovement.currentSoundRadius = pMovement.sneakSoundRadius * crateMultiplier;
                    clip = crateSFX[(int)Speed.sneak];
                    
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


        pMovement.currentSoundRadius = 1;
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
             break;
         case FloorType.Hardwood:
             hardWoodWalk.enabled = true;
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
            }
            else if (other.tag == "HardWood")
            {
                TerrainType = FloorType.Hardwood;
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
