using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private Collider2D _collider;

    void Start()
    {
        _collider = this.GetComponent<Collider2D>();
        if (_collider == null) 
            _collider = this.AddComponent<BoxCollider2D>();

        _collider.isTrigger = true;

        if (this.GetComponent<Rigidbody2D>() == null) this.AddComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var gameManager = GameManager.Instance;

        var player = other.GetComponent<PlayerController>();
        if (player == null) return;

        var playerCharacter = player.WhoAmI;
        if (playerCharacter == PlayerController.PlayerCharacter.SuperCapsuleBoy && gameManager.CurrentGameState != GameManager.GameState.Win)
        {
            gameManager.Win();
        }

        if (playerCharacter == PlayerController.PlayerCharacter.SuperCapsuleBoy && gameManager.CurrentGameState == GameManager.GameState.Win)
        {
            gameManager.WaitForAction();
        }
    }
}
