using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private const string Cut = "Cut";
    private Animator _animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        cuttingCounter.OnCutting += Cutting_Animation;
        _animator = GetComponent<Animator>();
    }

    private void Cutting_Animation(object sender, EventArgs e)
    {
        _animator.SetTrigger(Cut);
    }
}
