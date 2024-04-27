using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjectVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }
    [SerializeField] private PlateKitchenObjetct plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnPlateVisualChange += Activate_KitchenObject_GameObject;
    }
    private void Activate_KitchenObject_GameObject(object sender, PlateKitchenObjetct.OnKitchenObjectAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenGameObject in KitchenObjectSOGameObjectList)
        {
            if(kitchenGameObject.KitchenObjectSO == e.kitchenObjectSO)
                kitchenGameObject.GameObject.SetActive(true);
        }
    }
    
}
