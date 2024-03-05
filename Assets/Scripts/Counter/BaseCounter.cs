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
}
