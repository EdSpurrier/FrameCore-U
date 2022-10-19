using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Statistics
{
    [HideLabel]
    [HorizontalGroup("Split", 0.50f)]
    [SuffixLabel("Frame Rate", Overlay = true)]
    public float frameRate = 0f;

    [HideLabel]
    [HorizontalGroup("Split", 0.50f)]
    [SuffixLabel("Scene Time", Overlay = true)]
    public float sceneTime = 0f;



    [TitleGroup("Delta Time")]

    [HideLabel]
    [HorizontalGroup("Delta Time/Delta Split", 0.50f)]
    [SuffixLabel("Update", Overlay = true)]
    public float deltaTime = 0f;

    [HideLabel]
    [HorizontalGroup("Delta Time/Delta Split", 0.50f)]
    [SuffixLabel("FixedUpdate", Overlay = true)]
    public float fixedDeltaTime = 0f;



    public void Update()
    {
        CalculateFrameRate();

        sceneTime += Time.deltaTime;

        deltaTime = Time.deltaTime;
    }

    public void FixedUpdate()
    {
        fixedDeltaTime = Time.deltaTime;
    }

    public void LateUpdate()
    {

    }

    void CalculateFrameRate()
    {
        frameRate = 1.0f / Time.deltaTime;
    }
}
