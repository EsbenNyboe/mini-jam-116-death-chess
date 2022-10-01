using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControls : MonoBehaviour
{    
    
    float _moveHorizontal = 0f;
    float _moveVertical = 0f;
    [SerializeField] GameObject projectileSpawn;
    [SerializeField] GameObject meleeSpawn;
    [SerializeField] Transform playerTransform;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decelaration = 0.1f;

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
            playerTransform.position + meleePos,
            meleeSpawn.transform.rotation,
            playerTransform);
            melee.SetActive(true);
        }
    }

    private void PlayerShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectileSpawn,
            playerTransform.position,
            playerTransform.rotation);
            projectile.SetActive(true);
        }
    }

    void PlayerFlatMovement()
    {
        PlayerHorizontalMovement();
        PlayerVerticalMovement();
        Vector3 velocity = new Vector3(_moveHorizontal, 0f, _moveVertical);
        transform.localPosition += velocity * Time.deltaTime;
    }

    private void PlayerVerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moveVertical = 0f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveVertical = 0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _moveVertical += acceleration;
        }


        else if (Input.GetKey(KeyCode.S))
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveHorizontal = 0f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveHorizontal = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveHorizontal += acceleration;
        }

        else if (Input.GetKey(KeyCode.A))
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
