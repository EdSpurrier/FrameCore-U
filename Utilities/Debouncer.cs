using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Debounce
{
    [HorizontalGroup("Split", 0.7f)]
    [HideLabel]
    [SuffixLabel("Debounce (s)", Overlay = true)]
    public float debounceTime = 0f;
    [HorizontalGroup("Split", 0.3f)]
    [HideLabel]
    [SuffixLabel("db (s)", Overlay = true)]
    public float currentDebounceTime = 0f;

    [HideInInspector]
    bool active = false;

    

    public bool CheckDebounce()
    {

        //  IF ACTIVE RETURN TRUE
        if (active)
        {
            return true;
        }
        //  ACTIVATE DEBOUNCE & RETURN FALSE
        else if (debounceTime <= 0f)
        {

            return false;
        }
        //  ACTIVATE DEBOUNCE & RETURN FALSE
        else
        {
            currentDebounceTime = debounceTime;
            Frame.core.tools.debouncer.ActivateDebounce(this);
            active = true;
            return false;
        };

    }

    //  Update is called once per frame
    //  RETURN TRUE IF COMPLETED
    public bool DebounceUpdate()
    {

        if (currentDebounceTime <= 0f)
        {
            active = false;
        };

        currentDebounceTime -= Time.deltaTime;

        return active;
    }
}


[System.Serializable]
public class Debouncer
{

    [Title("Debounces")]
    public List<Debounce> debounces;
    List<Debounce> debounces_Completed = new List<Debounce>();


    [Title("System")]
    [HideLabel]
    public DeBugger debug;

    public void ActivateDebounce(Debounce debounce)
    {
        debug.Log("Activated Debounce.");
        debounces.Add(debounce);
    }


    // Update is called once per frame
    public void Update()
    {
        UpdateDebounces();

        ClearCompletedDebounces();
    }

    public void UpdateDebounces()
    {
        foreach (Debounce debounce in debounces)
        {
            if (debounce == null)
            {
                debug.Log("Removing NULL Debounce");
                debounces_Completed.Add(debounce);
                continue;
            };

            if (!debounce.DebounceUpdate())
            {
                debug.Log("Debounce Complete.");
                debounces_Completed.Add(debounce);
            };
        };
    }


    public void ClearCompletedDebounces()
    {
        foreach (Debounce debounce in debounces_Completed)
        {
            debounces.Remove(debounce);
        };

        debounces_Completed.Clear();
    }
}
