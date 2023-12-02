using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private ObjectFader fader;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider == null)
                {
                    return;
                }
                if(hit.collider.gameObject == player)
                {
                    //nothing in front of us
                    if(fader != null)
                    {
                        fader.doFade = false;
                    }
                }
                else
                {
                    fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if(fader != null)
                    {
                        fader.doFade = true;
                    }
                }
            }
        }
    }
}
