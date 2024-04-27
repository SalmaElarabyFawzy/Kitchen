using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHavePrograss
{
   [SerializeField] private List<CuttingRecipeSO> cuttingRecipeSoList;
   public event EventHandler<IHavePrograss.OnProgressChangedArgs> OnProgressChanged;
   public event EventHandler OnCutting;
   private int _cuttingPrograss;
   private CuttingRecipeSO _cuttingRecipeSo;
   public override void Interaction(Player player) 
   {
      if (KitchenObject is null)
      {
         if (player.KitchenObject is not null)
         {
            _cuttingRecipeSo = GetCuttingRecipe(player.KitchenObject.KitchenObjectSO);
            if(_cuttingRecipeSo is null )
               return;
            player.KitchenObject.KitchenObjectParent = this;
            _cuttingPrograss = 0;
            CuttingProgressChange(_cuttingPrograss);
            _cuttingPrograss++;
         }
      }
      else
      {
         if (player.KitchenObject is null)
            KitchenObject.KitchenObjectParent = player;
         else
            IfPlayerHasPlate_AddIngredientToIt(player,KitchenObject);
      }
   }
   public override void InteractAlternate()
   {
      _cuttingRecipeSo = GetCuttingRecipe(KitchenObject.KitchenObjectSO);
     
      if (KitchenObject is not null)
      {
         
         if(_cuttingRecipeSo is null) 
            return;
      
         if (_cuttingPrograss < _cuttingRecipeSo.CuttingTime)
         {
            CuttingProgressChange(_cuttingPrograss);
            CuttingAnimation();
            _cuttingPrograss++;
         }
         else
         {
            CuttingProgressChange(_cuttingPrograss);
            CuttingAnimation();
            Transform kitchenObjectslices = Instantiate(_cuttingRecipeSo.Output.prefab).transform;
            KitchenObject.SelfDestroy();
            kitchenObjectslices.GetComponent<KitchenObject>().KitchenObjectParent = this;
         }
    
      }
   }
   
   private CuttingRecipeSO GetCuttingRecipe(KitchenObjectSO input)
   {
      foreach (var cuttingRecipeSO in cuttingRecipeSoList)
      {
         if (cuttingRecipeSO.Input == input)
            return cuttingRecipeSO;
      }
      return null;
   }
   private void CuttingProgressChange(int prograss) 
   {
      // Fire UI Event
      float cuttingPrograssNormalized =(float) prograss / _cuttingRecipeSo.CuttingTime;
      OnProgressChanged?.Invoke(this, new IHavePrograss.OnProgressChangedArgs(cuttingPrograssNormalized));
   }
   private void CuttingAnimation()
   {
      OnCutting?.Invoke(this , EventArgs.Empty);
   }
}
