using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircleDraw : MonoBehaviour
{
    public static int segments = 32;
    public Color Color = Color.blue;
    public float XRadius = 2;
    public float ZRadius = 2;



    private void OnDrawGizmos()
    {
        //DrawEllipse(transform.position, transform.forward, transform.up, XRadius * transform.localScale.x, ZRadius * transform.localScale.y, Segments, Color);
    }

    public static void DrawEllipse(Vector3 pos, float radiusX, float radiusZ, Color color, float duration = 0)
    {
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }

/*
    public static void DrawEllipseSphere(Vector3 pos, float radiusX, float radiusZ, Color color, float duration = 0)
    {

        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }

        angle = 0f;
        rot = Quaternion.LookRotation(Vector3.right, Vector3.forward);
        lastPoint = Vector3.zero;
        thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }


        angle = 0f;
        rot = Quaternion.LookRotation(Vector3.forward, Vector3.right);
        lastPoint = Vector3.zero;
        thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color, duration);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }
*/

    public static void DrawEllipseSphere(Vector3 pos, float radiusX, float radiusZ, Color color)
    {

        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }

        angle = 0f;
        rot = Quaternion.LookRotation(Vector3.right, Vector3.forward);
        lastPoint = Vector3.zero;
        thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }


        angle = 0f;
        rot = Quaternion.LookRotation(Vector3.forward, Vector3.right);
        lastPoint = Vector3.zero;
        thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            thisPoint.z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusZ;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }
}