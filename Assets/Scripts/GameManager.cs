using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private GameState _currentGameState;
    
    public delegate void ResetButtonPressed(Vector3 superBoyPosition, Vector3 superGirlPosition);
    public delegate void ResetAllButtonPressed(Vector3 superBoyPosition, Vector3 superGirlPosition);

    public event ResetButtonPressed resetButtonPressed;
    public event ResetAllButtonPressed resetAllButtonPressed;

    // private Level _currentLevel;


    public GameState CurrentGameState { get => _currentGameState; set => _currentGameState = value; }

    private void Awake()
    {
        _instance = FindObjectOfType<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.SuperRectangleGirlTurn;
        resetAllButtonPressed += (Vector3 p1, Vector3 p2) => updateGameState(GameState.SuperRectangleGirlTurn);
        // _currentLevel = LevelManager.Instance.CurrentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        var gameInput = GameInput.Instance;
        var pos1 = new Vector3(-8.77f, 2.45f, 0f);
        var pos2 = new Vector3(-8.68f, -2.66f, 0f);

        if (gameInput.GetResetButtonPressed())
        {
            //resetButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
            resetButtonPressed?.Invoke(pos1, pos2);
        }   
        else if (gameInput.GetResetAllButtonPressed())
        {   
            //resetAllButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
            resetAllButtonPressed?.Invoke(pos1, pos2);

            updateGameState(GameState.SuperRectangleGirlTurn);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperRectangleGirlTurn)
        {   
            updateGameState(GameState.SuperCapsuleBoyTurn);
            PlayerManager.Instance.ResetPosition(pos1, pos2);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperCapsuleBoyTurn)
        {
            //resetAllButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
        }


    }

    public void Win()
    {
        updateGameState(GameState.Win);
    }

    public void Lose()
    {
        updateGameState(GameState.Lose);
    }


    private void updateGameState(GameState newGameState)
    {
        var oldState = CurrentGameState;
        CurrentGameState = newGameState;

        switch (newGameState)
        {
            case GameState.SuperRectangleGirlTurn:
                break;
            case GameState.SuperCapsuleBoyTurn:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }
    }

    public enum GameState
    {
        SuperRectangleGirlTurn,
        SuperCapsuleBoyTurn,
        Win,
        Lose
    }
}
