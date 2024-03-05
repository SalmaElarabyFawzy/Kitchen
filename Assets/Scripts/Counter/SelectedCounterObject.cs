using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterObject : MonoBehaviour
{
 [SerializeField] private GameObject[] selectedCounterVisualArray;
 [SerializeField] private BaseCounter baseCounter;

 private void Start()
 {
  Player.Instance.OnChangeSelectedCounter += CheckCounter;
 }
 private void CheckCounter(object sender, OnChangeSelectedCounterEventArgs e)
 {
     if(e.selectedCounter == baseCounter)
         SeletedVisualEnable();
     else
         SeletedVisualDisable();  
 }
 private void SeletedVisualEnable()
 {
     foreach (var selectedCounterVisual in selectedCounterVisualArray)
     {
         selectedCounterVisual.SetActive(true);
     }

 }
 private void SeletedVisualDisable()
 {
     foreach (var selectedCounterVisual in selectedCounterVisualArray)
     {
         selectedCounterVisual.SetActive(false);
     }

 }
}
