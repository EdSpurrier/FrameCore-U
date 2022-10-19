using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class FrameCoreEvent
{

    [HideLabel]
    [SuffixLabel("Event Name", Overlay = true)]
    public string eventName = "Event Name";

    
    


    
    private void ActivateTriggerEvent()
    {
        if (EditorInteractions.InPlayerButton())
        {
            TriggerEvent();
        };
    }





    

    bool hideActionDetails = false;
    [HideIfGroup("hideActionDetails")]


    [HorizontalGroup("Split", 0.5f)]
    [Button(ButtonSizes.Small), GUIColor(0f, 0.8f, 1)]
    public void HideActionDetails()
    {
        Debug.Log("Hiding Action Details...");
        hideActionDetails = !hideActionDetails;

        actions.ForEach(action => {
            action.hideDetails = hideActionDetails;
        });
    }


    [HideLabel]
    [HorizontalGroup("Split", 0.5f)]
    [SuffixLabel("Queue (s)", Overlay = true)]
    public float waitTime = 0f;





    



    [InlineButton("ActivateTriggerEvent")]

    [HideLabel]
    public Debounce debounce;

    [OdinSerialize]
    public List<ActionManager> actions;


    [FoldoutGroup("Event")]
    [Title("Events")] 
    public UnityEvent triggerEvent;

    

    
    [FoldoutGroup("hideActionDetails/System")]
    public bool active = false;
    [FoldoutGroup("hideActionDetails/System")]
    public EventManager eventManager;
    [FoldoutGroup("hideActionDetails/System")]
    public float processTime = 0f;


    [FoldoutGroup("hideActionDetails/Setup")]
    [Button("Auto Name", ButtonSizes.Medium), GUIColor(0.8f, 1f, 0.8f)]
    public void AutoName()
    {
        if (!EditorInteractions.InEditorButton())
        {
            return;
        };

        foreach (ActionManager action in actions)
        {
            action.actionName = action.actionType.ToString();
        };
    }



    
    [HideLabel]
    [HideIf("hideActionDetails")]
    public DeBugger debug;




    





    public void ResetActions()
    {
        foreach (ActionManager action in actions)
        {
            action.Reset();
        };
    }

    public void TriggerEvent()
    {
        DebugEvent();

        triggerEvent.Invoke();

        foreach(ActionManager action in actions)
        {
            action.Activate();
        };

        if (eventManager)
        {
            eventManager.ActivateLoop();
        };
        
    }

    public void Activate(float queueTime = 0f)
    {
        //  IF TRUE => DEBOUNCE IS CURRENTLY ACTIVE
        if( debounce.CheckDebounce() )
        {
            return;
        };

        ResetActions();

        processTime = waitTime + queueTime;

        if (processTime > 0f)
        {
            Frame.core.events.ActivateTimedEvent(this);
        }
        else {
            TriggerEvent();
        };
    }


    // Update is called once per frame
    public bool EventUpdate()
    {
        if (!active)
        {
            return false;
        };

        if (processTime <= 0f)
        {
            TriggerEvent();
            return true;
        };

        processTime -= Time.deltaTime;

        return false;
    }

    void DebugEvent()
    {
        debug.active = (debug.active || Frame.core.events.debugAllEvents);
        debug.Log("FrameCoreEvent [" + Frame.core.events.eventCoreTime + "] >> " + eventName + " - Triggered");
    }



}



public class EventCore : MonoBehaviour
{

    [Title("Events")]
    public List<FrameCoreEvent> timedEvents;
    List<FrameCoreEvent> timedEvents_Completed = new List<FrameCoreEvent>();

    [Title("System")]
    public float eventCoreTime = 0f;
    [HideLabel]
    public DeBugger debug;
    public bool debugAllEvents;

    

    public void ActivateTimedEvent(FrameCoreEvent frameCoreEvent)
    {
        debug.Log("Activated Timed Event >> " + frameCoreEvent.eventName);
        timedEvents.Add(frameCoreEvent);
        frameCoreEvent.active = true;
    }



    // Update is called once per frame
    void Update()
    {
        debug.active = (debug.active || Frame.core.events.debugAllEvents);

        eventCoreTime += Time.deltaTime;

        UpdateTimedEvents();

        ClearCompletedEvents();
    }


    public void UpdateTimedEvents()
    {
        foreach (FrameCoreEvent timedEvent in timedEvents)
        {
            if (timedEvent == null)
            {
                debug.Log("Removing NULL Timed Event");
                timedEvents_Completed.Add(timedEvent);
                continue;
            };

            if (timedEvent.EventUpdate())
            {
                debug.Log("Timed Event Complete >> " + timedEvent.eventName);
                timedEvents_Completed.Add(timedEvent);
            };
        };
    }



    public void ClearCompletedEvents()
    {
        foreach (FrameCoreEvent timedEvent in timedEvents_Completed)
        {
            timedEvents.Remove(timedEvent);
        };

        timedEvents_Completed.Clear();
    }

}
