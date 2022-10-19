#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightControllerFrameEditor))]
public class LightControllerFrameEditorEditor : Editor
{
    Color lightControllerColour = new Color(1f, 0f, 1f);

    void OnSceneGUI()
    {
        // get the chosen game object
        LightControllerFrameEditor t = target as LightControllerFrameEditor;
        Event e = Event.current;

        if (t == null)
        {
            return;
        };


        foreach (Light light in t.lightController.lights)
        {
            CircleDraw.DrawEllipseSphere(light.transform.position, 0.2f, 0.2f, lightControllerColour);
            CircleDraw.DrawEllipseSphere(light.transform.position, 0.4f, 0.4f, lightControllerColour);
        };

    }


}
#endif
