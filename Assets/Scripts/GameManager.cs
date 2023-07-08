using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;

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

        if (gameInput.GetResetButtonPressed())
        {
            //resetButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
            resetButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);
        }   
        else if (gameInput.GetResetAllButtonPressed())
        {   
            //resetAllButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
            resetAllButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);

            updateGameState(GameState.SuperRectangleGirlTurn);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperRectangleGirlTurn)
        {   
            updateGameState(GameState.SuperCapsuleBoyTurn);
            PlayerManager.Instance.ResetPosition(_pos1.localPosition, _pos2.localPosition);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperCapsuleBoyTurn)
        {
            //resetAllButtonPressed?.Invoke(_currentLevel.superBoyInitialPos, _currentLevel.superGirlInitialPos);
        }


    }

    public async void Win()
    {
        PlayerManager.Instance.FreezePlayers();

        await Task.Delay(1000);

        PlayerManager.Instance.ResetPosition(_pos1.localPosition, _pos2.localPosition);
        PlayerManager.Instance.ResetVelocity();
        PlayerManager.Instance.UnFreezePlayers();

        RecorderManager.Instance.ReplayAll();
        updateGameState(GameState.Win);
    }

    public void Lose()
    {
        resetButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);
        // updateGameState(GameState.Lose);
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

    internal void WaitForAction()
    {
        updateGameState(GameState.WaitForAction);
    }

    public enum GameState
    {
        SuperRectangleGirlTurn,
        SuperCapsuleBoyTurn,
        Win,
        WaitForAction,
        Lose
    }
}
