using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateKitchenObjectUI : MonoBehaviour
{
    [Serializable]
    public struct GameObject_Icon
    {
        public GameObject IconGameObject;
        public Image IconImage;
    }
    [SerializeField] private PlateKitchenObjetct plateKitchenObject;
    [SerializeField] private List<GameObject_Icon> plateIcons;
    // Start is called before the first frame update
    private void Start()
    {
        DisableAllIcons();
        plateKitchenObject.OnPlateUIChange += ChanePlateIcons;
    }

    private void ChanePlateIcons(object sender, EventArgs e)
    {
       DisableAllIcons();
      ShowCurrentIngredientsIcons();
    }

    private void DisableAllIcons()
    {
        foreach (GameObject_Icon icon in plateIcons)
            icon.IconGameObject.SetActive(false);
    }
    private void ShowCurrentIngredientsIcons()
    {
        List<KitchenObjectSO> ingredientsList = plateKitchenObject.GetCurrentKitchenObjectOnPlateList;
        for (int index = 0; index < ingredientsList.Count; index++)
        {
            GameObject_Icon icon = plateIcons[index];
            KitchenObjectSO ingredient = ingredientsList[index];
            icon.IconGameObject.SetActive(true);
            icon.IconImage.sprite = ingredient.sprite;

        }
    }
}
