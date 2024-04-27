using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public delegate KitchenObject PlateCounterEventHandler();
    public event PlateCounterEventHandler OnPlateRemove;
    
    public override void Interaction(Player player) 
    {
        if (player.KitchenObject == null)
        {
            KitchenObject plate = OnPlateRemove?.Invoke();
            plate.KitchenObjectParent = player;
        }

    }
}
