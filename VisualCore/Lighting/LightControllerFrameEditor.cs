using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LightControllerFrameEditor : MonoBehaviour
{
    public LightController lightController;

    [PropertySpace(SpaceBefore = 30, SpaceAfter = 30)]
    [Button("Refresh / Setup", ButtonSizes.Medium), GUIColor(0.5f, 0.95f, 0.4f)]
    private void Refresh()
    {
        lightController = GetComponent<LightController>();
    }
}
