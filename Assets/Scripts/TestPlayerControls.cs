using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControls : MonoBehaviour
{    
    
    float _moveHorizontal = 0f;
    float _moveVertical = 0f;

    [SerializeField] GameObject projectileSpawn;
    [SerializeField] Transform playerTransform;
    [SerializeField] float acceleration = 0.1f;

    void Update()
    {
        PlayerFlatMovement();

        
        // shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pew pew!");
            

            GameObject projectile = Instantiate (projectileSpawn, 
            playerTransform.position, 
            playerTransform.rotation);
            projectile.SetActive(true);
        }


    }

    void PlayerFlatMovement()
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
            _moveHorizontal = 0f;
        }

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
            _moveVertical = 0f;
        }

        Vector3 velocity = new Vector3(_moveHorizontal, 0f, _moveVertical);
        transform.localPosition += velocity * Time.deltaTime;


        // FUCKING DECELARATION?!?!
        // if (Input.GetKeyUp(KeyCode.A))
        // {

        // float playerSpeed = (transform.position - this.transform.position).magnitude / Time.time;
        // transform.localPosition = playerSpeed - acceleration;

        // Vector3 distanceTraveled = new Vector3((this.transform.position).magnitude, 0f, 0f);
        // float speed = distanceTraveled / (Time.time - Time.timeSinceLevelLoad);

        // speed = total distance traveled / total time taken
        // total distance traveled = start position - current position

        // }
    }
}
