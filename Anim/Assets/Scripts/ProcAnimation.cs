using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProcAnimObject", menuName = "ScriptableObjects/ProcAnimObject", order = 1)]
public class ProcAnimation: ScriptableObject
{
    public int numPoints = 30;
    public int frameRate = 120;
    public enum animationType {position, rotation, scale}
    public bool startFromPos;
    public AnimationPattern defaultPattern;
    public AnimSegment[] animSegs = new AnimSegment[3];
    public bool hasEvent = false;
    public string EventTrigger = "None";
    /*
     * Priority for anims
     * Break anim/Parry anim/Got Parryed anim/Clash particle
     * 
     * mvp
     * Sword clash
     * thrust/swing/block/overpower
     * recoil after bad hit
     * swing after first swing
     * 
     */

    public void Init()
    {
        for (int i = 0; i < animSegs.Length; i++)
        {
            animSegs[i].dataPoints = new Vector3[4];
        }
        SetAnimSegment(animSegs[0]);
        SetAnimSegment(animSegs[1]);
        SetAnimSegment(animSegs[2]);
    }
    private void SetAnimSegment(AnimSegment animSeg)
    {
        if (animSeg.pattern == null)
        {
            animSeg.pattern = defaultPattern;
        }
        animSeg.dataPoints[0] = animSeg.start;
        animSeg.dataPoints[1] = getOffsetPoint(new Vector3(animSeg.pattern.firstX, animSeg.pattern.firstY, animSeg.pattern.firstZ), animSeg.start, animSeg.end);
        animSeg.dataPoints[2] = getOffsetPoint(new Vector3(animSeg.pattern.secondX, animSeg.pattern.secondY, animSeg.pattern.secondZ), animSeg.start, animSeg.end);
        animSeg.dataPoints[3] = animSeg.end;
        animSeg.procAnimation = this;
    }

    public void SetAnimSegment(AnimSegment animSeg, Vector3 firstPoint)
    {
        if (animSeg.pattern == null)
        {
            animSeg.pattern = defaultPattern;
        }
        animSeg.dataPoints[0] = animSeg.start;
        animSeg.dataPoints[1] = getOffsetPoint(new Vector3(animSeg.pattern.firstX, animSeg.pattern.firstY, animSeg.pattern.firstZ), firstPoint, animSeg.end);
        animSeg.dataPoints[2] = getOffsetPoint(new Vector3(animSeg.pattern.secondX, animSeg.pattern.secondY, animSeg.pattern.secondZ), firstPoint, animSeg.end);
        animSeg.dataPoints[3] = animSeg.end;
        animSeg.procAnimation = this;
    }

    private Vector3 getOffsetPoint(Vector3 point, Vector3 firstPos, Vector3 lastPos)
    {
        Vector3 newPoint = new Vector3();
        newPoint.x = ((lastPos.x - firstPos.x) * (float)(point.x / 100)) + firstPos.x;
        newPoint.y = ((lastPos.y - firstPos.y) * (float)(point.y / 100)) + firstPos.y;
        newPoint.z = ((lastPos.z - firstPos.z) * (float)(point.z / 100)) + firstPos.z;

        return newPoint;
    }
}


[System.Serializable]
public class AnimSegment
{
    public ProcAnimation.animationType animType;
    public Vector3 start;
    public Vector3 end;
    [HideInInspector]
    public Vector3 second;
    [HideInInspector]
    public Vector3 third;
    public AnimationPattern pattern;

    public int curveNum;
    public bool enabled = true;
    [HideInInspector]
    public Vector3[] dataPoints = new Vector3[4];
    [HideInInspector]
    public ProcAnimation procAnimation;
    [HideInInspector]
    public Vector3[] results;
}
