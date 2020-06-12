/*===================*\
|*   System Usings   *|
\*===================*/

using System.Collections;

/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

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
        Destroy(m_hitObject);

        // Spawn Explosion
        // ---------------
        GameObject explosion = Instantiate(m_explosion);
        explosion.transform.position = transform.position;

        // Destroy this gameobject
        // -----------------------
        StartCoroutine(LateDestroy());
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [HideInInspector]
        [SerializeField] private string m_tag           = "";
        [SerializeField] private GameObject m_explosion = null;

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

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private IEnumerator LateDestroy()
        {
            // Wait for hitObject to be destroyed
            // ----------------------------------
            while(m_hitObject != null) { yield return null; }
            
            // Destroy this
            // ------------
            Destroy(this.gameObject);
            
            // Return
            // ------
            yield return null;
        }

}
