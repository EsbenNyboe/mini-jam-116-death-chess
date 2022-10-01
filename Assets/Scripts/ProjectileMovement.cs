using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

[SerializeField] GameObject projectile;
[SerializeField] float projectileSpeed = 20f;
[SerializeField] float projectileHeight = 1f;
[SerializeField] float timeToDestroy = 3f;


    void Start() 
    {
        Destroy(projectile, timeToDestroy);
    }

    
    void Update()
    {
        ProjectileTrajectory();

    }

    void OnCollisionEnter(Collision other) 
        {
            switch (other.gameObject.tag)
            {
                case "Enemy":
                Debug.Log("Enemy HIT!");
                break;

                default:
                Debug.Log("Not HIT!");
                break;
            }    
        }

    private void ProjectileTrajectory()
    {
        Vector3 projectilePath = new Vector3(0f, projectileHeight, projectileSpeed);
        transform.localPosition += projectilePath * Time.deltaTime;
    }
}
