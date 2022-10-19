using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LayerMaskNames
{
    None,
    Interactable,
    CollisionSounds,
    Ground,
    Player,
    Weapon,
    PlayerPoint
}

[System.Serializable]
public class LayerMasks
{
    [System.Serializable]
    public class LayerMaskProperty
    {
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        public LayerMaskNames maskName;
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        public LayerMask layerMask;
    }

    public List<LayerMaskProperty> masks;

    

    public LayerMask GetLayerMask(LayerMaskNames maskName)
    {
        return masks.First(mask => mask.maskName == maskName).layerMask;
    }

    public bool InLayerMask(LayerMaskNames maskName, GameObject gameObject)
    {
        return GetLayerMask(maskName).IsInLayerMask(gameObject);
    }
}
