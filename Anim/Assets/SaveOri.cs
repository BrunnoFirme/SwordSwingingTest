using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOri : MonoBehaviour
{
    public ProcAnimation anim;
    public Transform trans;

    void SaveData()
    {
        for (int i = 0; i < 3; i++)
        {
            switch(i)
            {
                case 0:
                    anim.animSegs[0].animType = ProcAnimation.animationType.position;
                    anim.animSegs[0].start = trans.position;
                    anim.animSegs[0].end = trans.position;
                    break;
                case 1:
                    anim.animSegs[1].animType = ProcAnimation.animationType.rotation;
                    anim.animSegs[1].start = trans.rotation.eulerAngles;
                    anim.animSegs[1].end = trans.position;
                    break;
                case 2:
                    anim.animSegs[2].animType = ProcAnimation.animationType.scale;
                    anim.animSegs[2].start = trans.localScale;
                    anim.animSegs[2].end = trans.localScale;
                    break;
                default:
                    Debug.Log("Invalid number of animation segments. Animation is " + anim.name);
                    break;
            }
        }
    }
}
