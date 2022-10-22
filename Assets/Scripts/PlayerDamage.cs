using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float invulnerabilityLength;
    private float _timeSinceLastHit;
    
    [SerializeField] private float playerStability;
    [SerializeField] private float forceMultiplier;
    public Vector3 CurrentForceOnPlayer { get; private set; }

    private void Update()
    {
        _timeSinceLastHit += Time.deltaTime;
        float forceX = NewForce(CurrentForceOnPlayer.x);
        float forceY = NewForce(CurrentForceOnPlayer.y);
        float forceZ = NewForce(CurrentForceOnPlayer.z);

        CurrentForceOnPlayer = new Vector3(forceX, forceY, forceZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_timeSinceLastHit > invulnerabilityLength && other.gameObject.CompareTag("Enemy"))
        {
            _timeSinceLastHit = 0;
            Enemy attackingEnemy = other.GetComponentInParent<Enemy>();
            Vector3 attackingDirection = new Vector3(attackingEnemy.MoveAmountX, 0, attackingEnemy.MoveAmountY).normalized;
            // attackingDirection.y = 0.5f;
            CurrentForceOnPlayer = attackingDirection * forceMultiplier;
            
            Debug.Log("Hit Player");
            UIManager.Instance.TakeDamage(1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyHit", transform.position);
        }
    }
    
    private float NewForce(float currentForceAxis)
    {
        if (currentForceAxis == 0)
        {
            return 0;
        }
        
        float force = 0;
        if (currentForceAxis > 0)
        {
            force = currentForceAxis - playerStability * Time.deltaTime;
            if (force < 0)
            {
                force = 0;
            }
        }
        else if (currentForceAxis < 0)
        {
            force = currentForceAxis + playerStability * Time.deltaTime;
            if (force > 0)
            {
                force = 0;
            }
        }
        return force;
    }
}