using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
          else
              Check_WhoCarriesPlate_WhoHasIngredient(player);
      }
  }
  private void Check_WhoCarriesPlate_WhoHasIngredient(Player player)
  {
      bool thereIsAPlateOnCounter = KitchenObject.TryGetComponent(out PlateKitchenObjetct counterPlate);
      if (!thereIsAPlateOnCounter && IfPlayerHasPlate_AddIngredientToIt(player, KitchenObject)) 
          return;
      if (thereIsAPlateOnCounter && !IfPlayerHasPlate_AddIngredientToIt(player, KitchenObject))
          TryToAddKichendObjectToPlate(counterPlate , player.KitchenObject);
  }
 
}
