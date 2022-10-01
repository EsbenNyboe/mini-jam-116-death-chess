using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPawn : MonoBehaviour
{
    [SerializeField] private int xAmount;
    // [SerializeField] private int zAmount;
    [SerializeField] private float moveSpeed;
    // private bool _alternateDirection;

    private float _timer;
    [SerializeField] private float moveWaitTime;

    private bool _isMoving;
    private Vector3 _startMovingPosition;

    [SerializeField] private Rigidbody enemyRigidbody;

    private bool _isRagDoll;
    [SerializeField] private float despawnTimer;

    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpGravity;
    private float _jumpSpeed;

    private void Start()
    {
        EnablePhysics(false);
        _timer = moveWaitTime * 0.99f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetKilled();
        }

        if (_isRagDoll == false)
        {
            MoveBehaviour();
        }
    }
    
    private void MoveBehaviour()
    {
        if (_isMoving)
        {
            Move();
        }
        else
        {
            _timer += Time.deltaTime;
            if (_timer > moveWaitTime)
            {
                _timer = 0;
                _startMovingPosition = transform.position;
                _isMoving = true;
                _jumpSpeed = jumpPower;
            }
        }
    }

    private void Move()
    {
        if (transform.rotation.x < -0.001f || transform.rotation.x > 0.001f )
        {
            return;
        }
        if (transform.position.x < _startMovingPosition.x + xAmount)
        {
            // transform.Translate(new Vector3(moveSpeed, 0, 0));
            if (transform.position.y < _startMovingPosition.y)
            {
                _jumpSpeed = 0;
            }
            else
            {
                _jumpSpeed -= jumpGravity;
            }
            transform.position += new Vector3(moveSpeed, _jumpSpeed, 0);
        }
        else
        {
            _isMoving = false;
        }
    }

    private void GetKilled()
    {
        EnablePhysics(true);
        Destroy(gameObject, despawnTimer);
    }
    
    private void EnablePhysics(bool enable)
    {
        enemyRigidbody.detectCollisions = enable;
        enemyRigidbody.useGravity = enable;
        _isRagDoll = enable;
    }
}
