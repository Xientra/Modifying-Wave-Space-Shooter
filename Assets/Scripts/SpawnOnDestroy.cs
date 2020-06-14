using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public void Spawn()
    {
        GameObject instance         = Instantiate(spawnObject);
        instance.transform.position = transform.position; 
    }

    public GameObject spawnObject;
}
