using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Vector3 waveDirection;
    public float waveSpeed;
    public float waveHeight;
    public float waveDuration;

    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(WaveLength());
        waveDirection = this.transform.position;
    }

    void MoveWave()
    {
        waveDirection.x--;
        transform.position = waveDirection * waveSpeed * Time.deltaTime;
    }

    IEnumerator WaveLength()
    {
        yield return new WaitForSeconds(1);
        MoveWave();
    }
}
