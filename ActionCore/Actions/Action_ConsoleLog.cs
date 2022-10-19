using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_ConsoleLog
{

    [HideLabel]
    [SuffixLabel("Console Text", Overlay = true)]
    public string consoleText = "";

    public void Activate()
    {
        Debug.Log(consoleText);
    }


}
