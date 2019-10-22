using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwinging : MonoBehaviour
{
    public ProcAnimation idle;
    public ProcAnimation[] swingToLeft;
    public ProcAnimation[] swingToRight;
    public AnimChain[] animChain;
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

    public ProcAnimation GetAnim()
    {
        if (isInChain)
        {
            if (chainPos >= animChain[currentChain].procAnimChain.Length)
            {
                chainPos = 0;
                isInChain = false;
            }
            else
            {
                chainPos++;
                return animChain[currentChain].procAnimChain[chainPos-1];
            }
        }
        if (attackQ)
        {
            isIdle = false;
            attackQ = false;
            if (isSwingingRight)
            {
                //int i = Random.Range(0, swingToRight.Length);
                isInChain = true;
                chainPos++;
                currentChain = 1;
                return animChain[1].procAnimChain[0];
            }
            else
            {
                //int i = Random.Range(0, swingToLeft.Length);
                isInChain = true;
                chainPos++;
                currentChain = 0;
                return animChain[0].procAnimChain[0];
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