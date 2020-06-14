using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject instance         = Instantiate(spawnObject);
        instance.transform.position = transform.position; 
    }

    public GameObject spawnObject;
}
