﻿/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections.Generic;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*=========================*\
|*   CLASS: SpawnObjects   *|
\*=========================*/

public class SpawnObjects : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Start()
    {
        // Iterate over Objects
        // --------------------
        int angle = 0;
        foreach(GameObject obj in m_objects)
        {
            GameObject instance         = Instantiate(obj);
            instance.transform.position = transform.position;
            Move move                   = instance.AddComponent<Move>();

            Vector3 direction = Quaternion.AngleAxis(angle, transform.forward) * transform.up;

            move.SetDirection(direction);
            move.SetSpeed(Random.Range(1, 2));
            angle += 360 / m_objects.Count;
        }
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private List<GameObject> m_objects = null;
}
