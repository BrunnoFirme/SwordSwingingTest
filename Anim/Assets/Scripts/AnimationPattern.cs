using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimationPattern", menuName = "ScriptableObjects/AnimationPattern", order = 2)]
public class AnimationPattern : ScriptableObject
{
    [Range(-200,200)]
    public int firstX = 25;
    [Range(-200, 200)]
    public int firstY = 25;
    [Range(-200, 200)]
    public int firstZ = 25;
    [Range(-200, 200)]
    public int secondX = 75;
    [Range(-200, 200)]
    public int secondY = 75;
    [Range(-200, 200)]
    public int secondZ = 75;
}
