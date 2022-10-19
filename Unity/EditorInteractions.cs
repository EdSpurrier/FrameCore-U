using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using Sirenix.OdinInspector;

public static class EditorInteractions
{
    


    public static void FocusCameraBounds(Bounds focusBounds)
    {
        if (EditorInteractions.InEditorButton())
        {
#if UNITY_EDITOR
            SceneView.lastActiveSceneView.Frame(focusBounds, false);
#endif
        };
    }


    public static void FocusCamera(Vector3 focusPoint, float zoom = 1f)
    {

        if (zoom != -1f)
        {
            zoom *= -1;
        };

        if (EditorInteractions.InEditorButton())
        {
#if UNITY_EDITOR
            SceneView.lastActiveSceneView.Frame(new Bounds(focusPoint, (Vector3.one * zoom)), false);
#endif
        };
    }

    public static void AddToPool(Transform prefab)
    {
        if (EditorInteractions.InEditorButton())
        {
            GameObject poolCore = GameObject.Find("PoolCore");
            poolCore.GetComponent<PoolCore>().AddToPool(prefab);
#if UNITY_EDITOR
            UnityEditor.Selection.activeGameObject = poolCore;
            EditorInteractions.SetDirty(poolCore.GetComponent<PoolCore>());
#endif
        };
    }


    public static void SelectThis(GameObject thisObject)
    {
        if (EditorInteractions.InEditorButton())
        {
#if UNITY_EDITOR
            Selection.activeObject = thisObject;
#endif
        };
    }

    public static void SetDirty(Object dirtyObject)
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(dirtyObject);
#endif
    }

    public static bool InPlayerButton()
    {
        if (Application.isEditor && Application.isPlaying)
        {
            return true;
        }
        else
        {
            Debug.Log("WARNING >> Cannot Run This Function Unless In Play Mode.");
            return false;
        };
    }

    public static bool InEditorButton()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            return true;

            
        }
        else
        {
            Debug.Log("WARNING >> Cannot Run This Function Unless In Editor Mode.");
            return false;
        };
    }
}
