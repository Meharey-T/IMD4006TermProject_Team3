using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code from https://www.youtube.com/watch?v=mOqHVMS7-Nw
public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material[] mats;
    public bool doFade = false;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<MeshRenderer>().materials;
        foreach(Material mat in mats)
        {
            originalOpacity = mat.color.a;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    void FadeNow()
    {
        foreach (Material mat in mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
        
    }

    void ResetFade()
    {
        foreach (Material mat in mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }
}
