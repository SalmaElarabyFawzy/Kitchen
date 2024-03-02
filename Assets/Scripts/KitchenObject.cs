using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSo;
  [SerializeField] private ClearCounter clearCounter;

  public ClearCounter ClearCounter
  {
    get { return clearCounter; }
    set
    {
      ClearCounter _clearCounter = value;
      if(clearCounter is not null)
        clearCounter.KitchenObject = null;
      
      clearCounter = _clearCounter;
      Debug.Log(clearCounter.name);
      if(clearCounter.KitchenObject is not null)
      {
        Debug.Log("This counter has already kitchen object !!");
      }

      clearCounter.KitchenObject = this;
      transform.parent = clearCounter.ClearCounterTop;
      transform.localPosition = Vector3.zero;

    }
    
  }







}
