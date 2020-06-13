/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

/*===================================*\
|*   CLASS: ZickZackMotionModifier   *|
\*===================================*/

public class ZickZackMotionModifier : MotionModifier
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetZickZackTime(float zickzackTime)       { m_zickZackTime = zickzackTime; }
            
        /*================================*\
        |*   Protected Member Functions   *|
        \*================================*/

            /*===============*\
            |*   Overrides   *|
            \*===============*/

            protected override Vector3 ComputeDirection()  { return Vector3.zero; }
            protected override Vector3 ComputeJitter() 
            {
                // Update timer
                // ------------
                timer += Time.deltaTime;

                // If timer up
                // -----------
                if (timer > m_zickZackTime)
                {
                    // Revert direction
                    // ----------------
                    flyRight *= -1;

                    // Reset timer
                    // -----------
                    timer = 0;
                }

                // Return jitter
                // -------------
                return Vector3.right * flyRight;
            }
            protected override float ComputeRotation() { return 0; }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/

        [SerializeField] private float m_zickZackTime = 0.5f;
        
        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private float flyRight  = -1;
        private float timer     = 0;
}

