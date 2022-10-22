using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private bool useRotationControls;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationMin;
    [SerializeField] private float rotationMax;
    private float _rotationY;
    
    float _moveHorizontal = 0f;
    float _moveVertical = 0f;
    [Range(0.0001f,0.3f)]
    [SerializeField] private float autoShootInterval;
    private float _autoShootTimer;
    [SerializeField] GameObject projectileSpawn;
    [SerializeField] GameObject meleeSpawn;
    [SerializeField] Transform attackTransform;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decelaration = 0.1f;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float zMin;
    [SerializeField] private float zMax;

    private void Start()
    {
        _rotationY = -90;
    }

    void Update()
    {
        PlayerMelee();
        PlayerShooting();
        PlayerFlatMovement();
    }

    private void PlayerMelee()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 meleePos = new Vector3(0f, 0f, 1f);
            GameObject melee = Instantiate(meleeSpawn,
                attackTransform.position + meleePos,
                meleeSpawn.transform.rotation,
                attackTransform);
            melee.SetActive(true);
        }
    }

    private void PlayerShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }

        if (autoShootInterval <= 0)
        {
            return;
        }

        _autoShootTimer += Time.deltaTime;
        
        if (_autoShootTimer >= autoShootInterval)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _autoShootTimer = 0f;
                SpawnProjectile();
            }
        }

    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectileSpawn,
            attackTransform.position,
            transform.rotation);
        projectile.SetActive(true);
    }
    
    void PlayerFlatMovement()
    {
        PlayerHorizontalMovement();
        PlayerVerticalMovement();
        Transform t = transform;

        if (maxSpeed != 0)
        {
            if (_moveHorizontal > maxSpeed)
                _moveHorizontal = maxSpeed;
            else if (_moveHorizontal < -maxSpeed)
                _moveHorizontal = -maxSpeed;
            if (_moveVertical > maxSpeed)
                _moveVertical = maxSpeed;
            else if (_moveVertical < -maxSpeed)
                _moveVertical = -maxSpeed;
        }
        
        Vector3 velocity = new Vector3(_moveHorizontal, 0f, _moveVertical) * Time.deltaTime;
        
        // rotation relative movement
        if (useRotationControls)
        {
            _rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
            _rotationY = Mathf.Clamp(_rotationY, rotationMin, rotationMax);
            transform.rotation = Quaternion.Euler(0, _rotationY, 0);
            
            Vector3 newPosition = t.position + velocity.x * transform.forward * -1f;
            if (velocity.z != 0)
            {
                newPosition = newPosition + velocity.z * transform.right;
            }
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);
            // print(newPosition);
            t.position = newPosition;
            return;
        }

        // rotation absolute movement
        Vector3 newPositionLocal = t.position + velocity;

        newPositionLocal.x = Mathf.Clamp(newPositionLocal.x, xMin, xMax);
        newPositionLocal.z = Mathf.Clamp(newPositionLocal.z, zMin, zMax);
        
        t.localPosition = newPositionLocal;
    }

    private void PlayerVerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveVertical = 0f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveVertical = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveVertical += acceleration;
        }


        else if (Input.GetKey(KeyCode.A))
        {
            _moveVertical -= acceleration;
        }

        else
        {
            VerticalDecel();
        }
    }

    private void PlayerHorizontalMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moveHorizontal = 0f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveHorizontal = 0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveHorizontal += acceleration;
        }

        else if (Input.GetKey(KeyCode.W))
        {
            _moveHorizontal -= acceleration;
        }

        else
        {
            HorizontalDecel();
        }
    }

    private void VerticalDecel()
    {
        if (_moveVertical > 0)
        {
            _moveVertical -= decelaration;
            if (_moveVertical < 0)
            {
                _moveVertical = 0;
            }
        }

        if (-_moveVertical > 0)
        {
            _moveVertical += decelaration;
            if (-_moveVertical < 0)
            {
                _moveVertical = 0;
            }
        }
    }

    private void HorizontalDecel()
    {
        if (_moveHorizontal > 0)
        {
            _moveHorizontal -= decelaration;
            if (_moveHorizontal < 0)
            {
                _moveHorizontal = 0;
            }
        }

        if (-_moveHorizontal > 0)
        {
            _moveHorizontal += decelaration;
            if (-_moveHorizontal < 0)
            {
                _moveHorizontal = 0;
            }
        }
    }
}