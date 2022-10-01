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

    void Update()
    {
        // transform.localPosition += playerTransform * Time.deltaTime;
    }
}
