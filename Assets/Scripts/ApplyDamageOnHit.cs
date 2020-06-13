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

        // Fetch Damageable interface
        // --------------------------
        IDamagable damageableInterface = collision.gameObject.GetComponent<IDamagable>();

        // Early exit (No IDamageable object)
        // ----------------------------------
        if (damageableInterface == null) return;

        // Apply damage
        // ------------
        damageableInterface.TakeDamage(m_damage);
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private Collision m_collider = null;
        [SerializeField] private string m_tag         = "";
        [SerializeField] private int m_damage         = 3;
}
