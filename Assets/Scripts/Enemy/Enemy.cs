using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Pathfinding
    [Range(0, 1)] [SerializeField] private float initialMoveWaitTimeFactor;
    [SerializeField] private float moveWaitTime;
    private float _timer;
    
    [SerializeField] private Vector2Int gridMovePattern;
    [SerializeField] private bool useDirectionToggle;
    [SerializeField] private bool chasePlayer;
    [SerializeField] private float timeToClearOccupation;
    private Vector2Int _previousTargetGridCell = new Vector2Int(-1, -1);
    private Vector2Int _currentTargetGridCell = new Vector2Int(-1, -1);

    // Movement
    [Range(0.001f, 10f)] [SerializeField] private float moveSpeed;
    private bool _isMoving;
    private float _moveProgress;
    private Vector3 _startMovingPosition;
    private Vector3 _currentTargetPosition;

    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpGravity;
    private bool _isJumping;
    private float _jumpSpeed;

    // Death
    [SerializeField] private float despawnTimer;
    [SerializeField] private Animator animator;
    
    [SerializeField] private Rigidbody enemyRigidbody;
    private bool _isRagDoll;

    private void Start()
    {
        if (animator) 
            animator.SetBool("isAlive", true);

        EnablePhysics(false);
        
        SelectFirstPath();
    }

    void Update()
    {
        if (_isRagDoll == false)
        {
            if (_isMoving)
            {
                Move();
            }
            else
            {
                SelectPath();
            }
        }
    }

    #region Pathfinding
    private void SelectFirstPath()
    {
        _timer = moveWaitTime * initialMoveWaitTimeFactor;
        var position = transform.position;
        _currentTargetGridCell = GameGridScript.Instance.GetGridPosFromWorld(position);
        _previousTargetGridCell = _currentTargetGridCell;
        SetOccupation(true, _currentTargetGridCell);
    }
    
    private void SelectPath()
    {
        _timer += Time.deltaTime;
        if (_timer > moveWaitTime)
        {
            _timer = 0;

            if (useDirectionToggle)
            {
                gridMovePattern.y = -gridMovePattern.y;
            }

            int xMoveAmount = gridMovePattern.x;
            int yMoveAmount = gridMovePattern.y;

            if (chasePlayer)
            {
                Vector2Int playerGridPosition = PlayerPositionTracker.Instance.playerGridPosition;
                Vector2Int distanceToPlayer = playerGridPosition - _previousTargetGridCell;

                bool useYAxis = Mathf.Abs(distanceToPlayer.y) > Mathf.Abs(distanceToPlayer.x);

                if (useYAxis == false)
                {
                    if (distanceToPlayer.x < 0)
                    {
                        xMoveAmount = -gridMovePattern.x;
                    }
                }
                else
                {
                    if (distanceToPlayer.y > 0)
                    {
                        yMoveAmount = gridMovePattern.x;
                        xMoveAmount = -gridMovePattern.y;
                    }
                    else
                    {
                        xMoveAmount = gridMovePattern.y;
                        yMoveAmount = -gridMovePattern.x;
                    }
                }
            }

            Vector2Int currentGridCell = GameGridScript.Instance.GetGridPosFromWorld(transform.position);
            int xTarget = currentGridCell.x + xMoveAmount;
            int yTarget = currentGridCell.y + yMoveAmount;

            Vector2Int targetCell = new Vector2Int(xTarget, yTarget);
            if (targetCell.x >= GameGridScript.Instance.width)
            {
                if (currentGridCell.x >= GameGridScript.Instance.width - 1)
                {
                    GetKilled();
                    UIManager.Instance.TakeDamage(1);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/EnemySuicide", transform.position);
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
            _isJumping = true;
            _jumpSpeed = jumpPower;
            _startMovingPosition = transform.position;
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

    #endregion
    #region Movement
    private void Move()
    {
        if (transform.rotation.x < -0.001f || transform.rotation.x > 0.001f)
        {
            return;
        }

        float newPositionY = _startMovingPosition.y;
        if (_isJumping)
        {
            _jumpSpeed -= jumpGravity * Time.deltaTime;
            newPositionY = transform.position.y + _jumpSpeed;
            if (newPositionY < _startMovingPosition.y)
            {
                newPositionY = _startMovingPosition.y;
                _isJumping = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyLand", transform.position);
            }
        }

        _moveProgress += moveSpeed * Time.deltaTime;

        Vector3 interpolatedPosition = Vector3.Lerp(_startMovingPosition, _currentTargetPosition, _moveProgress);
        
        if (_moveProgress > 1)
        {
            _moveProgress = 0;
            _isMoving = false;
            interpolatedPosition.x = _currentTargetPosition.x;
            interpolatedPosition.z = _currentTargetPosition.z;
        }

        interpolatedPosition.y = newPositionY;

        transform.position = interpolatedPosition;
    }
    #endregion
    #region Death

    public void GetHurt()
    {
        animator.SetTrigger("getHurt");
    }

    public void GetKilled()
    {
        if (animator) 
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
    #endregion
}