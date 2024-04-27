using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
   public KitchenObjectSO Input;
   public KitchenObjectSO Output;
   public KitchenObjectSO Burned;
   public int FryingTime;
   public int BurringTime;
}
