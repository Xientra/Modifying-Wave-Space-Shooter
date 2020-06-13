/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*=============================*\
|*   CLASS: ApplyDamageOnHit   *|
\*=============================*/

public class ApplyDamageOnHit : MonoBehaviour
{
    /*============*\
    |*   Events   *|
    \*============*/

    void OnCollisionEnter(Collision collision)
    {
        // Early exit (Wrong tag)
        // ----------------------
        if (collision.transform.tag != m_tag) return;

        collision.gameObject.GetComponent<Player>()

        // Early exit (No IDamageable object)
        // ----------------------------------
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private Collision m_collider = null;
        [SerializeField] private string m_tag         = "";
}
