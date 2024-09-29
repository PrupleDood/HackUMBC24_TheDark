using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class doorAnimator : MonoBehaviour
{

    private Animator mAnimator;

    public bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update() {

    }

    public void TriggerOpen() {
        mAnimator.SetBool("TrClose", false);
        mAnimator.SetBool("TrOpen", true);
    }

    public void TriggerClose() {
        mAnimator.SetBool("TrOpen", false);
        mAnimator.SetBool("TrClose", true);
    }

}
