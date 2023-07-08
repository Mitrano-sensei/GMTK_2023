using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject SuperBoi { get => _superBoi; }
    public GameObject SuperGirl { get => _superGirl; }

    [SerializeField] private GameObject _superBoi;
    [SerializeField] private GameObject _superGirl;

    private GameManager _gameManager;
    private RecorderManager _recorderManager;

    private void Awake()
    {
        _instance = FindObjectOfType<PlayerManager>();
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _recorderManager = RecorderManager.Instance;

        _gameManager.resetAllButtonPressed += (Vector3 p1, Vector3 p2) => ResetAllPlayersAndRecords(p1, p2);
        _gameManager.resetButtonPressed += (Vector3 p1, Vector3 p2) => ResetPlayers(p1, p2);
        _gameManager.resetButtonPressed += (Vector3 p1, Vector3 p2) => ResetRecord();
    }

    private void ResetRecord()
    {
        if (_gameManager.CurrentGameState == GameManager.GameState.SuperCapsuleBoyTurn)
        {
            _recorderManager.ResetSuperBoi();
            _recorderManager.GetRecord(TarodevController.PlayerController.PlayerCharacter.SuperRectangleGirl).ResetFrame();
        }
    }

    private void ResetAllPlayersAndRecords(Vector3 p1, Vector3 p2)
    {
        ResetPosition(p1, p2);

        _recorderManager.ResetAll();
    }

    private void ResetPlayers(Vector3 p1, Vector3 p2)
    {
        if (_gameManager.CurrentGameState == GameManager.GameState.SuperRectangleGirlTurn)
        {
            ResetAllPlayersAndRecords(p1, p2);
            return;
        }

        ResetPosition(p1, p2);
        _recorderManager.ResetSuperBoi();

    }
    
    public void ResetPosition(Vector3 p1, Vector3 p2)
    {
        _superBoi.transform.position = p1;
        _superGirl.transform.position = p2;
    }
}
