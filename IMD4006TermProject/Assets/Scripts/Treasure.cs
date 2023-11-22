using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When creating a new treasure prefab make sure you do the following:
 * Put this script on it
 * Put the "Interactable" script on it
 * Create or reuse a stat value for it
 * Drag the stat into the slot on this script
 * Make sure it has a collider that matches its shape (use a mesh collider for complex shapes)
 * Set its tag to "Treasure"
 */
public class Treasure : Interactable
{
    Material outlineShader;
    public TreasureStats treasureStats;
    MeshRenderer m_renderer;

    // public void material.EnableKeyword(_ALPHAPREMULTIPLY_ON);
    private void OnMouseOver()
    {
        //get it 
        m_renderer = GetComponent<MeshRenderer>();
        int lastMat = m_renderer.materials.Length - 1;
        // outlineShader.EnableKeyword("_Scale");
        outlineShader = m_renderer.materials[lastMat];
        // outlineShader.SetInt(_Scale, int );

        outlineShader.SetFloat("_Scale", (float)1.15);

        //print("you are hovering over treasure");
        //print(outlineShader);

    }
    private void OnMouseExit()
    {
        //get it 
        m_renderer = GetComponent<MeshRenderer>();
        int lastMat = m_renderer.materials.Length - 1;
        // outlineShader.EnableKeyword("_Scale");
        outlineShader = m_renderer.materials[lastMat];
        // outlineShader.SetInt(_Scale, int );

        outlineShader.SetFloat("_Scale", (float)1.05);

        //print("you are hovering over treasure");
        //print(outlineShader);

    }
}
