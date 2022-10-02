using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEventScript : MonoBehaviour
{

    public GameObject particleToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnemySpawnParticle();
        }

    }

    public void EnemySpawnParticle()
    {
        GameObject newParticle = Instantiate(particleToSpawn, transform);
        Destroy(newParticle, 1f);
    }
}
