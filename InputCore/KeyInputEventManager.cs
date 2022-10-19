using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[System.Serializable]
public class KeyInputEventControl
{
    [FoldoutGroup("$eventName")]

    [HorizontalGroup("$eventName/Row_1")]
    [HideLabel]
    public KeyCode shortcut;
    
    [HorizontalGroup("$eventName/Row_1")]
    [HideLabel]
    public string eventName;

    [FoldoutGroup("$eventName/Input Event")]
    [HideLabel]
    public FrameCoreEvent inputEvent;
}

public class KeyInputEventManager : MonoBehaviour
{

    public List<KeyInputEventControl> keyEvents;

    private void OnValidate()
    {
        keyEvents.ForEach(keyEvents => {
            keyEvents.eventName = keyEvents.inputEvent.eventName;
        });
    }


    [FoldoutGroup("System")]
    [HideLabel]
    public DeBugger debug;


    // Update is called once per frame
    void Update()
    {
        keyEvents.ForEach(keyEvents => {
            if (Input.GetKeyDown(keyEvents.shortcut))
            {
                debug.Log(keyEvents.shortcut + " was pressed.");
                
                keyEvents.inputEvent.Activate();

            };
        });
    }
}
