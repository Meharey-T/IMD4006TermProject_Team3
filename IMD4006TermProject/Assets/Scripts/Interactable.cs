using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We can put this on any type of thing that can be interacted with, collided with, or removed I think?
public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
    }
}
