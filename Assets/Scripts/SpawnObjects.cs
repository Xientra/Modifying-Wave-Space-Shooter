/*===================*\
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
        foreach(GameObject obj in m_objects)
        {
            GameObject instance = Instantiate(obj);
            instance.AddComponent<RandomMotion>();
        }
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        private List<GameObject> m_objects = null;
}
