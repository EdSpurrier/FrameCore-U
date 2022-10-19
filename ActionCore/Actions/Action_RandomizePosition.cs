using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_RandomizePosition
{

    [HideLabel]
    public RandomPositionType randomPositionType;

    [HorizontalGroup("Split")]
    [HideLabel]
    [VerticalGroup("Split/Left")]
    [ShowIf("randomPositionType", RandomPositionType.Vector3)]
    public PositionType positionType;

    [ShowIf("randomPositionType", RandomPositionType.Vector3)]
    [HideLabel]
    public MaxMinVector3 randomRange;


    [ShowIf("@this.randomPositionType == RandomPositionType.Collider || this.randomPositionType == RandomPositionType.NavMeshWithinCollider")]
    [HideLabel]
    [VerticalGroup("Split/Left")]
    public List<MeshCollider> colliders;


    public List<Transform> transforms;

    [Title("System")]
    public DeBugger debug;
    


    public void Activate()
    {
        foreach(Transform trans in transforms)
        {
            RandomTransformPosition(trans);
        };
    }

    public void RandomTransformPosition(Transform trans)
    {
        Vector3 newPosition = trans.position;

        


        if (randomPositionType == RandomPositionType.Vector3) {
            RandomSeedGenerator.RandomizeSeed();
            newPosition.x = Random.Range(randomRange.min.x, randomRange.max.x);
            RandomSeedGenerator.RandomizeSeed();
            newPosition.y = Random.Range(randomRange.min.y, randomRange.max.y);
            RandomSeedGenerator.RandomizeSeed();
            newPosition.z = Random.Range(randomRange.min.z, randomRange.max.z);

            if (positionType == PositionType.Local)
            {
                trans.localPosition = newPosition;
            }
            else if (positionType == PositionType.World)
            {
                trans.position = newPosition;
            };

        }
        else if (randomPositionType == RandomPositionType.Collider)
        {
            RandomSeedGenerator.RandomizeSeed();
            MeshCollider collider = colliders[Random.Range(0, colliders.Count)];
            newPosition = collider.transform.TransformPoint(collider.convex ? collider.sharedMesh.GetRandomPointInsideConvex() : collider.sharedMesh.GetRandomPointInsideNonConvex(collider.transform.position));
            trans.position = newPosition;
        }
        else if (randomPositionType == RandomPositionType.NavMeshWithinCollider)
        {
            RandomSeedGenerator.RandomizeSeed();
            MeshCollider collider = colliders[Random.Range(0, colliders.Count)];
            newPosition = collider.transform.TransformPoint(collider.convex? collider.sharedMesh.GetRandomPointInsideConvex() : collider.sharedMesh.GetRandomPointInsideNonConvex(collider.transform.position));

            newPosition = NavMeshUtilities.GetClosestNavMeshPoint(newPosition, 1.0f);

            trans.position = newPosition;
        }
        else if (randomPositionType == RandomPositionType.NavMesh)
        {
            newPosition = NavMeshUtilities.GetRandomNavMeshLocation();
            trans.position = newPosition;
        };


        


        
    }

    
}
