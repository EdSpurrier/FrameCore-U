using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DeBugger
{
    [LabelText("Debug")]
    public bool active = false;

    public void Log(string output)
    {
        if (active)
        {
            Debug.Log(output);
        };
    }

}
