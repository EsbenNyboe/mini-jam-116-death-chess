using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int xAmount;
    [SerializeField] private int zAmount;

    private bool _alternateDirection;

    private float _timer;
    [SerializeField] private float moveFrequency;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > moveFrequency)
        {
            _timer = 0;
            StartCoroutine(Move());
        }
        
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(Move());
        // }
    }

    IEnumerator Move()
    {
        Vector3 pos = new Vector3();
        int xCounter = 0;
        while (xCounter < xAmount)
        {
            yield return new WaitForEndOfFrame();
            xCounter++;
            pos += new Vector3(1, 0, 0);
        }
        
        // _alternateDirection = !_alternateDirection;
        // int zVelocity = _alternateDirection ? -1 : 1;
        // int zCounter = 0;
        // while (zCounter < zAmount)
        // {
        //     yield return new WaitForEndOfFrame();
        //     zCounter++;
        //     pos += new Vector3(0, 0, zVelocity);
        // }

        transform.position += pos;
    }
}
