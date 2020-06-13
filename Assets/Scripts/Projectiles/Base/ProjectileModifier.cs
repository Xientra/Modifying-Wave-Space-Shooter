/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===============================*\
|*   CLASS: ProjectileModifier   *|
\*===============================*/

public abstract class ProjectileModifier : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    private void Awake() 
    {
        // Destroy after some time for cleanup 
        // -----------------------------------
        Destroy(gameObject, 10f);

        // Define runtime
        // --------------
        m_formerPosition = transform.position;
    }
    private void FixedUpdate()
    {
        // Compute new position
        // --------------------
        Vector3 newPosition = m_formerPosition + ComputeDirection();

        // Move object to new position
        // ---------------------------
        m_rigidbody.MovePosition(m_speed * (newPosition + ComputeJitter(newPosition)));

        // Update formerPosition
        // ---------------------
        m_formerPosition = transform.position;
    }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] protected Rigidbody m_rigidbody = null;
        [SerializeField] protected float m_speed         = 1;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private Vector3 m_formerPosition = default;

    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Abstracts   *|
        \*===============*/

        protected abstract Vector3 ComputeDirection();
        protected abstract Vector3 ComputeJitter(Vector3 currentPosition);
}
