using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapChanger : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameManager gameManager;

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.UI:
                playerInput.SwitchCurrentActionMap("UI");
                break;
            case GameManager.GameState.Gameplay:
                playerInput.SwitchCurrentActionMap("Gameplay");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnEnable()
    {
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
}