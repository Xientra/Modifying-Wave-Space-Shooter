/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*=========================*\
|*   CLASS: SpawnObjects   *|
\*=========================*/

public class RandomMotion : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Start()
    {
        direction.x = Random.Range(0, 1);
        direction.z = Random.Range(0, 1);
        m_speed     = Random.Range(1, 20);
    }
    void Update()
    {
        transform.Translate(direction * m_speed);
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/
    
        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private Vector3 direction = default;
        private float m_speed     = 0;
}
