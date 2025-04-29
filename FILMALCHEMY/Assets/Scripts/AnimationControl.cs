using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationControl : MonoBehaviour
{

    [Header("µ¹Ë®")]
    public GameObject ContainerWater;
    public int ContainerPouringStep;

    [Header("Ò¡»Î")]
    public Animator myDevelopingAnimator;


    private void OnEnable()
    {
        ClickAreaSpawner.StartWaterPouring += StartPouring;
        ClickAreaSpawner.EndWaterPouring += EndPouring;
        //PhotoDevelopController.StartDeveloping += StartDevelopPhoto;
        //PhotoDevelopController.StopDeveloping += EndDevelopPhoto;

    }

    private void OnDisable()
    {
        ClickAreaSpawner.StartWaterPouring      -= StartPouring;
        ClickAreaSpawner.EndWaterPouring        -= EndPouring;
        //PhotoDevelopController.StartDeveloping  -= StartDevelopPhoto;
        //PhotoDevelopController.StopDeveloping   -= EndDevelopPhoto;
    }

    public void StartDevelopPhoto()
    {
        if (myDevelopingAnimator != null)
        {
            myDevelopingAnimator.Play("StartDeveloping");
        }
    }

    public void EndDevelopPhoto()
    {
        if (myDevelopingAnimator != null)
        {
            myDevelopingAnimator.Play("Idle");
        }
    }

    public void StartPouring(int nextStep)
    {
        if (nextStep == ContainerPouringStep + 1)
        {
            Debug.Log("Start Pouring Water");
            ContainerWater.SetActive(true);
        }
    }

    public void EndPouring(int nextStep)
    {
        if (nextStep == ContainerPouringStep + 1) 
        {
            Debug.Log("Stop Pouring Water");
            ContainerWater.SetActive(false);
        }

    }

}
