using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    [Title("Settings")]

    [InlineButton("ActivateEventChain")]
    public string eventManagerName = "";
    public ActivateWhen activate;
    public bool queueInSuccession = true;
    public bool loop = false;
    public bool singleUse = true;


    [Title("Events")]
    public List<FrameCoreEvent> events;

    [Title("System")]
    public bool activated = false;

    [HideLabel]
    public DeBugger debug;

    

    private void ActivateEventChain()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }

    private void OnEnable()
    {
        if (activate == ActivateWhen.ActivateOnEnable)
        {
            Activate();
        };
    }

    private void Start()
    {
        if (activate == ActivateWhen.ActivateOnStart)
        {
            Activate();
        };
    }

    private void Awake()
    {
        if (activate == ActivateWhen.ActivateOnAwake)
        {
            Activate();
        };
    }

    public void Activate()
    {
        debug.active = (debug.active || Frame.core.events.debugAllEvents);
        
        debug.Log("EventManager - Activate()");

        if (activated && !loop && singleUse )
        {
            debug.Log("EventManager - Cannot Activate - Already Activated");
            return;
        };

        activated = true;
        
        float queueTime = 0f;

        foreach (FrameCoreEvent frameCoreEvent in events)
        {

            frameCoreEvent.Activate(queueTime);

            if (queueInSuccession)
            {
                queueTime += frameCoreEvent.waitTime;
            };
        };


        if (loop)
        {
            events[events.Count - 1].eventManager = this;
        };
    }



    public void ActivateLoop()
    {
        StartCoroutine(Loop());
    }


    IEnumerator Loop()
    {
        //returning 0 will make it wait 1 frame
        yield return 0;

        debug.Log("EventManager - Loop()");

        Activate();
    }

}
