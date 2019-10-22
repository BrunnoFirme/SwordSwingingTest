using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProcAnimObjectChain", menuName = "ScriptableObjects/ProcAnimObjectChain", order = 2)]
public class AnimChain : ScriptableObject
{
    public ProcAnimation[] procAnimChain;
    int currentAnim = -1;

    public ProcAnimation getNextAnim()
    {
        currentAnim++;
        if (currentAnim > procAnimChain.Length)
        {
            currentAnim = 0;
        }
        return procAnimChain[currentAnim];
    }
}
