using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_PlayerAction
{

    public enum PlayerActionType
    {
        FreezePlayer,
        ReleasePlayer,
        DisableInput,
        EnableInput,
        Disappear,
        Reappear,
        ResetPlayerPosition,

    }
    public PlayerActionType action;

    [ShowIf("@this.action == PlayerActionType.ResetPlayerPosition")]
    public Transform resetPosition;




    [Title("System")]
    public DeBugger debug;

    private void ActivateSpawnAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }




    public void Activate()
    {
        if (action == PlayerActionType.FreezePlayer)
        {
            Frame.core.player.FreezePlayer();
        }
        else if (action == PlayerActionType.ReleasePlayer)
        {
            Frame.core.player.UnfreezePlayer();
        }
        if (action == PlayerActionType.DisableInput)
        {
            Frame.core.player.DeactivatePlayer();
        }
        else if (action == PlayerActionType.EnableInput)
        {
            Frame.core.player.ActivatePlayer();

        }
        else if (action == PlayerActionType.Disappear)
        {
            Frame.core.player.Disappear();
        }
        else if (action == PlayerActionType.Reappear)
        {
            Frame.core.player.Reappear();
        };
    }


}
