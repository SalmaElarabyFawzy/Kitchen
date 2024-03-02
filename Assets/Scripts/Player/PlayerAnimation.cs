using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string Is_Walking = "IsWalking";
    private Animator _animator;
    
    [SerializeField] private Player player;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetBool(Is_Walking , player.IsWalking);
    }
}
