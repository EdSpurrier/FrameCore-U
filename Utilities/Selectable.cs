using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;


[System.Serializable]
public class Selectable
{
    [InlineButton("TestDeselect")]
    [InlineButton("TestSelect")]
    public bool selected = false;

    public HighlightType highlightType;
    public HighlightEffect highlight;
    
    [FoldoutGroup("Select")]
    [HideLabel]
    public FrameCoreEvent selectEvent = new FrameCoreEvent {
        eventName = "Select"
    };

    [FoldoutGroup("Deselect")]
    [HideLabel]
    public FrameCoreEvent deselectEvent = new FrameCoreEvent
    {
        eventName = "Deselect"
    };

    public void TestSelect()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Select();
        };
    }
    public void TestDeselect()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Deselect();
        };
    }

    public void Init()
    {
        if (highlightType != HighlightType.None)
        {
            highlight.ProfileLoad(Frame.core.highlights.GetHighlightProfile(highlightType));
        };
    }

    public void Select()
    {
        if (!EditorInteractions.InPlayerButton())
        {
            return;
        };

        if (selected)
        {
            return;
        };

        selectEvent.Activate();

        if (highlightType != HighlightType.None)
        {
            highlight.highlighted = true;
        };

        selected = true;
    }


    public void Deselect()
    {

        if (!selected)
        {
            return;
        };

        if (highlightType != HighlightType.None)
        {
            highlight.highlighted = false;
        };

        deselectEvent.Activate();
        selected = false;
    }


    public void ToggleSelect(bool state)
    {
        if (state)
        {
            Select();
        }
        else
        {
            Deselect();
        };
    }
}
