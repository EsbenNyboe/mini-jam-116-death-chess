using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPawn : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed;

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

    [Range(0, 1)] [SerializeField] private float initialMoveWaitTimeFactor;

    private Vector2Int _currentTargetGridCell = new Vector2Int(-1, -1);
    private Vector3 _currentTargetPosition;

    [SerializeField] private Vector2Int gridMovePattern;

    [SerializeField] private float timeToClearOccupation;
    private Vector2Int _previousTargetGridCell = new Vector2Int(-1, -1);

    private void Start()
    {
        animator.SetBool("isAlive", true);

        EnablePhysics(false);
        _timer = moveWaitTime * initialMoveWaitTimeFactor;
        _currentTargetGridCell = GameGridScript.Instance.GetGridPosFromWorld(transform.position);
        _previousTargetGridCell = _currentTargetGridCell;
        SetOccupation(true, _currentTargetGridCell);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Debug.Log();
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

                Vector2Int currentGridCell = GameGridScript.Instance.GetGridPosFromWorld(transform.position);
                int xTarget = currentGridCell.x + gridMovePattern.x;
                int yTarget = currentGridCell.y + gridMovePattern.y;

                Vector2Int targetCell = new Vector2Int(xTarget, yTarget);
                if (targetCell.x >= GameGridScript.Instance.width)
                {
                    if (currentGridCell.x >= GameGridScript.Instance.width - 1)
                    {
                        GetKilled();
                        return;
                    }
                    targetCell = new Vector2Int(GameGridScript.Instance.width - 1, yTarget);
                }

                if (targetCell.y < 0 || targetCell.y >= GameGridScript.Instance.width)
                {
                    gridMovePattern.y = -gridMovePattern.y;
                    targetCell.y = currentGridCell.y + gridMovePattern.y;
                }

                GridCellScript cellScript = GameGridScript.Instance.GetGridCellScriptFromGridPos(targetCell);

                if (cellScript.isOccupied)
                {
                    return;
                }

                _currentTargetGridCell = targetCell;
                SetOccupation(true, _currentTargetGridCell);
                StartCoroutine(ClearPreviousOccupation(_previousTargetGridCell));
                _previousTargetGridCell = _currentTargetGridCell;

                _currentTargetPosition = GameGridScript.Instance.GetWorldPosFromGridPos(_currentTargetGridCell);

                _isMoving = true;
                _jumpSpeed = jumpPower;
                _startMovingPosition = transform.position;
            }
        }
    }

    IEnumerator ClearPreviousOccupation(Vector2Int cellVector)
    {
        yield return new WaitForSeconds(timeToClearOccupation);
        SetOccupation(false, cellVector);
        // GridCellScript gridCellScript = GameGridScript.Instance.GetGridCellScriptFromGridPos(gridCellToClear);
        // gridCellScript.objectInThisGridSpace = null;
        // gridCellScript.isOccupied = false;
    }

    private void SetOccupation(bool willOccupy, Vector2Int cellVector) // to do: set false when killed
    {
        GridCellScript gridCellScript = GameGridScript.Instance.GetGridCellScriptFromGridPos(cellVector);
        // gridCellScript.objectInThisGridSpace = willOccupy ? gameObject : null;
        gridCellScript.isOccupied = willOccupy;
    }

    private void Move()
    {
        if (transform.rotation.x < -0.001f || transform.rotation.x > 0.001f)
        {
            return;
        }

        if (transform.position.x < _currentTargetPosition.x)
        {
            // transform.Translate(new Vector3(moveSpeed, 0, 0));
            _jumpSpeed -= jumpGravity;
            Vector3 newPosition = transform.position +
                                  new Vector3(moveSpeed * gridMovePattern.x, _jumpSpeed, moveSpeed * gridMovePattern.y);
            if (newPosition.y < _startMovingPosition.y)
            {
                newPosition.y = _startMovingPosition.y;
            }

            transform.position = newPosition;
        }
        else
        {
            _isMoving = false;
        }
    }


    public void GetHurt()
    {
        animator.SetTrigger("getHurt");
    }

    public void GetKilled()
    {
        animator.SetBool("isAlive", false);
        EnablePhysics(true);
        Destroy(gameObject, timeToClearOccupation + despawnTimer);
        StartCoroutine(ClearPreviousOccupation(_previousTargetGridCell));
    }

    private void EnablePhysics(bool enable)
    {
        enemyRigidbody.detectCollisions = enable;
        enemyRigidbody.useGravity = enable;
        _isRagDoll = enable;
    }
}