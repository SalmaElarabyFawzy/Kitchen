using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
   public override void Interaction(Player player)
   {
      if (player.KitchenObject != null)
      {
         player.KitchenObject.SelfDestroy();
      }
   }
}
