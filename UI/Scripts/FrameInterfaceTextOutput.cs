using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class FrameInterfaceTextOutput : MonoBehaviour
{

    [FoldoutGroup("States")]
    [HideLabel]
    [SuffixLabel("Output", Overlay = true)]
    public string textOutput = "";

    [FoldoutGroup("Settings")]


    [FoldoutGroup("Parts")]
    [HideLabel]
    [SuffixLabel("Text UI", Overlay = true)]
    public Text text;

/*    [FoldoutGroup("System Settings")]*/
    [FoldoutGroup("System")]
    [HideLabel]
    public DeBugger debug;


    private void OnValidate()
    {
        if (!text)
        {
            text = GetComponentInChildren<Text>();
        };

        if (text)
        {
            text.text = textOutput;
        }
        else {
            Debug.LogError("No Text Component Found...");
        };
    }


    public void UpdateText(string output)
    {
        textOutput = output;
        text.text = textOutput;
    }

}
