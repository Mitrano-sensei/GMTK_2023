using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private static GameInput instance;
    private PlayerInputActions playerInputActions;

    public static GameInput Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameInput>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public void Update()
    {
        GetMovementVectorNormalized();
    }

    internal Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    internal bool GetJumpDown()
    {
        return playerInputActions.Player.Jump.triggered;
    }

    internal bool GetJumpUp()
    {
        return playerInputActions.Player.Jump.WasReleasedThisFrame();


    }

    internal bool GetResetButtonPressed()
    {
        return playerInputActions.Player.Reset.triggered;
    }

    internal bool GetResetAllButtonPressed()
    {
        return playerInputActions.Player.ResetAll.triggered;
    }

    internal bool GetChangeCharacterButtonPressed()
    {
        return playerInputActions.Player.ChangeCharacter.triggered;
    }
}
