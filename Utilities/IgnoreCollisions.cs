using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class IgnoreCollisions : MonoBehaviour
{
    [HideLabel]
    public string ignoreName = "Ignore Reference Name";

    [TitleGroup("Parts")]
    public List<Collider> colliderIgnoreMaster;
    [TitleGroup("Parts")]
    public List<Collider> colliderIgnoreChildGroup;

    [FoldoutGroup("Setup")]
    [InlineButton("GetChildColliders", "Get Child Colliders")]
    public GameObject searchObject;

    private void GetChildColliders()
    {
        Collider[] childColliders = searchObject.GetComponentsInChildren<Collider>();
        
        collidersFound.Clear();
        
        foreach (Collider colliderChild in childColliders)
        {
            collidersFound.Add(colliderChild);
        };

    }

    [FoldoutGroup("Setup")]
    public List<Collider> collidersFound;

    [FoldoutGroup("Setup")]
    [Button(ButtonSizes.Small)]
    private void AddFoundCollidersToMaster()
    {
        collidersFound.ForEach(x => { 
            if (!colliderIgnoreMaster.Contains(x))
            {
                colliderIgnoreMaster.Add(x);
            };
        });
    }
    [FoldoutGroup("Setup")]
    [Button(ButtonSizes.Small)]
    private void AddFoundCollidersToChildGroup()
    {
        collidersFound.ForEach(x => {
            if (!colliderIgnoreChildGroup.Contains(x))
            {
                colliderIgnoreChildGroup.Add(x);
            };
        });
    }




    private void Awake()
    {
        foreach(Collider colliderMaster in colliderIgnoreMaster)
        {
            foreach (Collider colliderChild in colliderIgnoreChildGroup)
            {
                Physics.IgnoreCollision(colliderMaster, colliderChild);
            };
        };
    }


}
