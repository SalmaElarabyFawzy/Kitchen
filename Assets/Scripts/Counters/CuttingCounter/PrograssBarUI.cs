using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrograssBarUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private GameObject counter;
    private IHavePrograss _baseCounter;
    // Start is called before the first frame update
    private void Start()
    {
        _baseCounter = counter.GetComponent<IHavePrograss>();
        if(_baseCounter is null)
        {
            Debug.LogError("This object doesn't have IHaveCounter interface");
            return;
        }
        _baseCounter.OnProgressChanged += Change_Bar_FillAmount;
        bar.fillAmount = 0;
        Hide();
    }

    private void Change_Bar_FillAmount(object sender, IHavePrograss.OnProgressChangedArgs e)
    {
        bar.fillAmount = e.Progress;
        if (bar.fillAmount >= 1 || bar.fillAmount == 0) 
            Hide();
        else 
            Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
