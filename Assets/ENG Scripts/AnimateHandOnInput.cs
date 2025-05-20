using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchInput;
    public InputActionProperty gripInput;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchInput.action.ReadValue<float>();
        float gripValue = gripInput.action.ReadValue<float>();

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}