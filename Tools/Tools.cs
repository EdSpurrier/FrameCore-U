using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;







[System.Serializable]
public class Tools
{
    [HideLabel]
    [SuffixLabel("Target Frame Rate", Overlay = true)]
    public int targetFrameRate = 60;

    [HideLabel]
    [HorizontalGroup("Split", 0.50f)]
    [SuffixLabel("Frame Update Rate", Overlay = true)]
    public int frameUpdateFrameRate = 10;
    int currentFrameUpdate = 0;
    [HorizontalGroup("Split", 0.50f)]
    public bool frameUpdate = false;

    [FoldoutGroup("Debounce Manager")]
    public Debouncer debouncer;

    [HideLabel]
    public DeBugger debug;



    public void Init()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    public void Update()
    {
        
        debouncer.Update();

        currentFrameUpdate--;

        if (currentFrameUpdate <= 0)
        {
            currentFrameUpdate = frameUpdateFrameRate;
            frameUpdate = true;
        }
        else {
            frameUpdate = false;
        };
    }

    public void FixedUpdate()
    {

    }

    public void LateUpdate()
    {

    }



}
