using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ListOfObjects))]
public class RaycastTrigger : MonoBehaviour
{

    [Title("Settings")]
    [InlineButton("TriggerRaycast")]
    public bool activateOnStart = false;
    public bool singleTrigger = false;
    public UpdateType updateType = UpdateType.LateUpdate;
    public bool transformForwardAim = true;

    public void TriggerRaycast()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }

    [Title("Raycast Trigger")]
    public LayerMaskNames layerMask;
    [InlineButton("Setup")]
    public Transform origin;
    public Transform hitPoint;


    public void Setup()
    {
        if (EditorInteractions.InEditorButton())
        {
            if (!hitPoint)
            {
                hitPoint = transform.FindChildWithName("Hit Point");
            };
            
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

            if (!origin)
            {
                origin = transform;
            };

            hitObjects = GetComponent<ListOfObjects>();
            if (!hitObjects)
            {
                hitObjects = gameObject.AddComponent<ListOfObjects>();
            };
            hitObjects.listName = "Hit Objects With Hit Point & Float";
            hitObjects.listType = ListType.GameObjectsWithHitPointAndFloat;
        };
    }

    [Title("Trigger Controller")]
    [OdinSerialize]
    [HideLabel]
    public TriggerTypeController type;

    [Space]

    [Title("Distance & Fall Off Settings")]
    public bool distanceFallOff = true;

    [ShowIf("distanceFallOff", true)]
    [HideLabel]
    public MaxMinFloat distanceAndFallOff = new MaxMinFloat
    {
        min = 50f,
        max = 100f
    };

    [ShowIf("@!this.distanceFallOff")]
    public float distance = 100f;

    [Title("Pre Raycast Event")]
    public bool preRaycastEventActive = false;
    [ShowIf("preRaycastEventActive")]
    [HideLabel]
    public FrameCoreEvent preRaycastEvent = new FrameCoreEvent
    {
        eventName = "Pre Raycast"
    };


    [Title("Post Raycast Event")]
    [HideLabel]
    public FrameCoreEvent postRaycastEvent = new FrameCoreEvent {
        eventName = "Post Raycast"
    };

    [Space]

    [Title("Hit Objects")]
    [HideLabel]
    public ListOfObjects hitObjects;

    [Title("System")]
    public bool active = false;

    [HideLabel]
    public DeBugger debug;

    private RaycastHit hit;


    public void Activate()
    {
        active = true;
        Raycast();
    }



    private void Start()
    {
        if (activateOnStart)
        {
            Activate();
        };
    }


    /// <summary>
    /// Execute methods responsible for component's behaviour
    /// </summary>
    void LateUpdate()
    {
        if (updateType != UpdateType.LateUpdate) return;
        UpdateMethods();
    }

    void Update()
    {
        if (updateType != UpdateType.Update) return;
        UpdateMethods();
    }

    void FixedUpdate()
    {
        if (updateType != UpdateType.FixedUpdate) return;
        UpdateMethods();
    }


    void UpdateMethods()
    {
        if (active)
        {
            Raycast();

            if (singleTrigger)
            {
                active = false;
            };
        };
    }


    public void Raycast()
    {
        if (!type.IsTriggerAllowed())
        {
            active = false;
            return;
        };
        
        if (preRaycastEventActive)
        {
            preRaycastEvent.Activate();
        };

        Vector3 fwd = origin.TransformDirection(Vector3.forward);

        if (transformForwardAim)
        {
            fwd = origin.forward;
        };

        if (distanceFallOff)
        {
            distance = distanceAndFallOff.max;
        };

        if (Physics.Raycast(origin.position, fwd, out hit, distance, Frame.core.layerMasks.GetLayerMask(layerMask)))
        {
            if (Frame.core.layerMasks.InLayerMask(layerMask, hit.collider.gameObject))
            {
                Debug.DrawLine(origin.position, hit.point, Color.green);
                hitPoint.position = hit.point;

                float hitMultiplier = 1f;

                if (distanceFallOff)
                {
                    hitMultiplier = Vector3.Distance((fwd * distanceAndFallOff.min), hit.point) / (distanceAndFallOff.max - distanceAndFallOff.min);
                };

                debug.Log("hitMultipler = " + hitMultiplier);

                Vector3 inverseDirection = origin.position - hitPoint.position;

                hitObjects.AddGameObjectWithHitPointAndFloat(hit.collider.gameObject, hitMultiplier, hit.point, Quaternion.LookRotation(inverseDirection));
            }
            else {
                PositionMissedHitPoint(fwd);
            };
        }
        else
        {
            PositionMissedHitPoint(fwd);
        };

        postRaycastEvent.Activate();
    }


    void PositionMissedHitPoint(Vector3 fwd)
    {
        Debug.DrawRay(origin.position, fwd * distance, Color.red);
        hitPoint.position = origin.position + (fwd * distance);
    }
}
