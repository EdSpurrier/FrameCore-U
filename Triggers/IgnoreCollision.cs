using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IgnoreCollision
{
    [Title("Settings")]
    public List<Collider> collisionsToDisable;

    public void Init()
    {
        foreach (Collider colliderToIgnore in collisionsToDisable)
        {
            IgnorePhysicsCollisions(colliderToIgnore, true);
        };
    }

    [Title("Disabled Collision Colliders")]
    public List<Collider> collisionsDisabled;

    //  IGNORE COLLISIONS FROM INTERACTABLE ITEM
    public void IgnorePhysicsCollision(Collider colliderToIgnore, bool ignoreState)
    {
        IgnorePhysicsCollisions(colliderToIgnore, ignoreState);
    }

    //  IGNORE COLLISIONS FROM INTERACTABLE ITEM
    public void IgnorePhysicsCollisions(List<Collider> collidersToIgnore, bool ignoreState)
    {
        foreach (Collider colliderToIgnore in collidersToIgnore)
        {
            IgnorePhysicsCollisions(colliderToIgnore, ignoreState);
        };
    }

    //  IGNORE COLLISIONS FROM SINGLE COLLIDER
    public void IgnorePhysicsCollisions(Collider colliderToIgnore, bool ignoreState)
    {

        foreach (Collider collider in colliders)
        {
            Physics.IgnoreCollision(collider, colliderToIgnore, ignoreState);
        };

        //  STORE COLLISIONS
        if (ignoreState)
        {
            if (!collisionsDisabled.Contains(colliderToIgnore))
            {
                collisionsDisabled.Add(colliderToIgnore);
            };
        }
        else {
            collisionsDisabled.Remove(colliderToIgnore);
        };

    }



    [Title("Ignored Collision Objects")]
    public List<GameObject> objectsToIgnore;

    // Start is called before the first frame update
    public void AddIgnoreObject(GameObject ignoreObject)
    {
        if (!objectsToIgnore.Contains(ignoreObject))
        {
            objectsToIgnore.Add(ignoreObject);
        };
    }

    public void RemoveIgnoreObject(GameObject ignoreObject)
    {
        if (objectsToIgnore.Contains(ignoreObject))
        {
            objectsToIgnore.Remove(ignoreObject);
        };
    }


    public bool CanCollide(GameObject collisionObject)
    {
        return !objectsToIgnore.Contains(collisionObject);
    }









    [Title("Parts")]
    public Rigidbody rigidbody;

    public List<Collider> colliders;

    [FoldoutGroup("System Settings/Parts")]
    [Button("Get Colliders")]
    void GetColliders()
    {
        colliders.Clear();

        Collider[] collidersFound = rigidbody.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider colliderFound in collidersFound)
        {
            if (!colliderFound.isTrigger && !colliders.Contains(colliderFound))
            {
                colliders.Add(colliderFound);
            };
        };

    }



    
}
