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
        int angle = 0;

        Vector3 up      = transform.forward;
        Vector3 left    = new Vector3(-1, 0, 1);
        Vector3 right   = new Vector3(1, 0, 1);

        foreach(GameObject obj in m_objects)
        {
            GameObject instance         = Instantiate(obj);
            instance.transform.position = transform.position;
            Move move                   = instance.AddComponent<Move>();

            Vector3 direction = Quaternion.AngleAxis(angle, transform.forward) * transform.up;

            move.SetDirection(direction);
            move.SetSpeed(Random.Range(1, 10));
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
