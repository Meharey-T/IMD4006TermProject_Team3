using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSet : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource volSource;
    void Start()
    {
        volSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       volSource.volume = (float)0.21;
}
    

}
