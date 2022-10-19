using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class EventLibrary : MonoBehaviour
{

    public List<FrameCoreEvent> events;

    public void TriggerEvent(string eventName)
    {

        FrameCoreEvent frameCoreEvent = events.FirstOrDefault(frameCoreEventObject => frameCoreEventObject.eventName == eventName);

        if (frameCoreEvent == null)
        {
            Debug.LogError("No FrameCoreEvent Trigger Found = " + eventName);
            Debug.Break();
        }
        else {
            frameCoreEvent.Activate();
        };

    }

}
