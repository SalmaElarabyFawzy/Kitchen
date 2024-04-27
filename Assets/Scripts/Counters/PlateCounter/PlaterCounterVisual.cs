using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter baseCounter;
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private int platesMaxNumber = 3;
    [SerializeField] private Vector3 platesOffset;
    [SerializeField] private int timeBetweenSpawns = 4;
    private float _timeToSpawn =0f;
    public List<KitchenObject> platesList;
    // Start is called before the first frame update
    private void Start()
    {
        platesList = new List<KitchenObject>();
        baseCounter.OnPlateRemove += PlateCounterVisual_OnPlateRemove;
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnPlate();
    }

    private void SpawnPlate()
    {
        _timeToSpawn += Time.deltaTime;
        if (_timeToSpawn >= timeBetweenSpawns)
        { 
            _timeToSpawn = 0;
            if(platesList.Count < platesMaxNumber)
            {
                Debug.Log("Plate Spawned!");
                Transform plateObject = Instantiate(plateSO.prefab).transform;
                KitchenObject plateKitchenObject = plateObject.GetComponent<KitchenObject>();
                plateKitchenObject.KitchenObjectParent = baseCounter;
                plateObject.localPosition += platesOffset *platesList.Count;
                platesList.Add(plateKitchenObject);
            }
        }
    }
    private KitchenObject PlateCounterVisual_OnPlateRemove()
    {
        KitchenObject plateObject = platesList[platesList.Count - 1];
        platesList.Remove(plateObject);
        return plateObject;
    }
}
