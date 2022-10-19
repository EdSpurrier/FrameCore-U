using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_ActivateEventManager
{
    [InlineButton("TriggerEventManager")]
    public EventManager eventManager;

    [HideLabel]
    public DeBugger debug;


    private void TriggerEventManager()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {
        eventManager.Activate();

        debug.Log("Event Manager Activated");
    }
    

   
}
