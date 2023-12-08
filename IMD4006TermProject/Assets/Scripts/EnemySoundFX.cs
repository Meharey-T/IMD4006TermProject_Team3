using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundFX : MonoBehaviour
{

    [SerializeField] private Enemy enemy;
  
    public enum AngerLevel
    {
        INDIFFERENT, 
        IRRITATED, 
        ANGRY, 
        FURIOUS
    }

    public enum EnemyState
    {
       Idle,
       See,
       Hear,
       HadSeen,
       HadHeard
    }
    public AngerLevel currEmotion = AngerLevel.INDIFFERENT;
    public EnemyState currState = EnemyState.Idle;


    [Header("FootSteps")]
    public List<AudioClip> idleSFX;
    public List<AudioClip> seeSFX;
    public List<AudioClip> hearSFX;
    public List<AudioClip> hadSeenSFX;
    public List<AudioClip> hadHeardSFX;

    private AudioSource enemySoundSource;

    AudioClip clip = null;

    private bool hearSoundPlayed=false;
    private bool sawSoundPlayed = false;
    public bool hasAcknowledged;
    void Start()
    {

        enemySoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Getplayers current emotion
  private AngerLevel GetEmotion()
    {
        return currEmotion;
    }
    public void PlayIdleSound()
    {


        if (currState==EnemyState.Idle)
        {
            clip = null;
            currEmotion = GetEmotion();

            switch (currEmotion)
            {

                case AngerLevel.INDIFFERENT:
                    clip = idleSFX[(int)1];
                    break;
            }
            enemySoundSource.enabled = true;
            enemySoundSource.clip = clip;
            enemySoundSource.volume = 1;
            if (!enemySoundSource.isPlaying)
            {
                enemySoundSource.Play();
            }
        }
        else
        {
            PlayIdleSound();
        }
    }

    public void hasHearSoundSaid()
    {
        if (!hearSoundPlayed) {
            PlayCheckOutSoundSound();
        }

    }

  
    public void hasSawSoundSaid()
    {
        if (!sawSoundPlayed)
        {
            PlayCheckOutSawSound();
        }

    }
   
    //If you heard the player, say something
    public void PlayCheckOutSoundSound()
    {
     

        if (currState == EnemyState.HadHeard)
        {
            clip = null;
            currEmotion = GetEmotion();
            switch (currEmotion)
            {
                case AngerLevel.INDIFFERENT:
                    clip = hadHeardSFX[(int)1];
                    break;
                case AngerLevel.IRRITATED:
                    clip = hadHeardSFX[(int)1];
                    break;
                case AngerLevel.ANGRY:
                    clip = hadHeardSFX[(int)2];
                    break;
                case AngerLevel.FURIOUS:
                    clip = hadHeardSFX[(int)2];
                    break;

            }
            if (!enemySoundSource.isPlaying)
            {
                enemySoundSource.enabled = true;
                enemySoundSource.clip = clip;
                enemySoundSource.volume = 1;

                enemySoundSource.Play();
            }
        }
        else
        {
            //PlayIdleSound();
        }
    }



    public void PlayCheckOutSawSound()
    {


        if (currState == EnemyState.HadSeen)
        {

            clip = null;
            currEmotion = GetEmotion();
            switch (currEmotion)
            {



                case AngerLevel.INDIFFERENT:
                    clip = hadSeenSFX[(int)1];
                    break;
                case AngerLevel.IRRITATED:
                    clip = hadSeenSFX[(int)1];
                    break;
                case AngerLevel.ANGRY:
                    clip = hadSeenSFX[(int)2];
                    break;
                case AngerLevel.FURIOUS:
                    clip = hadSeenSFX[(int)2];
                    break;



            }
            if (!enemySoundSource.isPlaying)
            {
                enemySoundSource.enabled = true;
                enemySoundSource.clip = clip;
                enemySoundSource.volume = 1;

                enemySoundSource.Play();
            }
        }
    }

    public void PlaySeeSound()
    {


        if (currState == EnemyState.HadHeard)
        {

            clip = null;
            currEmotion = GetEmotion();
            switch (currEmotion)
            {
                case AngerLevel.INDIFFERENT:
                    clip = seeSFX[(int)1];
                    break;
                case AngerLevel.IRRITATED:
                    clip = seeSFX[(int)1];
                    break;
                case AngerLevel.ANGRY:
                    clip = seeSFX[(int)2];
                    break;
                case AngerLevel.FURIOUS:
                    clip = seeSFX[(int)2];
                    break;
            }
            if (!enemySoundSource.isPlaying)
            {
                enemySoundSource.enabled = true;
                enemySoundSource.clip = clip;
                enemySoundSource.volume = 1;

                enemySoundSource.Play();

            }
        }

    }

    //If you heard the player, say something
    public void PlayHearSound()
    {
        if (currState == EnemyState.Hear)
        {
            clip = null;
            currEmotion = GetEmotion();
            switch (currEmotion)
            {
                case AngerLevel.INDIFFERENT:
                    clip = hearSFX[(int)1];
                    break;
                case AngerLevel.IRRITATED:
                    clip = hearSFX[(int)1];
                    break;
                case AngerLevel.ANGRY:
                    clip = hearSFX[(int)2];
                    break;
                case AngerLevel.FURIOUS:
                    clip = hearSFX[(int)2];
                    break;

            }
            if (!enemySoundSource.isPlaying)
            {
                enemySoundSource.enabled = true;
                enemySoundSource.clip = clip;
                enemySoundSource.volume = 1;

                enemySoundSource.Play();

            }
        }
        else
        {
            //PlayIdleSound();
        }
    }

    //Get the differen emotions and later set them to currEmotion in the TaskClearDetection
    public AngerLevel GetIndifference()
    {

        return AngerLevel.INDIFFERENT;
    }
    public AngerLevel GetIrritated()
    {

        return AngerLevel.IRRITATED;
    }
    public AngerLevel GetAngry()
    {

        return AngerLevel.ANGRY;
    }
    public AngerLevel GetFurious()
    {

        return AngerLevel.FURIOUS;
    }


    //Get current State and later set them to currState in the TaskClearDetection
    public EnemyState GetHearState()
    {

        return EnemyState.Hear;
    }
    public EnemyState GetSeeState()
    {

        return EnemyState.See;
    }
    public EnemyState GetHeardState()
    {

        return EnemyState.HadHeard;
    }
    public EnemyState GetSawState()
    {

        return EnemyState.HadSeen;
    }

}


/*


// Update is called once per frame
void Update()
{
    if (enemy.sawPlayer)
    {
        hasSawSoundSaid();

    }
}

private AngerLevel GetEmotion()
{
    return currEmotion;
}
public void PlayIdleSound()
{


    if (currState == EnemyState.Idle)
    {
        clip = null;
        currEmotion = GetEmotion();

        switch (currEmotion)
        {

            case AngerLevel.INDIFFERENT:
                clip = idleSFX[(int)1];
                break;



        }
        enemySoundSource.enabled = true;
        enemySoundSource.clip = clip;
        enemySoundSource.volume = 1;
        if (!enemySoundSource.isPlaying)
        {
            enemySoundSource.Play();
        }
    }
    else
    {
        PlayIdleSound();
    }
}

public void hasHearSoundSaid()
{
    if (!hearSoundPlayed)
    {
        PlayCheckOutSoundSound();
    }

}


public void hasSawSoundSaid()
{
    if (!sawSoundPlayed)
    {
        PlayCheckOutSawSound();
    }

}
public void PlayCheckOutSoundSound()
{


    /*     
     if ( hasAcknowledged == true)
     {
         enemySoundSource.enabled = false;
     }*/
  /*  if (currState == EnemyState.Hear)
    {
        clip = null;
        currEmotion = GetEmotion();
        switch (currEmotion)
        {



            case AngerLevel.INDIFFERENT:
                clip = hearSFX[(int)1];
                break;
            case AngerLevel.IRRITATED:
                clip = hearSFX[(int)1];
                break;
            case AngerLevel.ANGRY:
                clip = hearSFX[(int)2];
                break;
            case AngerLevel.FURIOUS:
                clip = hearSFX[(int)2];
                break;



        }
        if (!enemySoundSource.isPlaying)
        {
            enemySoundSource.enabled = true;
            enemySoundSource.clip = clip;
            enemySoundSource.volume = 1;

            enemySoundSource.Play();

        }



        hearSoundPlayed = true;
        hasAcknowledged = true;



    }
    else
    {
        //PlayIdleSound();
    }
}



public void PlayCheckOutSawSound()
{

    /* if (hasAcknowledged == true)
     {
         enemySoundSource.enabled = false;
     }*/
    //if (currState == EnemyState.HadSeen)
    // {
    //
    /*
    clip = null;
    currEmotion = GetEmotion();
    switch (currEmotion)
    {



        case AngerLevel.INDIFFERENT:
            clip = seeSFX[(int)1];
            break;
        case AngerLevel.IRRITATED:
            clip = seeSFX[(int)1];
            break;
        case AngerLevel.ANGRY:
            clip = seeSFX[(int)2];
            break;
        case AngerLevel.FURIOUS:
            clip = seeSFX[(int)2];
            break;



    }
    if (!enemySoundSource.isPlaying)
    {
        enemySoundSource.enabled = true;
        enemySoundSource.clip = clip;
        enemySoundSource.volume = 1;

        enemySoundSource.Play();

    }

    sawSoundPlayed = true;
    // hasAcknowledged = true;



    //}

}
*/


/*


public AngerLevel GetIndifference()
{

    return AngerLevel.INDIFFERENT;
}
public EnemyState GetHearState()
{

    return EnemyState.Hear;
}


}*/