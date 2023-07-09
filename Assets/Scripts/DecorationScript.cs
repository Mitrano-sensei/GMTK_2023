using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationScript : MonoBehaviour
{
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null) _collider = gameObject.AddComponent<BoxCollider2D>();

        _collider.isTrigger = true;

        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        var rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidBody2D.isKinematic = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Bump());
        }
    }

    private IEnumerator Bump()
    {
        var maxAngle = 5;
        var angleSpeed = 2f;
        var randDir = UnityEngine.Random.Range(0, 2);
        if (randDir == 0) randDir = -1; else randDir = 1;

        EnableCollider(false);

        for (var angle = 0; angle <= maxAngle; angle+=1)
        {
            transform.Rotate(0, 0, randDir * angleSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        for (var angle = maxAngle; angle >= -maxAngle; angle -= 1)
        {
            transform.Rotate(0, 0, -1 * randDir * angleSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        for (var angle = -maxAngle; angle <= 0; angle += 1)
        {
            transform.Rotate(0, 0, randDir * angleSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        transform.rotation = Quaternion.identity;

        EnableCollider(true);
    }

    private void EnableCollider(bool v)
    {
        _collider.enabled = v;
    }
}
