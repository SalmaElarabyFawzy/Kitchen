using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interaction(Player player)
    {
        if (player.KitchenObject is not null)
            return;
        Transform kitchenObject = Instantiate(kitchenObjectSO.prefab).transform;
        kitchenObject.GetComponent<KitchenObject>().KitchenObjectParent = player;
        
    }
}
