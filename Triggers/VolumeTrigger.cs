using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTrigger : MonoBehaviour
{
    [Title("Volume Trigger")]
    public LayerMaskNames layerMask;
    public MaxMinInt volumeBoundsInclusive = new MaxMinInt {
        min = 1,
        max = 10000
    };

    public bool activated = false;

    public bool enterEvents = false;
    [ShowIf("enterEvents")]
    [FoldoutGroup("Enter Trigger")]
    [HideLabel]
    public FrameCoreEvent enterEvent;


    public bool exitEvents = false;
    [ShowIf("exitEvents")]
    [FoldoutGroup("Exit Trigger")]
    [HideLabel]
    public FrameCoreEvent exitEvent;



    [FoldoutGroup("Objects In Trigger")]
    public List<GameObject> gameObjectsInTrigger;

    [Title("System")]
    [HideLabel]
    public DeBugger debug;



    bool CheckIfInTrigger(GameObject thisGameObject)
    {
        bool result = false;

        if (!gameObjectsInTrigger.Contains(thisGameObject))
        {
            if (Frame.core.layerMasks.InLayerMask(layerMask, thisGameObject))
            {
                gameObjectsInTrigger.Add(thisGameObject);
                result = true;
            };
        };

        return result;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (CheckIfInTrigger(other.gameObject))
        {
            debug.Log("Object Entered & Accepted = " + other.gameObject.name);
            
            if (activated)
            {
                return;
            };

            if ( gameObjectsInTrigger.Count.BetweenRangeInt(volumeBoundsInclusive.min, volumeBoundsInclusive.max, true) )
            {
                debug.Log("Trigger Enter Activated");
                enterEvent.Activate();
                activated = true;
            };
        };

    }



    private void OnTriggerExit(Collider other)
    {
        if (gameObjectsInTrigger.Contains(other.gameObject))
        {
            gameObjectsInTrigger.Remove(other.gameObject);

            debug.Log("Object Exited & Removed = " + other.gameObject.name);

            if (!activated)
            {
                return;
            };

            if ( !gameObjectsInTrigger.Count.BetweenRangeInt(volumeBoundsInclusive.min, volumeBoundsInclusive.max, true) )
            {
                debug.Log("Trigger Exit Activated");
                exitEvent.Activate();
                activated = false;
            };
        };
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
