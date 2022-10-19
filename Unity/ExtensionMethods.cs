using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using Random = System.Random;

[System.Serializable]
public class PushSettings
{
    public enum PushType
    {
        Direction,
        OriginToRigidbody,
        OriginToDestination
    }

    [Title("Push Settings")]
    public ForceMode forceMode;
    public ValueType valueType;
    public PushType pushType;
    [ShowIf("@this.pushType == PushType.OriginToDestination")]
    public bool forceAtPosition = true;

    [Title("PushType Settings")]
    [ShowIf("@this.pushType == PushType.OriginToDestination || this.pushType == PushType.OriginToRigidbody")]
    public Transform pushOrigin;

    [ShowIf("@this.pushType == PushType.OriginToDestination")]
    public Transform pushDestination;


    [ShowIf("@this.pushType == PushType.Direction")]
    public Vector3 direction = Vector3.forward;


    [Title("Push Velocity")]
    [ShowIf("valueType", ValueType.Random)]
    [HideLabel]
    public MaxMinFloat velocityRange = new MaxMinFloat
    {
        min = 100f,
        max = 200f
    };


    [ShowIf("valueType", ValueType.Set)]
    public float velocity = 500f;

    public void Push(Rigidbody rigidbody)
    {

        if (pushType == PushType.OriginToDestination && forceAtPosition)
        {
            rigidbody.AddForceAtPosition(GetForce(rigidbody), pushDestination.position, forceMode);
        }
        else {
            rigidbody.AddForce(GetForce(rigidbody), forceMode);
        };
    }


    public float GetVelocity()
    {
        if (valueType == ValueType.Random)
        {
            RandomSeedGenerator.RandomizeSeed();
            return UnityEngine.Random.Range(velocityRange.min, velocityRange.max);
        }
        else {
            return velocity;
        };
    }


    public Vector3 GetForce(Rigidbody rigidbody)
    {

        if (pushType == PushType.OriginToRigidbody)
        {
            Vector3 pushDirection = rigidbody.position - pushOrigin.position;
            return (pushDirection.normalized * GetVelocity());
        }
        else if (pushType == PushType.OriginToDestination)
        {
            Vector3 pushDirection = pushDestination.position - pushOrigin.position;
            return (pushDirection.normalized * GetVelocity());
        }
        else {
            return (direction.normalized * GetVelocity());
        };

    }



}


public enum PositionPointType
{
    Transform,
    Vector
}


public enum ActiveState
{
    Active,
    InActive
}


public enum LevelSetting
{
    VeryHigh,
    High,
    Medium,
    Low,
    VeryLow
}

public enum SwitchState
{
    On,
    Off
}

public enum AnimationLoopMode
{
    Once,
    Loop,
    PingPong
}

public enum UpdateType
{
    Update,
    LateUpdate,
    FixedUpdate
}

public enum ValueType
{
    Set,
    Random
}

public enum RandomPositionType
{
    Vector3,
    Collider,
    NavMeshWithinCollider,
    NavMesh
}

public enum ColliderType
{
    Mesh,
    Box,
    Sphere
}

public enum PositionType
{
    Local,
    World
}

public enum RotationType
{
    LocalRotation,
    Rotation
}



public enum ActivateWhen
{
    ActivateOnStart,
    ActivateOnEnable,
    ActivateFromExternalTrigger,
    ActivateOnAwake,
}

public enum Axis
{
    x,
    y,
    z,
    All
}

[System.Serializable]
public class AxisValue
{
    public bool active = true;
    public float value = 1f;
}


[System.Serializable]
public class AxisValues
{
    public AxisValue x;
    public AxisValue y;
    public AxisValue z;
}

[System.Serializable]
public class MaxMinVector3
{
    public Vector3 min = Vector3.zero;
    public Vector3 max = new Vector3(1, 1, 1);
}


[System.Serializable]
public class GameObjectAndName
{
    public string name;
    public GameObject gameObject;
}


[System.Serializable]
public class MaxMinFloat
{
    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    [SuffixLabel("(float) Min", Overlay = true)]
    public float min = -1f;
    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    [SuffixLabel("(float) Max", Overlay = true)]
    public float max = 1f;
}

[System.Serializable]
public class MaxMinInt
{
    public int min = -1;
    public int max = 1;
}

public enum Side {
    Left,
    Right
}

public enum Direction {
    None,
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward,
    SidewaysLeft,
    SidewaysRight
}

public enum AxisDirection
{
    Vertical,
    Horizontal
}


[System.Serializable]
public class ActiveAxis
{
    public bool x = false;
    public bool y = false;
    public bool z = false;
}


public enum AnimationType
{
    Trigger,
    Bool,
    Float,
    Int
}

[System.Serializable]
public class AnimationController
{
    public bool active = false;
    public string parameterName = "Parameter Name";
    public Animator animator;

    public void Trigger()
    {
        if (!active)
        {
            return;
        };

        animator.SetTrigger(parameterName);
    }

