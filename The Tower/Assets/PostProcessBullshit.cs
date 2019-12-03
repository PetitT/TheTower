using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessBullshit : MonoBehaviour
{
    private ColorGrading colorGrading;
    private PostProcessVolume ppv;
    public float colorChangeSpeed;
    public float currentColorValue = 0f;
    private bool goingUp = true;

    private void Start()
    {
        ppv = GetComponent<PostProcessVolume>();
        ppv.profile.TryGetSettings(out colorGrading);
    }

    private void Update()
    {
        ChangeCurrentColorValue();
        UpdateCurrentColor();
    }

    private void UpdateCurrentColor()
    {
        Color currentColor = Color.HSVToRGB(currentColorValue, 100, 100);
        colorGrading.colorFilter.value = currentColor;
    }

    private void ChangeCurrentColorValue()
    {
        if (goingUp)
            currentColorValue += Time.deltaTime;
        else
            currentColorValue -= Time.deltaTime;

        if (currentColorValue >= 360)
            goingUp = false;
        if (currentColorValue <= 0)
            goingUp = true;
    }
}
