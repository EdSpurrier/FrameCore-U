using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshUtilities : MonoBehaviour
{
    public static Vector3 GetClosestNavMeshPoint(Vector3 point, float multiplier)
    {
        NavMeshHit hit;

        if (multiplier < 1f)
        {
            multiplier = 1f;
        };

        //if (NavMesh.SamplePosition(point, out hit, (1.0f * multiplier), NavMesh.AllAreas))
        if (NavMesh.SamplePosition(point, out hit, (1.0f * multiplier), 1))
        {
            point = hit.position;
        }
        else {
            multiplier += 1f;
            GetClosestNavMeshPoint(point, multiplier);
        };

        return point;
    }

    public static Vector3 GetRandomNavMeshLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        RandomSeedGenerator.RandomizeSeed();

        int maxIndices = navMeshData.indices.Length - 3;
        // Pick the first indice of a random triangle in the nav mesh
        int firstVertexSelected = UnityEngine.Random.Range(0, maxIndices);
        int secondVertexSelected = UnityEngine.Random.Range(0, maxIndices);
        //Spawn on Verticies
        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];
        //Eliminate points that share a similar X or Z position to stop spawining in square grid line formations
        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x ||
            (int)firstVertexPosition.z == (int)secondVertexPosition.z
            )
        {
            point = GetRandomNavMeshLocation(); //Re-Roll a position - I'm not happy with this recursion it could be better
        }
        else
        {
            // Select a random point on it
            point = Vector3.Lerp(
                firstVertexPosition,
                secondVertexPosition, //[t + 1]],
                UnityEngine.Random.Range(0.05f, 0.95f) // Not using Random.value as clumps form around Verticies 
            );
        }
        //Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value); //Made Obsolete


        NavMeshHit hit;
        //if (NavMesh.SamplePosition(point, out hit, 1.0f, NavMesh.AllAreas))
        if (NavMesh.SamplePosition(point, out hit, 1.0f, 1))
        {
            point = hit.position;
        }
        else {
            point = GetRandomNavMeshLocation(); //Re-Roll a position - I'm not happy with this recursion it could be better
        };


        return point;
    }




    public static Vector3 RandomDistanceBasedLocation(Vector3 origin, float dist)
    {
        // GET RANDOM DIRECTION
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        Vector3 point = origin;


        NavMeshHit hit;

        for (int i = 0; i < 10; i++)    //  RE-ROLL MAX 10 TIMES OTHERWISE RETURN ORIGIN
        {
            //if (NavMesh.SamplePosition(randDirection, out hit, dist, NavMesh.AllAreas))
            if (NavMesh.SamplePosition(randDirection, out hit, dist, 1))
            {
                point = hit.position;
                break;
            };
        };
        
        return point;

    }
}
