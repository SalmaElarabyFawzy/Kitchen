using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  private IKitchenObjectParent kitchenObjectParent;
  public IKitchenObjectParent KitchenObjectParent
  {
    get { return KitchenObjectParent; }
    set
    {
      if(kitchenObjectParent is not null)
        kitchenObjectParent.KitchenObject = null;
      
      kitchenObjectParent = value;
      
      if(kitchenObjectParent.KitchenObject is not null)
      {
        Debug.Log("This counter has already kitchen object !!");
      }
  
      kitchenObjectParent.KitchenObject = this;
      transform.parent = kitchenObjectParent.KitchenObjectFollowTransform;
      transform.localPosition = Vector3.zero;
    }
  }
  public void SelfDestroy()
  {
    kitchenObjectParent.KitchenObject = null;
    Destroy(gameObject);
  }
  public KitchenObjectSO KitchenObjectSO
  {
    get { return kitchenObjectSO; }
  }
}
