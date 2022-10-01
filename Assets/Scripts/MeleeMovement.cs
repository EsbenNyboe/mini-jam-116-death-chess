using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
[SerializeField] GameObject melee;
[SerializeField] Transform playerTransform;
[SerializeField] float timeToDestroy = 1f;


    void Start() 
    {
        Destroy(melee, timeToDestroy);
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
}
