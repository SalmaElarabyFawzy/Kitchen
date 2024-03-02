using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteraction;
    private PlayerInputAction playerInputAction;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += InteractPerformed;
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
