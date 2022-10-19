using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPrefAction
{
    Save,
    Load
};


[System.Serializable]
public class Action_PlayerPrefs
{


    [HorizontalGroup("Split", 0.4f)]
    [HideLabel]
    [SuffixLabel("PlayerPref Id", Overlay = true)]
    public string playerPrefId;

    [HorizontalGroup("Split", 0.30f)]
    [HideLabel]
    public PlayerPrefAction action;
    
    [HorizontalGroup("Split", 0.30f)]
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

    [HideLabel]
    public DeBugger debug;

    private void TriggerPlayerPref()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    public void Activate()
    {

        if (action == PlayerPrefAction.Save || !PlayerPrefs.HasKey(playerPrefId))
        {
            if (playerPrefType == PlayerPrefType.String)
            {
                PlayerPrefs.SetString(playerPrefId, stringValue);
            }
            else if (playerPrefType == PlayerPrefType.Int)
            {
                PlayerPrefs.SetInt(playerPrefId, intValue);
            }
            else if (playerPrefType == PlayerPrefType.Float)
            {
                PlayerPrefs.SetFloat(playerPrefId, floatValue);
            };

            debug.Log("Player Pref Saved >> " + playerPrefId + " => " + playerPrefType);

        } else {

            if (playerPrefType == PlayerPrefType.String)
            {
                stringValue = PlayerPrefs.GetString(playerPrefId);
            }
            else if (playerPrefType == PlayerPrefType.Int)
            {
                intValue = PlayerPrefs.GetInt(playerPrefId);
            }
            else if (playerPrefType == PlayerPrefType.Float)
            {

                floatValue = PlayerPrefs.GetFloat(playerPrefId);
            };

            debug.Log("Player Pref Loaded >> " + playerPrefId + " => " + playerPrefType);
        };

        

        
    }
}
