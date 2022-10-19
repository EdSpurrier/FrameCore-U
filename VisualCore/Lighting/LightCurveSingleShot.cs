using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCurveSingleShot : MonoBehaviour
{
    [Title("Settings")]
    public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float GraphTimeMultiplier = 1, GraphIntensityMultiplier = 1;
    public bool startOnStart = false;

    [Title("Parts")]
    public Light lightSource;

    [FoldoutGroup("System")]
    public bool active = false;
    public bool completed = false;
    public float startTime;
    public float eval = 0f;

    private void Start()
    {
        if (startOnStart)
        {
            StartLightCurve();
        };
    }

    public void StartLightCurve()
    {
        startTime = Time.time;
        completed = false;
        lightSource.enabled = true;
        active = true;
        eval = LightCurve.Evaluate(0); 
        SetLightIntensity();
    }


    public void SetLightIntensity()
    {
        lightSource.intensity = eval;
    }

    private void Update()
    {
        if (!active)
        {
            return;
        };

        var time = Time.time - startTime;


        eval = LightCurve.Evaluate(time / GraphTimeMultiplier) * GraphIntensityMultiplier;
        SetLightIntensity();

        if (time >= GraphTimeMultiplier)
        {
            completed = true;
            
            if (eval <= 0f)
            {
                lightSource.enabled = false;
            };
            
        }


        if (completed)
        {
            active = false;
            this.enabled = false;
        };
    }
}
