using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwinging : MonoBehaviour
{
    public ProcAnimation idle;
    public AnimChain[] animChainL;
    public AnimChain[] animChainR;
    public AnimateWithCurve aWC;
    bool isSwingingRight = false;
    bool isInChain = false;
    int chainPos = 0;
    bool isIdle = true;
    int currentChain = 0;

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

    public ProcAnimation GetAnim()
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