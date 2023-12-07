using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Library/Animation Library")]
public class AnimationLibrary : ScriptableObject
{
    [SerializeField]public List<AnimationClip> animationList;
}
