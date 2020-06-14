/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;
using UnityEngine.Events;

/*=========================*\
|*   CLASS: ExplodeOnHit   *|
\*=========================*/

public class ExplodeOnHit : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    protected void OnTriggerEnter(Collider other)
    {
        // Early exit (Colliding object has wrong tag)
        // -------------------------------------------
        if (other.tag != m_tag) return;

        // Destroy other gameobject
        // ------------------------
        m_hitObject = other.gameObject;
        if(m_destroyHitObject) Destroy(m_hitObject);

        // Spawn Explosion
        // ---------------
        GameObject explosion = Instantiate(m_explosion);
        explosion.transform.position = transform.position;
        onDestroy.Invoke();

        // Destroy this
        // ------------
        if(m_destroyThis) Destroy(this.gameObject);    
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [HideInInspector]
        [SerializeField] private string m_tag            = "";
        [SerializeField] private GameObject m_explosion  = null;
        [SerializeField] private bool m_destroyHitObject = true;
        [SerializeField] private bool m_destroyThis      = true;
        public UnityEvent onDestroy = null;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private GameObject m_hitObject = null; 

#if UNITY_EDITOR
        /*===================*\
        |*   Editor Memory   *|
        \*===================*/
    
#pragma warning disable 0414
        [HideInInspector]
        [SerializeField] private int m_index = 0;
#pragma warning restore 0414
#endif
}
