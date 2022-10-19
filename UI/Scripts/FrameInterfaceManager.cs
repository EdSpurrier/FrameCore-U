using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public static class FrameInterface
{
    public static FrameInterfaceManager core;
    public static FrameInterfaceButtonManager buttonManager;

}




public enum Size
{
    Tiny,
    Small,
    Medium,
    Large,
    Huge
}


[System.Serializable]
public class IconSize
{
    public Size size = Size.Medium;
    public float px = 10;
}


public enum FontType
{
    Primary,
    Secondary,
    Tertiary,
}

[System.Serializable]
public class FontSize
{
    public Size size = Size.Medium;
    public int px = 10;
}

public class FrameInterfaceManager : MonoBehaviour
{
    [BoxGroup("Fonts")]
    [HideLabel]
    [SuffixLabel("Primary Font", Overlay = true)]
    public Font primaryFont;

    [BoxGroup("Fonts")]
    [HideLabel]
    [SuffixLabel("Secondary Font", Overlay = true)]
    public Font secondaryFont;

    [BoxGroup("Fonts")]
    public List<FontSize> fontSizes;


    [BoxGroup("Icons")]
    public List<IconSize> iconSizes;


    [FoldoutGroup("Parts")]
    public FrameInterfaceButtonManager frameInterfaceButtonManager;

    [FoldoutGroup("System")]
    [Button("Build")]
    public void Build()
    {
        FrameInterface.core = this;
        FrameInterface.buttonManager = frameInterfaceButtonManager;
        EditorInteractions.SetDirty(gameObject);
    }

    private void Awake()
    {
        Build();
    }


    public Vector2 GetIconSize(Size size)
    {
        IconSize iconSize = iconSizes.Find(icon => (icon.size == size));
        return new Vector2(iconSize.px, iconSize.px);
    }

    public int GetFontSize(Size size)
    {
        return fontSizes.Find(font => (font.size == size)).px;
    }

}
