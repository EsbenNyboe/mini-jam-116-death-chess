using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
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

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float zMin;
    [SerializeField] private float zMax;

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
            Random.rotation);
        projectile.SetActive(true);
    }

    void PlayerFlatMovement()
    {
        PlayerHorizontalMovement();
        PlayerVerticalMovement();
        Transform t = transform;

        Vector3 velocity = new Vector3(_moveHorizontal, 0f, _moveVertical);
        Vector3 newPositionLocal = t.position + velocity * Time.deltaTime;

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