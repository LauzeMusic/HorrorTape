using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteAnimatorController : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFocused(bool focused)
    {
        animator.SetBool("IsFocused", focused);
    }
}