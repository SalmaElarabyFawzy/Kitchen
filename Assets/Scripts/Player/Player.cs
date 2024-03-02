using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnChangeSelectedCounterEventArgs : EventArgs
{
    public ClearCounter selectedCounter { get; private set; }

    public OnChangeSelectedCounterEventArgs(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
    }
}

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }
    
    [SerializeField] 
    private GameInput gameInput;
    [SerializeField] 
    private float moveSpeed =7f;
    [SerializeField] 
    private float rotateSpeed = 10f;
    [SerializeField] 
    private float playerHight = 7;
    [SerializeField] 
    private float playerRadius = .7f;
    [SerializeField]
    
    private LayerMask counterLayerMask;
    private bool _isWalking;
    private Vector3 _lastDirection;
    private ClearCounter _selectedCounter;

    public event EventHandler<OnChangeSelectedCounterEventArgs> OnChangeSelectedCounter;
    
    private void Awake()
    {
        if(Instance != null)
            Debug.Log("There is Multiple player instances !!");
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
        _selectedCounter = null;
    }
    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        // HandelInteraction();
      if(_selectedCounter != null)
          _selectedCounter.Interaction();
    }
    private void Update()
    {
      HandelMovement();
      HandelInteraction();
    }
    private void HandelInteraction()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        if (moveDirection != Vector3.zero)
            _lastDirection = moveDirection;
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position , _lastDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearcounter))
            {
                if(_selectedCounter != clearcounter)
                    ChangeSelectedCounter(clearcounter);
            }
            else
                ChangeSelectedCounter(null);
        }
        else
            ChangeSelectedCounter(null);
    }
    private void HandelMovement()
    {
        Vector2 input = gameInput.GetInputVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        
        // check if can move in moveDirection
        bool canMove = !HandelCollosion(moveDirection);
        if (!canMove)
        {
            // check if can move in x axis
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            canMove = !HandelCollosion(moveDirX);
            if (canMove)
                moveDirection = moveDirX.normalized; // Can Move in x axis only
            else
            {
                // check if can move in z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
                canMove = !HandelCollosion(moveDirZ);
                if (canMove)
                    moveDirection = moveDirZ.normalized;   // Can Move in z axis only
                else 
                    moveDirection = Vector3.zero; // Can't move
            }
        }
        // check if walking
        _isWalking = moveDirection != Vector3.zero;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    private bool HandelCollosion(Vector3 dir)
    {
        float maxdistanceOfSweep = moveSpeed * Time.deltaTime;
        return Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerHight),
            playerRadius, dir, maxdistanceOfSweep);
    }
    private void ChangeSelectedCounter(ClearCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        OnChangeSelectedCounter?.Invoke(this , new OnChangeSelectedCounterEventArgs(selectedCounter));
    }
    public bool IsWalking
    { 
        get { return _isWalking; }
    }
}
