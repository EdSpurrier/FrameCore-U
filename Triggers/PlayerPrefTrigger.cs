using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPrefConditionalEvent
{

    [HorizontalGroup("Split", 0.5f)]
    [SuffixLabel("PlayerPref Id", Overlay = true)]
    [HideLabel]
    public string playerPrefId;

    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    public PlayerPrefType playerPrefType;

    [ShowIf("playerPrefType", PlayerPrefType.String)]
    [SuffixLabel("String", Overlay = true)]
    [HideLabel]
    public string stringValue = "";

    [ShowIf("playerPrefType", PlayerPrefType.Int)]
    [SuffixLabel("Int", Overlay = true)]
    [HideLabel]
    public int intValue = 0;

    [ShowIf("playerPrefType", PlayerPrefType.Float)]
    [SuffixLabel("Float", Overlay = true)]
    [HideLabel]
    public float floatValue = 0f;

    [FoldoutGroup("Conditional Event")]
    [HideLabel]
    public FrameCoreEvent conditionalEvent = new FrameCoreEvent
    {
        eventName = "Conditional Event"
    };


    public bool CheckIfValid()
    {
        if (!PlayerPrefs.HasKey(playerPrefId))
        {
            return false;
        };

        if (playerPrefType == PlayerPrefType.String)
        {
            return (stringValue == PlayerPrefs.GetString(playerPrefId));
        }
        else if (playerPrefType == PlayerPrefType.Int)
        {
            return (intValue == PlayerPrefs.GetInt(playerPrefId));
        }
        else if (playerPrefType == PlayerPrefType.Float)
        {
            return (floatValue == PlayerPrefs.GetFloat(playerPrefId));
        };

        return false;
    }


}

public class PlayerPrefTrigger : MonoBehaviour
{
    public ActivateWhen activate;

    [Title("System")]
    public bool activated = false;

    [Title("Events")]
    public List<PlayerPrefConditionalEvent> playerPrefEvents;

    [HideLabel]
    public DeBugger debug;

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

        debug.Log("PlayerPrefTrigger - Activate()");

        if (activated)
        {
            debug.Log("PlayerPrefTrigger - Cannot Activate - Already Activated");
            return;
        };

        activated = true;

        foreach (PlayerPrefConditionalEvent playerPrefEvent in playerPrefEvents)
        {
            if (playerPrefEvent.CheckIfValid())
            {
                playerPrefEvent.conditionalEvent.Activate();
            };
        };
    }
}
