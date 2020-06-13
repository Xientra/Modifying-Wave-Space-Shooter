/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*=================================*\
|*   CLASS: HommingTargetManager   *|
\*=================================*/

public class HommingTargetManager : MonoBehaviour
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void Start()
    {
        // Fetch waveController
        // --------------------
        m_waveController = GameObject.Find("WaveController")?.GetComponent<WaveController>();
    }
    void Update()
    {
        // Early exit (No wavecontroller found)
        // ------------------------------------
        if (m_waveController == null) return;

        // Update nextTarget
        // -----------------
        if (m_nextTarget == null)
        {
            // Fetch next target
            // -----------------
            FetchNextTarget();

            // Apply target to homming modifiers
            // ---------------------------------
            ApplyTarget();
        }
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/
        
        [SerializeField] private WaveController m_waveController          = null;
        [SerializeField] private ModificationObject m_modificationObject  = null;

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private GameObject m_nextTarget = null;

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private void FetchNextTarget()
        {
            // Fetch active enemies
            // --------------------
            GameObject[] activeEnemies = m_waveController.GetActiveEnemies();
            
            // Early exit (No active Enemies) 
            // ------------------------------
            if (activeEnemies.Length == 0) return;

            // Set nextEnemy 
            // -------------
            m_nextTarget = activeEnemies[0];

            // Compute distance to enemy
            // -------------------------
            float distanceToEnemy = float.MaxValue;

            // Find closest enemy
            // ------------------
            foreach(GameObject enemy in activeEnemies)
            {
                // Skip ( enemy null )
                // -------------------
                if (enemy == null) continue;

                // Compute distance to enemy
                // -------------------------
                float nextDistanceToEnemy = (enemy.transform.position - m_modificationObject.transform.position).magnitude;

                // Check if enemy is closer than former enemy
                // ------------------------------------------
                if (nextDistanceToEnemy < distanceToEnemy) 
                {
                    // Set closer enemy
                    // ----------------
                    m_nextTarget = enemy;
                }
            }
        }
        private void ApplyTarget()
        {
            // Iterate over modifications
            // --------------------------
            foreach(Modification mod in m_modificationObject.GetModificationManager().GetModifications())
            {
                // Skip (No homming modifier)
                // --------------------------
                if (!(mod is HommingMotionModifier)) continue;

                // Apply new target
                // ----------------
                (mod as HommingMotionModifier).SetHommingTarget(m_nextTarget.transform);
            }
        }
}
