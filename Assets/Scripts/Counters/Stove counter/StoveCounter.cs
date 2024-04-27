using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//State Machine
public enum States
{
    Idle,
    Frying,
    Fried,
    Burned
};
public class StoveCounter : BaseCounter,IHavePrograss
{
    public event EventHandler<States> StoveVisual;
    public  event EventHandler<IHavePrograss.OnProgressChangedArgs> OnProgressChanged;
    [SerializeField] private List<FryingRecipeSO> fryingRecipeSoList;

    private FryingRecipeSO _fryingRecipeSO;
    private States _state;
    private float _fryingTime = 0f;
    private float _burnningTime = 0f;
    
    private void Update()
    {
        //comment
        switch (_state)
        {
            case States.Idle :
                break;
            case States.Frying :
                _fryingTime += Time.deltaTime;
                Frying_Burned_ProgressChange(_fryingTime);
                if (_fryingTime >= _fryingRecipeSO.FryingTime)
                {
                    KitchenObject.SelfDestroy();
                    Transform friedObject = Instantiate(_fryingRecipeSO.Output.prefab).transform;
                    friedObject.GetComponent<KitchenObject>().KitchenObjectParent = this;
                    _fryingTime = 0f;
                    _state = States.Fried;
                    StoveVisual?.Invoke(this , _state);
                }
                break;
            case States.Fried :
                _burnningTime += Time.deltaTime;
                Frying_Burned_ProgressChange(_burnningTime);
                if (_burnningTime >= _fryingRecipeSO.BurringTime)
                {
                    KitchenObject.SelfDestroy();
                    Transform burnedObject = Instantiate(_fryingRecipeSO.Burned.prefab).transform;
                    burnedObject.GetComponent<KitchenObject>().KitchenObjectParent = this;
                    _burnningTime = 0;
                    _state = States.Burned;
                    StoveVisual?.Invoke(this , _state);
                }
                break;
            case States.Burned :
                break;
        }
        
    }
    public override void Interaction(Player player) 
    {
        if (KitchenObject is null)
        {
            if (player.KitchenObject != null)
            {
                _fryingRecipeSO = GetfryingRecipe(player.KitchenObject.KitchenObjectSO);
                if(_fryingRecipeSO is null )
                    return;
                player.KitchenObject.KitchenObjectParent = this;
                _state = States.Frying;
                StoveVisual?.Invoke(this , _state);
                _fryingTime = 0;
                _burnningTime = 0;
                Frying_Burned_ProgressChange(_fryingTime);
            }
        }
        else
        {
            if (player.KitchenObject is null)
            {
                KitchenObject.KitchenObjectParent = player;
                _state = States.Idle;
                StoveVisual?.Invoke(this , _state);
                Frying_Burned_ProgressChange(0f);
            }
            else
            {
                if (!IfPlayerHasPlate_AddIngredientToIt(player ,KitchenObject))
                    return;
                _state = States.Idle;
                StoveVisual?.Invoke(this , _state);
                Frying_Burned_ProgressChange(0f);
            }
        }
    }
    private FryingRecipeSO GetfryingRecipe(KitchenObjectSO input)
    {
        foreach (var fryingRecipeSO in fryingRecipeSoList)
        {
            if (fryingRecipeSO.Input == input)
                return fryingRecipeSO;
        }
        return null;
    }
    private void Frying_Burned_ProgressChange(float prograss) 
    {
        // Fire frying or burned progress event
        float fryingTimeNormalized = prograss / _fryingRecipeSO.FryingTime;
        OnProgressChanged?.Invoke(this ,new IHavePrograss.OnProgressChangedArgs(fryingTimeNormalized));
    
    }
    
}
