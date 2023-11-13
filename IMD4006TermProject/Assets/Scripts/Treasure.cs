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
    public TreasureStats treasureStats;
}