    public void Trigger(bool value)
    {
        if (!active)
        {
            return;
        };
        animator.SetBool(parameterName, value);
    }

    public void Trigger(float value)
    {
        if (!active)
        {
            return;
        };
        animator.SetFloat(parameterName, value);
    }
}






//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{


    public static void ParentTo(this Transform child, Transform parent, bool autoAlign = true)
    {
        child.parent = parent;
        if (autoAlign)
        {
            child.position = parent.position;
            child.rotation = parent.rotation;
        };
    }

    public static bool RandomPercent(this int truePercentage)
    {
        Random r = new Random();
        return r.NextDouble() < truePercentage / 100.0;
    }



    public static void Push(this Rigidbody rigidbody, PushSettings pushSettings)
    {
        pushSettings.Push(rigidbody);
    }



    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(value.x, min.x, max.x),
            Mathf.Clamp(value.y, min.y, max.y),
            Mathf.Clamp(value.z, min.z, max.z)
            );
    }


    //===========//
    //  MACHIN3  //
    //===========//
    public static float Remap(this float value, float fromin, float toin, float fromout = 0, float toout = 1)
    {
        float remapped;

        remapped = (value - fromin) / (toin - fromin) * (toout - fromout) + fromout;

        if (remapped < fromout)
        {
            return fromout;
        }
        else if (remapped > toout)
        {
            return toout;
        }
        else
        {
            return remapped;
        }
    }

    public static float EaseInOut(this float value, float startvalue = 0, float endvalue = 1, float starttime = 0, float endtime = 1)
    {
        AnimationCurve curve = AnimationCurve.EaseInOut(starttime, startvalue, endtime, endvalue);

        return curve.Evaluate(value);
    }
    //===========//
    //===========//
    //===========//

    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.


    public static Vector3 ColliderBoundsCenterWorldPoint(this Collider collider)
    {
        return collider.transform.TransformPoint(collider.bounds.center);
    }






    public static Vector3 GetCenterOfChildRenderers(this GameObject parentSearchObject)
    {
        List<Renderer> allDescendants = parentSearchObject.GetAllChildMeshRenderers();

        Bounds bounds = new Bounds();

        if (allDescendants.Count > 0)
        {
            bounds = allDescendants[0].bounds;
        };

        foreach (Renderer childRenderer in allDescendants)
        {
            bounds = bounds.GrowBounds(childRenderer.bounds);
        };

        return bounds.center;
    }


    public static Bounds GetBoundsOfChildRenderers(this GameObject parentSearchObject, Vector3 boundsBasePoint, Vector3 expand)
    {
        List<Renderer> allDescendants = parentSearchObject.GetAllChildMeshRenderers();

        Bounds bounds = new Bounds();

        bounds.center = boundsBasePoint;
        bounds.size = Vector3.zero;

        foreach (Renderer childRenderer in allDescendants)
        {
            bounds = bounds.GrowBounds(childRenderer.bounds);
        };

        bounds.size += expand;


        return bounds;
    }


    public static List<Renderer> GetAllChildMeshRenderers(this GameObject parent)
    {
        List<Renderer> renderers = new List<Renderer>();


        MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer rend in meshRenderers)
        {
            renderers.Add(rend);
        };


        SkinnedMeshRenderer[] skinnedMeshRenderers = parent.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer rend in skinnedMeshRenderers)
        {
            renderers.Add(rend);
        };


        return renderers;
    }

    public static Bounds GrowBounds(this Bounds a, Bounds b)
    {
        Vector3 max = Vector3.Max(a.max, b.max);
        Vector3 min = Vector3.Min(a.min, b.min);

        a = new Bounds((max + min) * 0.5f, max - min);
        return a;
    }

    static public List<Transform> FindChildrenOneLevelDeep(this Transform parent)
    {
        List<Transform> found = new List<Transform>();

        foreach (Transform child in parent)
        {
            if (child.parent == parent)
            {
                found.Add(child);
            };
        };

        return found;
    }


    public static Vector3 GetVelocity(this Vector3 newPosition, Vector3 lastPosition, float tolerance = 0f)
    {
        if (lastPosition != newPosition)
        {
            Vector3 velocity = newPosition - lastPosition;
            velocity /= Time.deltaTime;
            if (velocity.magnitude > tolerance)
            {
                return velocity;
            }
            else {
                return Vector3.zero;
            };
        }
        else
        {
            return Vector3.zero;
        };
    }




    public static Vector3 Direction(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }



    static public Transform FindChildWithName(this Transform parent, string name)
    {
        Transform found = null;

        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                found = child;
            };
        };

        return found;
    }




    public static float ReturnFloatToZero(this float value, float speed, float deadzone = 0f)
    {
        if (value > deadzone)
        {
            value -= speed;

            if (value < 0f)
            {
                value = 0f;
            };

        } else if (value < deadzone)
        {
            value += speed;

            if (value > 0f)
            {
                value = 0f;
            };
        };

        return value;
    }






    /// <summary>
    /// Gets the declared type of the specified object.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// A <see cref="Type"/> object representing type 
    /// <typeparamref name="T"/>; i.e., the type of <paramref name="obj"/> 
    /// as it was declared. Note that the contents of 
    /// <paramref name="obj"/> are irrelevant; if <paramref name="obj"/> 
    /// contains an object whose class is derived from 
    /// <typeparamref name="T"/>, then <typeparamref name="T"/> is 
    /// returned, not the derived type.
    /// </returns>
    public static Type GetDeclaredType<T>(
        this T obj)
    {
        return (obj != null) ? obj.GetType() : typeof(T);
    }






    public static bool IsInLayerMask(this LayerMask hitMask, GameObject gameObject)
    {
        return hitMask == (hitMask | (1 << gameObject.layer));
    }

    public static Transform SpawnTransform(this Transform obj, Vector3 position, Quaternion rotation)
    {
        return MonoBehaviour.Instantiate(obj, position, rotation) as Transform;
    }



    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    


    static public List<GameObject> getAllChildGameObjects(this GameObject fromGameObject, string withName = "")
    {
        List<GameObject> foundObjects = new List<GameObject>();

        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (withName == "") {
                foundObjects.Add(t.gameObject);
            } else if (t.gameObject.name == withName)
            {
                foundObjects.Add(t.gameObject);
            };
        };

        return foundObjects;
    }

    static public GameObject getChildGameObject(this GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    public static string GetTime(this float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }



    public static bool ChancePercentage(this int chance)
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        int perCent = UnityEngine.Random.Range(0, 100);

        if (perCent < chance)
        {
            return true;
        }
        else {
            return false;
        };
    }

 


    public static Vector3 RandomizedNavMovePosition(this Transform transform, Vector3 minRange, Vector3 maxRange)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomizedPoint = new Vector3(
                UnityEngine.Random.Range(minRange.x, maxRange.x),
                UnityEngine.Random.Range(minRange.y, maxRange.y),
                UnityEngine.Random.Range(minRange.z, maxRange.z)
                ) + transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomizedPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            };
        };

        return transform.position + Vector3.zero;
    }


    public static bool BetweenVarianceFloat(this float thisNumber, float baseNumber, float variance)
    {

        return thisNumber.BetweenRangeFloat((baseNumber - variance), (baseNumber + variance));

    }


    public static bool InRange(this MaxMinFloat maxMin, float value, bool inclusive = false) {

        if (maxMin.min < value && value < maxMin.max)
        {
            return true;
        } else if (
            inclusive && maxMin.min == value ||
            inclusive && maxMin.max == value
            ) {
            return true;
        }
        else
        {
            return false;
        };

    }

    public static bool BetweenRangeFloat(this float thisNumber, float max, float min)
    {

        if (min < thisNumber && thisNumber < max)
        {
            return true;
        } else {
            return false;
        };

    }

    public static bool BetweenRangeInt(this int thisNumber, int min, int max, bool inclusive)
    {

        if (
            min < thisNumber && 
            thisNumber < max ||
            inclusive && min == thisNumber ||
            inclusive && max == thisNumber
            )
        {
            return true;
        }
        else
        {
            return false;
        };
    }

    public static float Difference(this float nr1, float nr2)
    {
        return (float)Math.Abs((double)nr1 - (double)nr2);
    }

    public static void SpawnObjectsToParent(this List<Transform> transforms, Vector3 position, Quaternion rotation, Transform parent)
    {
        foreach (Transform transform in transforms)
        {
            GameObject thisObject = Frame.core.pools.SpawnObject(transform, position, rotation);
            thisObject.transform.parent = parent;
        };
    }

    public static void SpawnObjects(this List<Transform> transforms, Vector3 position, Quaternion rotation)
    {
        foreach (Transform transform in transforms)
        {
            Frame.core.pools.SpawnObject(transform, position, rotation);
        };
    }

    public static GameObject SpawnObject(this Transform transform, Vector3 position, Quaternion rotation)
    {
        return Frame.core.pools.SpawnObject(transform, position, rotation);
    }

    public static void SetGameobjectsState(this List<GameObject> gameObjects, bool state)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(state);
        };
    }

     

    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static float ClampAngle(this float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }


    public static float ClampEulerAngle(this float angle, float min, float max)
    {
        float newAngle = angle % 360;

        Debug.Log("newAngle:" + newAngle);

        if (newAngle > min)
        {

        }


        /*if ((newAngle >= -360F) && (newAngle <= 360F))
        {
            if (newAngle < -360F)
            {
                newAngle += 360F;
            }
            if (newAngle > 360F)
            {
                newAngle -= 360F;
            }
        }*/

        return angle; //Mathf.Clamp(newAngle, min, max);
    }
}
