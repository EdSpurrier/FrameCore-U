using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCore : MonoBehaviour
{


    [Title("System")]
    public float actionCoreTime = 0f;
    public DeBugger debug;
    public bool debugAllEvents;
    public bool dontClearComplete = false;


    // Update is called once per frame
    void Update()
    {
        actionCoreTime += Time.deltaTime;


        RemoveCompleted();
    }


    

    void RemoveCompleted()
    {

    }




    public List<GameObject> DestroyObjects(List<GameObject> destroyObjects)
    {

        foreach (GameObject destroyObject in destroyObjects)
        {
            Destroy(destroyObject);
        };

        destroyObjects.Clear();

        return destroyObjects;
    }
}
