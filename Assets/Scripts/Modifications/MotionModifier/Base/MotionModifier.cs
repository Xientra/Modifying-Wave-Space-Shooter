/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;

public abstract class MotionModifier : Modification
{
    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Setter   *|
        \*============*/

        public void SetSpeed(float speed)                   { m_speed = speed; }
        public void SetJitterStrength(float jitterStrength) { m_jitterStrength = jitterStrength; }

        /*===============*\
        |*   Overrides   *|
        \*===============*/

        public override void ApplyModification()
        {
            // Apply Transformation and rotation modifications
            // -----------------------------------------------
            m_modificationTarget.transform.Translate(ComputeDirection() * CollectSpeedModifiers() * Time.deltaTime, Space.Self);
            m_modificationTarget.transform.Rotate(Vector3.up, ComputeRotation() * CollectSpeedModifiers() * Time.deltaTime, Space.Self);
            m_modificationTarget.transform.Translate(ComputeJitter() * m_jitterStrength * Time.deltaTime, Space.Self);
        }

    /*================================*\
    |*   Protected Member Variables   *|
    \*================================*/

        /*==================*\
        |*   Input Memory   *|
        \*==================*/
        
        [SerializeField] protected float m_speed            = 1;
        [SerializeField] protected float m_jitterStrength   = 1;

    /*================================*\
    |*   Protected Member Functions   *|
    \*================================*/

        /*===============*\
        |*   Abstracts   *|
        \*===============*/

        protected abstract Vector3 ComputeDirection();
        protected abstract float ComputeRotation();

        protected abstract Vector3 ComputeJitter();

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private float CollectSpeedModifiers()
        {
            // Define return value
            // -------------------
            float finalSpeed = m_speed;

            // Iterate over ModificationManager
            // --------------------------------
            foreach(Modification modification in m_modificationTarget.GetModificationManager().GetModifications())
            {
                // Skip (No SpeedModifier)
                // -----------------------
                if(!(modification is SpeedModifier)) continue;

                // Multiply speedModifier with finalSpeed
                // --------------------------------------
                finalSpeed *= (modification as SpeedModifier).GetSpeed();
            }

            // Return final speed
            // ------------------
            return finalSpeed;
        }
}
