using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class EventManagerLibrary : MonoBehaviour
{

    [System.Serializable]
    public class EventManagerObject
    {
        [InlineButton("TriggerEvent")]
        [HideLabel]
        public string eventName = "Event";

        [HideLabel]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public EventManager eventManager;

        public void TriggerEvent()
        {
            if (EditorInteractions.InPlayerButton())
            {
                eventManager.Activate();
            };
        }
    }

    public List<EventManagerObject> events;

    public void TriggerRandomEventManager()
    {
        events[Random.Range(0, (events.Count - 1))].eventManager.Activate();
    }

    public void TriggerEventManager(string eventName)
    {

        EventManagerObject eventManagerObject = events.FirstOrDefault(eventMO => eventMO.eventName == eventName);

        if (eventManagerObject == null)
        {
            Debug.LogError("No Event Manager Trigger Found = " + eventName);
            Debug.Break();
        }
        else {
            eventManagerObject.eventManager.Activate();
        };

    }

    private void OnValidate()
    {
        events.ForEach(eventManager => {
            if (eventManager.eventManager)
            {
                if (eventManager.eventName == "Event")
                {
                    eventManager.eventName = eventManager.eventManager.eventManagerName;
                };
            };
        });
    }

}
