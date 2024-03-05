using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interaction(Player player)
  {
      if (KitchenObject is null)
      {
          if (player.KitchenObject is not null) 
              player.KitchenObject.KitchenObjectParent = this;
      }
      else
      {
          if (player.KitchenObject is null)
              KitchenObject.KitchenObjectParent = player;
      }
  }
}
