/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*=========================*\
|*   CLASS: SpawnObjects   *|
\*=========================*/

public class Move : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Update()
    {
        transform.Translate(m_direction * m_speed * Time.deltaTime);
    }

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetSpeed(float speed)           { m_speed = speed; }
        public void SetDirection(Vector3 direction) { m_direction = direction; }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private Vector3 m_direction = Vector3.forward;
        private float m_speed     = 1;
}
