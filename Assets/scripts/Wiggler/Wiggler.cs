using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{
    protected Animator animator;
    // Use this for initialization
    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void CheckWiggle(bool wiggle)
    {
        animator.SetBool("bWiggling", wiggle);
    }
}
