using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [BoxGroup("Actions")]
    [FoldoutGroup("Actions/Freeze Player")]
    [HideLabel]
    public FrameCoreEvent freezeEvent = new FrameCoreEvent
    {
        eventName = "Freeze"
    };

    [FoldoutGroup("Actions/Unfreeze Player")]
    [HideLabel]
    public FrameCoreEvent unfreezeEvent = new FrameCoreEvent
    {
        eventName = "Unfreeze"
    };


    [BoxGroup("Player FX")]
    [FoldoutGroup("Player FX/Disappear")]
    [HideLabel]
    public FrameCoreEvent disappearEvent = new FrameCoreEvent
    {
        eventName = "Disappear"
    };

    [FoldoutGroup("Player FX/Reappear")]
    [HideLabel]
    public FrameCoreEvent reappearEvent = new FrameCoreEvent
    {
        eventName = "Reappear"
    };
   

    [Title("Parts")]
    public Transform playerBase;

    [Title("System")]
    public float currentVelocity = 0f;

    public Vector3 lastPosition = Vector3.zero;

    public void Awake()
    {

    }

    void FixedUpdate()
    {
        currentVelocity = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }


    public void UpdateController()
    {

    }

    public void FreezePlayer()
    {
        freezeEvent.Activate();
    } 

    public void UnfreezePlayer()
    {
        unfreezeEvent.Activate();
    }


}
