using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOri : MonoBehaviour
{
    public ProcAnimation anim;
    public Transform trans;

    public void SaveData()
    {
        if (anim == null || trans == null)
        {
            Debug.LogError("Either the anim or transform is null");
        }

        for (int i = 0; i < 3; i++)
        {
            switch(i)
            {
                case 0:
                    anim.animSegs[0].animType = ProcAnimation.animationType.position;
                    anim.animSegs[0].start = trans.localPosition;
                    anim.animSegs[0].end = trans.localPosition;
                    anim.animSegs[0].curveNum = 2;
                    break;
                case 1:
                    anim.animSegs[1].animType = ProcAnimation.animationType.rotation;
                    anim.animSegs[1].start = trans.rotation.eulerAngles;
                    anim.animSegs[1].end = trans.eulerAngles;
                    anim.animSegs[0].curveNum = 2;
                    break;
                case 2:
                    anim.animSegs[2].animType = ProcAnimation.animationType.scale;
                    anim.animSegs[2].start = trans.localScale;
                    anim.animSegs[2].end = trans.localScale;
                    anim.animSegs[0].curveNum = 2;
                    anim.animSegs[0].enabled = false;
                    break;
                default:
                    Debug.Log("Invalid number of animation segments. Animation is " + anim.name);
                    break;
            }
        }
    }

    public void ChangeToData()
    {
        if (anim == null || trans == null)
        {
            Debug.LogError("Either the anim or transform is null");
        }

        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    trans.localPosition = anim.animSegs[0].end;
                    break;
                case 1:
                    trans.rotation = Quaternion.Euler(anim.animSegs[1].end);
                    break;
                case 2:
                    trans.localScale = anim.animSegs[2].end;
                    break;
                default:
                    Debug.Log("Invalid number of animation segments. Animation is " + anim.name);
                    break;
            }
        }
    }
}
