using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWithCurve : MonoBehaviour
{
    public GameObject objectBeingAnimated;
    public ProcAnimation idleAnim;
    public ProcAnimation[] anims;
    bool hasAnim;
    [SerializeField]
    ProcAnimation defaultAnim;
    ProcAnimation lastAnim;
    public SwordSwinging swing;

    bool interupted = false;
    int interuptedNum = 0;
    ProcAnimation currentAnimation;

    private void Start()
    {
        defaultAnim.Init();
        lastAnim = defaultAnim;
        currentAnimation = defaultAnim;
        SendAnim(idleAnim);
    }

    public void Interupt(ProcAnimation newAnim)
    {
        interupted = true;
        StopAllCoroutines();
        interuptedNum = objectPos;
        EndAnimation();
        SendAnim(newAnim);
    }

    Vector3 GetEnd(AnimSegment seg)
    {
        if (!interupted)
        {
            return seg.end;
        }
        else
        {
            interupted = false;
            if (interuptedNum == 0)
            {
                interuptedNum = 1;
            }
            return seg.results[interuptedNum-1];
        }
    }

    public void SendAnim(ProcAnimation anim)
    {
        if (!hasAnim)
        {
            currentAnimation = anim;
            anim.Init();
            hasAnim = true;
            if (anim.startFromPos == false)
            {
                foreach (AnimSegment segment in anim.animSegs)
                {
                    foreach (AnimSegment oldSegment in lastAnim.animSegs)
                    {
                        if (segment.animType == oldSegment.animType)
                        {
                            segment.procAnimation.SetAnimSegment(segment, GetEnd(oldSegment));
                            continue;
                        }
                    }
                }
            }
            SendAnimSeg(anim.animSegs);
        }
        else
        {
            Debug.LogError("ALREADY HAS ANIMATION");
        }
    }

    void SendAnimSeg(AnimSegment[] segs)
    {
        CalculateCurves(segs);
        StartCoroutine(ProcAnim(segs));
    }

    void CalculateCurves(AnimSegment[] animSegs)
    {
        for (int i = 0; i < animSegs.Length; i++)
        {
            Vector3 startPos = animSegs[i].dataPoints[0];
            if (animSegs[i].procAnimation.startFromPos == false)
            {
                foreach (AnimSegment seg in lastAnim.animSegs)
                {
                    if (animSegs[i].animType == seg.animType)
                    {
                        animSegs[i].dataPoints[0] = seg.end;
                    }
                }
            }
            animSegs[i].results = new Vector3[animSegs[i].procAnimation.numPoints];
            animSegs[i].results = BezierCurve.DrawLine(animSegs[i].curveNum, animSegs[i].dataPoints, animSegs[i].procAnimation.numPoints);
            animSegs[i].dataPoints[0] = startPos;
        }
    }

    public int frameRate = 24;
    int objectPos = 0;

    private IEnumerator ProcAnim(AnimSegment[] animSeg)
    {
        yield return new WaitForSeconds(0.1f / animSeg[0].procAnimation.frameRate);
        for (int i = 0; i < animSeg.Length; i++)
        {
            if (animSeg[i].enabled)
            {
                switch (animSeg[i].animType)
                {
                    default:
                        objectBeingAnimated.transform.position = animSeg[i].results[objectPos]+this.transform.position;
                        break;
                    case ProcAnimation.animationType.rotation:
                        objectBeingAnimated.transform.rotation = Quaternion.Euler(animSeg[i].results[objectPos]);
                        break;
                    case ProcAnimation.animationType.scale:
                        objectBeingAnimated.transform.localScale = animSeg[i].results[objectPos];
                        break;
                }
            }
        }
        objectPos++;
        if (objectPos >= animSeg[0].procAnimation.numPoints)
        {
            EndAnimation();
            SendAnim(swing.GetAnim());
        }
        else
        {
            StartCoroutine(ProcAnim(animSeg));
        }
    }

    void EndAnimation()
    {
        objectPos = 0;
        lastAnim = currentAnimation;
        hasAnim = false;
    }
}