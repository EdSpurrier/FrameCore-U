using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HighlightPlus;
using System.Linq;

public enum HighlightType
{
    None,
    Interact,
    Pickup,
    Control,
    Weapon,
    Important,
    Inanimate,
    Ammo
}

[System.Serializable]
public class Highlight
{
    [System.Serializable]
    public class HighlightProperty
    {
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        public HighlightType type;
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        public HighlightProfile profile;
    }

    public List<HighlightProperty> highlights;

    public HighlightProfile GetHighlightProfile(HighlightType type)
    {
        return highlights.First(highlight => highlight.type == type).profile;
    }

}
