using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OnOffToggleEvent
{
    //  HOVER
    [ToggleGroup("toggleEvents", "$eventName")]
    public bool toggleEvents = false;




    [HideInInspector]
    public string eventName = "Toggle Event Name";

    /*[HideInInspector]*/
    [ToggleGroup("toggleEvents")]
    public bool toggleState = false;


    [FoldoutGroup("toggleEvents/On Event")]
    [HideLabel]
    public FrameCoreEvent onEvent;

    [FoldoutGroup("toggleEvents/Off Event")]
    [HideLabel]
    public FrameCoreEvent offEvent;

    public void ToggleEvent()
    {
        ToggleEvent(!toggleState);
    }

    public void ToggleEvent(bool state)
    {
        if (!toggleEvents)
        {
            return;
        };

        if (state)
        {
            if (!toggleState)
            {
                onEvent.Activate();
                toggleState = true;
            };
        }
        else
        {
            if (toggleState)
            {
                offEvent.Activate();
                toggleState = false;
            };
        };
    }
}
