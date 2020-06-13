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
            m_modificationTarget.transform.Translate(ComputeDirection() * AccumulateSpeeds() * Time.deltaTime, Space.Self);
            m_modificationTarget.transform.Rotate(Vector3.up, ComputeRotation() * AccumulateSpeeds() * Time.deltaTime, Space.Self);
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

        protected virtual Vector3 ComputeDirection() { return Vector3.forward; }
        protected virtual float ComputeRotation() { return 0; }

        protected virtual Vector3 ComputeJitter() { return Vector3.zero; }

    /*==============================*\
    |*   Private Member Functions   *|
    \*==============================*/

        /*=================*\
        |*   Auxiliaries   *|
        \*=================*/

        private float AccumulateSpeeds()
        {
            // Define return value
            // -------------------
            float finalSpeed = m_speed;

            // Iterate over ModificationManager
            // --------------------------------
            foreach(SpeedModifier modification in m_modificationTarget.GetModificationManager().CollectModifiers<SpeedModifier>())
            {
                // Multiply speedModifier with finalSpeed
                // --------------------------------------
                finalSpeed *= modification.GetSpeed();
            }

            // Return final speed
            // ------------------
            return finalSpeed;
        }
}
