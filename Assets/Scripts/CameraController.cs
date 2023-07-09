using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        var gameManager = GameManager.Instance;
        var playerManager = PlayerManager.Instance;
        var player = gameManager.CurrentGameState == GameManager.GameState.SuperRectangleGirlTurn ? playerManager.SuperGirl : playerManager.SuperBoi;

        if (player.transform.position.x > gameObject.transform.position.x + 2)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
        } else if (player.transform.position.x < gameObject.transform.position.x - 2 && player.transform.position.x + 2 > _initialPosition.x)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        gameManager.resetAllButtonPressed   += (Vector3 p1, Vector3 p2) => ResetPosition();
        gameManager.resetButtonPressed      += (Vector3 p1, Vector3 p2) => ResetPosition();
        gameManager.switchCharacter         += () => ResetPosition();
    }

    private void ResetPosition()
    {
        gameObject.transform.position = _initialPosition;
    }


}
