using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] 
    private StoveCounter counterBase;
    [SerializeField] 
    private GameObject stoveOnVisual;
    [SerializeField] 
    private GameObject sizzlingParticales;
    
    // Start is called before the first frame update
    private void Start()
    {
        counterBase.StoveVisual += StoveVisual;
    }

    private void StoveVisual(object sender, States state)
    {
       if(state == States.Idle || state == States.Burned)
           StoveVisual_StoveOff();
       else
           StoveVisual_StoveOn();
    }

    private void StoveVisual_StoveOn()
    {
        stoveOnVisual.SetActive(true);
        sizzlingParticales.SetActive(true);
    }

    private void StoveVisual_StoveOff()
    { stoveOnVisual.SetActive(false);
        sizzlingParticales.SetActive(false);
        
    }
}
