using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public KeyCode inputKey = KeyCode.RightControl;
    public float minTime = 2f;
    public float maxTime = 4f;

    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
       // Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
