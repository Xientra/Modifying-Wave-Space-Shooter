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

    void OnTriggerEnter(Collider collider)
    {
        // Early exit (Wrong tag)
        // ----------------------
        if (collider.transform.tag != m_tag) return;

        // Fetch Damageable interface
        // --------------------------
        IDamagable damageableInterface = collider.gameObject.GetComponent<IDamagable>();

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
        
        [SerializeField] private string m_tag         = "";
        [SerializeField] private int m_damage         = 3;
}
