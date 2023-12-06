using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundFX : MonoBehaviour
{

    [SerializeField] private GameObject Enemy;
  
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
   

    void Start()
    {

        enemySoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                    Debug.Log("IdleSFX");
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
            //PlayIdleSound();
        }
    }

}
