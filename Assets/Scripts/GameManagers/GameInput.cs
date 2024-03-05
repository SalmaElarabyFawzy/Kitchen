using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteraction;
    public event EventHandler OnInteractAlternate; 
    private PlayerInputAction playerInputAction;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += InteractPerformed;
        playerInputAction.Player.InteractAlternet.performed += InteractAlternate_Performed;
    }

    private void InteractAlternate_Performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this , EventArgs.Empty);
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
       OnInteraction?.Invoke(this , EventArgs.Empty);
    }
    public Vector2 GetInputVectorNormalized()
    {
        var inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        // make inputVector with a magnitude of one
        inputVector.Normalize();
        return inputVector;
    }
}
