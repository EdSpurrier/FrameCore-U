using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ListOfObjects))]
public class ImpactTrigger : MonoBehaviour
{
    [Title("Impact Trigger")]
    public IgnoreCollision ignoreCollisions;

    [PropertySpace(20)]

    [Title("Settings")]
    [InlineButton("Setup")]
    public LayerMaskNames layerMask;
    [InfoBox("Impact Effects On This Collider Only")]
    public bool impactColliderOnly = false;
    [ShowIf("impactColliderOnly")]
    public Collider coll;

    [PropertySpace(20)]

    [Title("Parts")]
    public Transform hitPoint;

    [PropertySpace(20)]

    private void Start()
    {
        ignoreCollisions.Init();
    }



    public void Setup()
    {
        if (EditorInteractions.InEditorButton())
        {
            hitPoint = transform.FindChildWithName("Hit Point");

            if (!hitPoint)
            {
                Debug.Log("No Hit Point Transform Found!");
                Debug.Log("Creating New Hit Point Transform...");
                GameObject newHitPoint = new GameObject();
                newHitPoint.name = "Hit Point";
                newHitPoint.transform.parent = transform;
                newHitPoint.transform.localPosition = Vector3.zero;
                newHitPoint.transform.localRotation = Quaternion.identity;
                hitPoint = newHitPoint.transform;
            };

            coll = GetComponent<Collider>();

            if (!coll)
            {
                Debug.LogError("No Collider Attached! Please Attach & Connect One!");
            };

            hitObjects = GetComponent<ListOfObjects>();
            if (!hitObjects)
            {
                hitObjects = gameObject.AddComponent<ListOfObjects>();
            };
            hitObjects.listName = "Hit Objects";
            hitObjects.listType = ListType.GameObjects;


            ignoreCollisions.rigidbody = GetComponent<Rigidbody>();

            if (!coll)
            {
                Debug.LogError("No Rigidbody Attached! Please Attach & Connect One!");
            };
        };
    }


    [Title("Trigger Controller")]
    [OdinSerialize]
    [HideLabel]
    public TriggerTypeController type;

    [PropertySpace(20)]

    [Title("Events")]
    public bool enterEvents = false;
    [ShowIf("enterEvents")]
    [FoldoutGroup("Enter Trigger")]
    [HideLabel]
    public FrameCoreEvent enterEvent = new FrameCoreEvent
    {
        eventName = "Enter"
    };

    public bool exitEvents = false;
    [ShowIf("exitEvents")]
    [FoldoutGroup("Exit Trigger")]
    [HideLabel]
    public FrameCoreEvent exitEvent = new FrameCoreEvent
    {
        eventName = "Exit"
    };

    [PropertySpace(20)]

    [Title("Hit Objects")]
    [HideLabel]
    public ListOfObjects hitObjects;

    [PropertySpace(20)]

    [Title("System")]
    [HideLabel]
    public DeBugger debug;


    public bool CheckObjectInTriggerMask(GameObject thisGameObject)
    {
        bool result = false;

        if (Frame.core.layerMasks.InLayerMask(layerMask, thisGameObject))
        {
            debug.Log("Impact With Object Allowed = " + thisGameObject.name);
            if (type.IsTriggerAllowed())
            {
                debug.Log("Trigger Allowed");
                result = true;
            };
        }
        else {
            debug.Log("No Impact With Object = " + thisGameObject.name);
        };

        return result;
    }




    //  TRIGGERS
    private void OnTriggerEnter(Collider other)
    {


        if (CheckObjectInTriggerMask(other.gameObject))
        {
            hitObjects.AddGameObject(other.gameObject);

            hitPoint.position = coll.ClosestPoint(other.transform.position);
            hitPoint.rotation = transform.rotation;
            enterEvent.Activate();
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckObjectInTriggerMask(other.gameObject))
        {
            hitObjects.RemoveGameObject(other.gameObject);

            hitPoint.position = coll.ClosestPoint(other.transform.position);
            hitPoint.rotation = transform.rotation;

            exitEvent.Activate();
        };
    }


    //  COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {



        if (impactColliderOnly)
        {
            if (coll != collision.contacts[0].thisCollider)
            {
                Debug.Log("Not This Collider");
                return;
            };
        };
        

        GameObject collisionObject = collision.contacts[0].otherCollider.gameObject;



        if (CheckObjectInTriggerMask(collisionObject))
        {
            //Debug.Log("Is In Trigger Mask!");

            hitObjects.AddGameObject(collisionObject);

            hitPoint.position = collision.contacts[0].point;
            hitPoint.rotation = Quaternion.LookRotation(collision.contacts[0].normal);

            enterEvent.Activate();
        };
    }

    private void OnCollisionExit(Collision collision)
    {
        if (impactColliderOnly)
        {
            if (coll != collision.contacts[0].thisCollider)
            {
                Debug.Log("Not This Collider");
                return;
            };
        };

        GameObject collisionObject = collision.gameObject;


        if (CheckObjectInTriggerMask(collisionObject))
        {
            hitObjects.RemoveGameObject(collisionObject);
        };
    }
}
