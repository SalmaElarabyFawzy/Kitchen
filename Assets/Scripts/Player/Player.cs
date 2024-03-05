using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnChangeSelectedCounterEventArgs : EventArgs
{
    public BaseCounter selectedCounter { get; private set; }

    public OnChangeSelectedCounterEventArgs(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
    }
}

public class Player : MonoBehaviour,IKitchenObjectParent
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
    [SerializeField] 
    private Transform holdPoint;
    
    private bool _isWalking;
    private Vector3 _lastDirection;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

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
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
        _selectedCounter = null;
    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if(_selectedCounter != null)
            _selectedCounter.InteractAlternate();
    }

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
      if(_selectedCounter != null)
          _selectedCounter.Interaction(this);
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearcounter))
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
        // (0,0,1)
        // check if can move in moveDirection
        bool canMove = !HandelCollosion(moveDirection);
        if (!canMove)
        {
            // check if can move in x axis
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            canMove = moveDirection.x !=0 && !HandelCollosion(moveDirX);
            if (canMove)
                moveDirection = moveDirX; // Can Move in x axis only
            else
            {
                // check if can move in z axis
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
                canMove = moveDirection.z !=0 && !HandelCollosion(moveDirZ);
                if (canMove)
                    moveDirection = moveDirZ;   // Can Move in z axis only
                else 
                {
                    // Can't move
                }
            }
        }
     
        if(canMove) 
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
        // check if walking
        _isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    private bool HandelCollosion(Vector3 dir)
    {
        float maxdistanceOfSweep = moveSpeed * Time.deltaTime;
        return Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerHight),
            playerRadius, dir, maxdistanceOfSweep);
    }
    private void ChangeSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        OnChangeSelectedCounter?.Invoke(this , new OnChangeSelectedCounterEventArgs(selectedCounter));
    }
    public bool IsWalking
    { 
        get { return _isWalking; }
    }
    public Transform KitchenObjectFollowTransform
    {
        get { return holdPoint; }
    }
    public KitchenObject KitchenObject
    {
        get { return _kitchenObject; }
        set { _kitchenObject = value; }
    }
}

