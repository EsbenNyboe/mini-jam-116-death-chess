using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControls : MonoBehaviour
{    
    
    float _moveSpeedA = 0f;
    float _moveSpeedD = 0f;
    float _moveSpeedW = 0f;
    float _moveSpeedS = 0f;

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
        // move left
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveSpeedA = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 velocityLeftX = new Vector3(-_moveSpeedA, 0f, 0f);
            transform.localPosition += velocityLeftX * Time.deltaTime;

            _moveSpeedA += acceleration;
        }

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

        //move right
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveSpeedD = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 velocityRightX = new Vector3(_moveSpeedD, 0f, 0f);
            transform.localPosition += velocityRightX * Time.deltaTime;

            _moveSpeedD += acceleration;
        }

        // move forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moveSpeedW = 0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 velocityForwardZ = new Vector3(0f, 0f, _moveSpeedW);
            transform.localPosition += velocityForwardZ * Time.deltaTime;

            _moveSpeedW += acceleration;
        }

        // move backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            _moveSpeedS = 0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 velocityBackZ = new Vector3(0f, 0f, -_moveSpeedS);
            transform.localPosition += velocityBackZ * Time.deltaTime;

            _moveSpeedS += acceleration;
        }

        
         
    }
}
