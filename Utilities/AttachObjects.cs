using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttachmentObject
{
    public Transform attachmentObject;
    public Transform attachmentRoot;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;


    public void DebugPosition()
    {
        if (!attachmentRoot)
        {
            return;
        };
        attachmentObject.position = attachmentRoot.position + positionOffset;
        attachmentObject.eulerAngles = attachmentRoot.eulerAngles + rotationOffset;
    }

    public void Attach()
    {
        if (!attachmentRoot)
        {
            originalParent = null;
            return;
        };
        originalParent = attachmentObject.parent;
        attachmentObject.parent = attachmentRoot;
        attachmentObject.localPosition = positionOffset;
        attachmentObject.localEulerAngles = rotationOffset;
    }


    public void Detach()
    {
        if (originalParent)
        {
            attachmentObject.parent = originalParent;
        };
    }

    Transform originalParent;
}


public class AttachObjects : MonoBehaviour
{
    

    public bool disableAutoReposition = false;
    public List<AttachmentObject> attachmentObjects;


    private void Awake()
    {
        AttachAllAttachObjects();
    }

    private void OnValidate()
    {

        if (!disableAutoReposition)
        {
            attachmentObjects.ForEach(x => x.DebugPosition());
        };
        
    }


    public void AttachAllAttachObjects()
    {
        attachmentObjects.ForEach(x => x.Attach());
    }
    public void DetachAllAttachObjects()
    {
        attachmentObjects.ForEach(x => x.Detach());
    }
}
