using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public delegate void SwitchCharacter();

    public event ResetButtonPressed resetButtonPressed;
    public event SwitchCharacter switchCharacter;
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
        resetAllButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        var gameInput = GameInput.Instance;

        if (gameInput.GetResetButtonPressed())
        {
            resetButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);
        }   
        else if (gameInput.GetResetAllButtonPressed())
        {   
            resetAllButtonPressed?.Invoke(_pos1.localPosition, _pos2.localPosition);

            updateGameState(GameState.SuperRectangleGirlTurn);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperRectangleGirlTurn)
        {   
            updateGameState(GameState.SuperCapsuleBoyTurn);
            switchCharacter?.Invoke();
            PlayerManager.Instance.ResetPosition(_pos1.localPosition, _pos2.localPosition);
        } else if (gameInput.GetChangeCharacterButtonPressed() && CurrentGameState == GameState.SuperCapsuleBoyTurn)
        {
            // Nothing happens

        } else if (gameInput.GetJumpDown() && CurrentGameState == GameState.WaitForAction)
        {
            EndZone.Instance.loadNextLevel();
        }


    }

    public async void Win()
    {
        /*PlayerManager.Instance.FreezePlayers();

        await Task.Delay(1000);

        PlayerManager.Instance.ResetPosition(_pos1.localPosition, _pos2.localPosition);
        PlayerManager.Instance.ResetVelocity();
        PlayerManager.Instance.UnFreezePlayers();

        RecorderManager.Instance.ReplayAll();
        updateGameState(GameState.Win);
        */
        await Task.Delay(1000);
        WaitForAction();
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
    }

    [SerializeField] private Canvas _menuCanvas;
    internal void WaitForAction()
    {
        updateGameState(GameState.WaitForAction);
        _menuCanvas.gameObject.SetActive(true);
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
