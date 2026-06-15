using System;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Interactable
{
    [SerializeField] SaveSlotMenu menu;

    /// <summary>
    /// Activate save menu
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        // need some way to turn controls back on when back clicked, 
        // hut woul need player reference then 
        //player.PauseInputControls();
        SpawnManager.Instance.PlayerPosition = player.transform.position;

        menu.ActivateMenu();
    }

}
