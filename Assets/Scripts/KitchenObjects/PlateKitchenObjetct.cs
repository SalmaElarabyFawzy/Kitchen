using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlateKitchenObjetct : KitchenObject
{
    public class OnKitchenObjectAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    public event EventHandler<OnKitchenObjectAddedEventArgs> OnPlateVisualChange;
    public event EventHandler OnPlateUIChange;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectasList;
    private List<KitchenObjectSO> currentKitchenObjectsOnPlate;

    private void Start()
    {
        currentKitchenObjectsOnPlate = new List<KitchenObjectSO>();
    }
    public bool CheckIfCanAddKitchenObjectAndAddIt(KitchenObjectSO kitchenObject)
    {
        if (!validKitchenObjectasList.Contains(kitchenObject))
            return false;
        if (!currentKitchenObjectsOnPlate.Contains(kitchenObject))
        {
              currentKitchenObjectsOnPlate.Add(kitchenObject);
              OnPlateVisualChange?.Invoke(this , new OnKitchenObjectAddedEventArgs
              {
                  kitchenObjectSO = kitchenObject
              });
              OnPlateUIChange?.Invoke(this , EventArgs.Empty);
              return true;
        }
        else
        {
            return false;
            // can't put in the plate
            // don't forget to make mate burned valid
        }
    }

    public List<KitchenObjectSO> GetCurrentKitchenObjectOnPlateList
    {
        get { return currentKitchenObjectsOnPlate; }
    }
}
