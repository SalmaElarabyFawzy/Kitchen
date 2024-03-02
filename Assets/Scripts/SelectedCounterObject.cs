using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterObject : MonoBehaviour
{
 [SerializeField] private GameObject selectedCounterVisual;
 [SerializeField] private ClearCounter clearCounter;

 private void Start()
 {
  Player.Instance.OnChangeSelectedCounter += CheckCounter;
 }
 private void CheckCounter(object sender, OnChangeSelectedCounterEventArgs e)
 {
     if(e.selectedCounter == clearCounter)
         SeletedVisualEnable();
     else
         SeletedVisualDisable();  
 }
 private void SeletedVisualEnable()
 {
   selectedCounterVisual.SetActive(true);
 }
 private void SeletedVisualDisable()
 {
     selectedCounterVisual.SetActive(false);
 }
}
