using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform clearCounterTop;
  [SerializeField] private ClearCounter secondCounter;
  [SerializeField] private bool testing;
  private KitchenObject _kitchenObject;

  
  private void Update()
  {
      if (testing && Input.GetKeyDown(KeyCode.T))
      {
          if (_kitchenObject is not null)
          {
              Debug.Log("Kitchen object transfered !!");
              _kitchenObject.ClearCounter = secondCounter;
              
          }
      }
  }

  public void Interaction()
  {
      if (_kitchenObject is null)
      {
          Transform kitchenObject = Instantiate(kitchenObjectSO.prefab, clearCounterTop).transform;
          kitchenObject.localPosition = Vector3.zero;
          _kitchenObject = kitchenObject.GetComponent<KitchenObject>();
          _kitchenObject.ClearCounter = this;
      }
      else
      {
          Debug.Log("Can't Instantiate more kitchen objects !!");
      }
  }

  public Transform ClearCounterTop
  {
      get { return clearCounterTop; }
  }

  // public void ClearKitchenObject()
  // {
  //     _kitchenObject = null;
  //     Debug.Log("Previous kitchen object cleared !!");
  // }

  public KitchenObject KitchenObject
  {
      get { return _kitchenObject; }
      set { _kitchenObject = value; }
  }
  
}
