using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{

    [Title("System")]
    public bool active = false;

    

    [BoxGroup("Player Controller")]
    [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
    public PlayerController controller;



    public void Init()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        };


        controller.UpdateController();
    }





    public void Disappear()
    {
        controller.disappearEvent.Activate();
    }


    public void Reappear()
    {
        controller.reappearEvent.Activate();
    }


    public void ActivatePlayer()
    {
        if (active)
        {
            return;
        };

        active = true;
    }


    public void DeactivatePlayer()
    {
        if (!active)
        {
            return;
        };

        active = false;
    }




    //  PLAYER ACTIONS

    //  Stop Input
    //  Disable Rigidbody
    public void FreezePlayer()
    {
        DeactivatePlayer();
        controller.FreezePlayer();
    }

    public void UnfreezePlayer()
    {
        DeactivatePlayer();
        controller.UnfreezePlayer();
    }



}
