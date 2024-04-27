using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform CounterTop;
    private KitchenObject _kitchenObject;

    public virtual void Interaction(Player player)
    {
       
    }
    public virtual void InteractAlternate()
    {
      
    }
    public Transform KitchenObjectFollowTransform
    { 
        get { return CounterTop; }
    }
    public KitchenObject KitchenObject
    {
        get { return _kitchenObject; }
        set { _kitchenObject = value; }
    }

    protected bool IfPlayerHasPlate_AddIngredientToIt(Player player , KitchenObject kitchenObject)
    {
        bool playerCarriesPlate = player.KitchenObject.TryGetComponent(out PlateKitchenObjetct playerPlate);
        if (playerCarriesPlate)
        {
            if(TryToAddKichendObjectToPlate(playerPlate, KitchenObject)) 
                return true;
        }
        return false;
    }
    protected bool TryToAddKichendObjectToPlate(PlateKitchenObjetct plate, KitchenObject kitchenObject)
    {
        if (plate.CheckIfCanAddKitchenObjectAndAddIt(kitchenObject.KitchenObjectSO))
        {
            kitchenObject.SelfDestroy();
            return true;
        }
        return false;
    }


    public  abstract class Responder
    {
        
        
        
        // Returns an instance of the Responder being tested. 
        protected abstract Responder responderInstance();
        
        
        
        
        
    }
    
   
    
}
