using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Treasure Stat", menuName = "StatBlock/TreasureStats")]
public class TreasureStats : ScriptableObject
{
    public enum TreasureType {COIN, CHEST, FINALCHEST};
    [SerializeField] public TreasureType treasureType;
    [SerializeField] public int coinValue;
    [SerializeField] public bool finalChest;
}
