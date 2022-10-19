using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventLibrary : MonoBehaviour
{
    public EventLibrary eventLibrary;

    public void TriggerEvent(string eventName)
    {
        eventLibrary.TriggerEvent(eventName);
    }
}
