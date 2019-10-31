using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwinging : MonoBehaviour
{
    public ProcAnimation idle;
    public ProcAnimation gotParried;
    public AnimChain[] animChainL;
    public AnimChain[] animChainR;
    public AnimateWithCurve aWC;
    public SwordSwingScript sSS;
    bool isSwingingRight = false;
    bool isInChain = false;
    int chainPos = 0;
    bool isIdle = true;


    private void Start()
    {
        if (aWC == null)
        {
            Debug.LogError("MISSING ANIMATE WITH CURVE");
        }
        if (sSS == null)
        {
            Debug.LogError("MISSING SWORD SWING SCRIPT");
        }
        if (idle == null)
        {
            Debug.LogError("IDLE ANIM IS MISSING");
        }
    }

    public void FixedUpdate()
    {
        if (aWC.objectBeingAnimated.transform.localPosition.z > 1)
        {
            isSwingingRight = false;
        }
        else
        {
            isSwingingRight = true;
        }
    }

    AnimChain chain;

    ProcAnimation GetAnimRaw()
    {
        if (isInChain)
        {
            if (chainPos >= chain.procAnimChain.Length)
            {
                chainPos = 0;
                isInChain = false;
            }
            else
            {
                chainPos++;
                return chain.procAnimChain[chainPos-1];
            }
        }
        if (attackQ)
        {
            isIdle = false;
            attackQ = false;
            if (isSwingingRight)
            {
                int i = Random.Range(0, animChainR.Length);
                isInChain = true;
                chainPos++;
                chain = animChainR[i];
                return animChainR[i].procAnimChain[0];
            }
            else
            {
                int i = Random.Range(0, animChainL.Length);
                isInChain = true;
                chainPos++;
                chain = animChainL[i];
                return animChainL[i].procAnimChain[0];
            }
        }
        else
        {
            isIdle = true;
            isSwingingRight = false;
            return idle;
        }
    }

    public ProcAnimation GetAnim()
    {
        ProcAnimation anim = GetAnimRaw();
        if (anim.hasEvent)
        {
            chainPos = 0;
            isInChain = false;
            return gotParried;
            //sSS.SwingSword();
        }
        return anim;
    }

    bool attackQ = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(AttackQWait());
            if (isIdle)
            {
                aWC.Interupt(GetAnim());
            }
        }
    }


    private IEnumerator AttackQWait()
    {
        attackQ = true;
        yield return new WaitForSeconds(1f);
        attackQ = false;
    }

}