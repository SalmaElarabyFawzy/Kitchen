using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
   public override void Interaction(Player player) 
   {
      if (KitchenObject is null)
      {
         if (player.KitchenObject != null && player.KitchenObject.KitchenObjectSlices != null) 
            player.KitchenObject.KitchenObjectParent = this;
      }
      else
      {
         if (player.KitchenObject is null)
            KitchenObject.KitchenObjectParent = player;
      }
   }
   public override void InteractAlternate()
   {
     
      if (KitchenObject is not null)
      {
         Debug.Log("Cutting !! ");
         if(KitchenObject.KitchenObjectSlices is null)
         {
            Debug.Log("Slices Null");
            return;
         }
         Transform kitchenObjectslices = Instantiate(KitchenObject.KitchenObjectSlices.prefab).transform;
         KitchenObject.SelfDestroy();
         kitchenObjectslices.GetComponent<KitchenObject>().KitchenObjectParent = this;
      }
   }
}
