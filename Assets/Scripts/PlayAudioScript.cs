using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayAudioScript : MonoBehaviour
{

    FMODUnity.StudioEventEmitter eventEmitter;


    // Start is called before the first frame update
    void Awake()
    {
        eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlaySpawnSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/EnemySpawn", transform.position);
        // Debug.Log("Playing SPAWN sound");
    }

    void PlayDestroySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyDestroyed", transform.position);
        // Debug.Log("Playing DESTROY sound");
    }
}
