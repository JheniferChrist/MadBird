using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 2000;
    [SerializeField] float _maxDragDistance = 5;

    Vector2 _startingPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp()
    {
        _spriteRenderer.color = Color.white;
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startingPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);
        return;
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startingPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startingPosition;
            direction.Normalize();
            desiredPosition = _startingPosition + (direction * _maxDragDistance);
        }

        if (desiredPosition.x > _startingPosition.x)
            desiredPosition.x = _startingPosition.x;

        _rigidbody2D.position = desiredPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());  
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startingPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;

    }
}
