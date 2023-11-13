using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    //We might not need to put anything here?
    //If we put polish on to make Twig's tail or nose visible on certain hiding objects, we may need to put a marker
    //in this script to tell it to make that visible while we're hiding

    Color[] m_mainColour;
    Color[] m_brightenedColour;
    MeshRenderer m_renderer;
    [SerializeField] GameObjectRuntimeSet player;

    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_mainColour = new Color[m_renderer.materials.Length];
        m_brightenedColour = new Color[m_renderer.materials.Length];
        for (int i = 0; i < m_renderer.materials.Length; i++)
        {
            m_mainColour[i] = m_renderer.materials[i].color;
            m_brightenedColour[i] = MultiplyColour(m_mainColour[i]);
        }
        
    }

    private Color MultiplyColour(Color colourToBeBrightened)
    {
        float rgbModifier = 0.25f;
        colourToBeBrightened.r += rgbModifier;
        colourToBeBrightened.g += rgbModifier;
        colourToBeBrightened.b += rgbModifier;
        return colourToBeBrightened;
    }

    private void OnMouseOver()
    {
        //There will only ever be one player so we can be sure they'll be at index 0
        float distance = Vector3.Distance(this.transform.position, player.Items[0].transform.position);
        if (distance < 3)
        {
            player.Items[0].GetComponent<Player>().inRangeOfHideable = true;
            if(player.Items[0].GetComponent<Player>().hiding == false)
            {
                player.Items[0].GetComponent<Player>().selectedHideable = this.transform.position;
            }
            for (int i = 0; i < m_renderer.materials.Length; i++)
            {
                m_renderer.materials[i].color = m_brightenedColour[i];
            }
        }
        /*
        else
        {
            player.Items[0].GetComponent<Player>().inRangeOfHideable = false;
        }
        */
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < m_renderer.materials.Length; i++)
        {
            m_renderer.materials[i].color = m_mainColour[i];
        }
        player.Items[0].GetComponent<Player>().inRangeOfHideable = false;
    }
}
